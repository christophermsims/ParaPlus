using System;
using System.IO;
using System.Linq;
using System.Text;
using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Model;
using ScottPlot;

using ShapeCrawler;

namespace ParaPlus.Business.Presentations
{
    public class OnePagerPresentationBuilder( 
        IFileProcessor<OnePagerDetails> fileProcessor, 
        IQuarterlySummaryService<OriginalPatentFiling> filingSummaryService, 
        IQuarterlySummaryService<OriginalPatentIssuance> issuanceSummaryService,
        Action<string>? reportAction = null)
    {
        private readonly IFileProcessor<OnePagerDetails> _onePagerFileProcessor = fileProcessor;
        private readonly IQuarterlySummaryService<OriginalPatentFiling> _filingsSummaryService = filingSummaryService;
        private readonly IQuarterlySummaryService<OriginalPatentIssuance> _issuanceSummaryService = issuanceSummaryService;
        private readonly Action<string> _reporter = reportAction ?? Console.WriteLine;

		private string _quarterlyOnePagerFile = @"";
        private string _templateFile = @"";
        private string _outputFolder = @"";
		private readonly string _outputFile = @"Patent Applications.pptx";

        public string QuarterlyOnePagersFilePath
        {
            get => _quarterlyOnePagerFile;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _reporter("Warning: File to process path is empty. Retaining previous value.");
                    return;
                }
                if (!File.Exists(value))
                {
                    _reporter($"Warning: File '{value}' does not exist. Retaining previous value.");
                    return;
                }
                _quarterlyOnePagerFile = value;
            }
		}

        public string TemplateFilePath
        {
            get => _templateFile;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _reporter("Warning: Template file path is empty. Retaining previous value.");
                    return;
                }
                if (!File.Exists(value))
                {
                    _reporter($"Warning: Template file '{value}' does not exist. Retaining previous value.");
                    return;
                }
                _templateFile = value;
            }
		}

        public string OutputFolder
        {
            get => _outputFolder;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _reporter("Warning: Output folder path is empty. Retaining previous value.");
                    return;
                }
                if (!Directory.Exists(value))
                {
                    _reporter($"Warning: Output folder '{value}' does not exist. Retaining previous value.");
                    return;
                }
                _outputFolder = value;
            }
        }

        private bool ValidateRequiredFields() 
        {
            // Validate required inputs
            if (string.IsNullOrWhiteSpace(_quarterlyOnePagerFile) || !File.Exists(_quarterlyOnePagerFile))
            {
                _reporter("Error: FileToProcess has not been set or does not exist. Aborting presentation generation.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_templateFile) || !File.Exists(_templateFile))
            {
                _reporter("Error: TemplateFile has not been set or does not exist. Aborting presentation generation.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_outputFolder) || !Directory.Exists(_outputFolder))
            {
                _reporter("Error: OutputFolder has not been set or does not exist. Aborting presentation generation.");
                return false;
            }

            return true;
		}

		public void BuildPresentation(string? summaryStartQuarter = null, string? summaryEndQuarter = null)
        {
            if (!ValidateRequiredFields())
            {
                return;
            }

            _reporter("Parsing Quarterly 1 Pager Report");

            var details = _onePagerFileProcessor.ProcessFile(_quarterlyOnePagerFile).ToList();
            if (!details.Any())
            {
                _reporter("No details found in the input file. Aborting presentation generation.");
                return;
            }

            string finalOutputFile = BuildOutputFileName(details, _outputFile);

            _reporter("Finished parsing Quarterly 1 Pager Report");

            _reporter("Reading Template File");

            var template = new Presentation(_templateFile);

            var titleSlide = template.Slide(1);
            var sectionSlide = template.Slide(3);

            var quarterlyPresentation = new Presentation(p => p.Slide())
            {
                SlideHeight = template.SlideHeight,
                SlideWidth = template.SlideWidth
            };

            quarterlyPresentation.Slides.Add(titleSlide);

            _reporter("Creating Quarterly Summary Chart");
            AddSummarySlide(quarterlyPresentation, template, summaryStartQuarter, summaryEndQuarter);

            _reporter("Creating Quarterly Presentation");

            string currentProduct = string.Empty;

            foreach(var detail in details)
            {
                if (!currentProduct.Equals(detail.DesignatedProduct))
                {
                    currentProduct = detail.DesignatedProduct;
                    sectionSlide.Shape("SectionTitle").TextBox!.SetText(currentProduct);
                    sectionSlide.Shape("SectionTitle").SetFontSize(24);
                    quarterlyPresentation.Slides.Add(sectionSlide);
                }

                var detailSlide = template.Slide(4);
                
                StringBuilder inventorsBox = new();
                inventorsBox.AppendLine($"Title: {detail.Title}");
                inventorsBox.Append($"Inventors: {detail.AllInventors}");

                detailSlide.Shape("Inventors").TextBox!.SetText(inventorsBox.ToString());
                detailSlide.Shape("Inventors").SetFontSize(9);

                StringBuilder referenceBox = new();
                referenceBox.AppendLine($"Zoom Ref.: {detail.ZOOM_REF}; Law Firm Ref.: {detail.LAWFIRM_REF}");
                referenceBox.Append($"Filing Date: {detail.FilingDate}"); 

                detailSlide.Shape("References").TextBox!.SetText(referenceBox.ToString());
                detailSlide.Shape("References").SetFontSize(9);

                StringBuilder prodcutBox = new();
                prodcutBox.AppendLine($"Product: {detail.DesignatedProduct}");
                prodcutBox.Append($"Technology: {detail.Technology}");

                detailSlide.Shape("Products").TextBox!.SetText(prodcutBox.ToString());
                detailSlide.Shape("Products").SetFontSize(9);

                detailSlide.Shape("UseCase_Text").TextBox!.SetText(detail.IllustrativeUseCase);
                detailSlide.Shape("UseCase_Text").SetFontSize(8);
                detailSlide.Shape("UseCase_Text").SetFontColor("000000");

                detailSlide.Shape("SubjectMatter_Text").TextBox!.SetText(detail.GeneralSubjectMatter);
                detailSlide.Shape("SubjectMatter_Text").SetFontSize(8);
                detailSlide.Shape("SubjectMatter_Text").SetFontColor("000000");

                quarterlyPresentation.Slides.Add(detailSlide);
            }

            quarterlyPresentation.Slide(1).Remove();

            _reporter("Saving Presentation");

            quarterlyPresentation.Save(finalOutputFile);

            _reporter($"Completed. Saved output to: {finalOutputFile}");
        }

        private string BuildOutputFileName(IReadOnlyList<OnePagerDetails> details, string defaultOutputPath)
        {
            var quarterValues = details
                .Select(d => d.FilingDateFiscalQuarter?.Trim())
                .Where(q => !string.IsNullOrWhiteSpace(q))
                .Distinct()
                .ToList();

            if (!quarterValues.Any())
            {
                _reporter("Warning: Filing date fiscal quarter not found in the data. Using default output filename.");
                return defaultOutputPath;
            }

            if (quarterValues.Count > 1)
            {
                _reporter("Warning: Multiple filing fiscal quarters found in the data; using the first value.");
            }

            var rawQuarter = quarterValues[0] ?? string.Empty;
            if (!TryBuildFiscalQuarterLabel(rawQuarter, out var fiscalQuarterLabel))
            {
                Console.WriteLine($"Warning: Could not normalize fiscal quarter '{rawQuarter}'. Using raw value in filename.");
                fiscalQuarterLabel = rawQuarter;
            }

            return Path.Combine(_outputFolder, $"{fiscalQuarterLabel} {_outputFile}");
        }

        private bool TryBuildFiscalQuarterLabel(string rawQuarter, out string label)
        {
            label = string.Empty;
            if (string.IsNullOrWhiteSpace(rawQuarter))
            {
                _reporter("Warning: Empty fiscal quarter string passed to TryBuildFiscalQuarterLabel.");
                return false;
            }

            var normalized = rawQuarter.Trim();
            if (normalized.StartsWith("FY", StringComparison.OrdinalIgnoreCase))
            {
                var withoutFy = normalized.Substring(2).Trim();
                if (TryParseQuarter(withoutFy) is (var year, var quarter) && (year != 0 || quarter != 0))
                {
                    label = $"FY{year} Q{quarter}";
                    return true;
                }

                label = normalized;
                return true;
            }

            if (TryParseQuarter(normalized) is (var parsedYear, var parsedQuarter) && (parsedYear != 0 || parsedQuarter != 0))
            {
                label = $"FY{parsedYear} Q{parsedQuarter}";
                return true;
            }

            label = normalized;
            return true;
        }

        private static (int year, int quarter) TryParseQuarter(string quarterString)
        {
            var parts = quarterString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && int.TryParse(parts[0], out int year) && parts[1].StartsWith("Q") && int.TryParse(parts[1].Substring(1), out int quarter))
            {
                return (year, quarter);
            }

            return (0, 0);
        }

        private void AddSummarySlide(Presentation presentation, Presentation template, string? startQuarter, string? endQuarter)
        {
            try
            {
                _reporter("Generating Quarterly Patent Filings and Issuances Summary");
                var filingReport = _filingsSummaryService.GetQuarterlySummary(startQuarter, endQuarter);
                var issuanceReport = _issuanceSummaryService.GetQuarterlySummary(startQuarter, endQuarter);

                var summarySlide = template.Slide(2); // Using summary slide template
                summarySlide.Shape("Title").TextBox!.SetText("Quarterly Patent Filings and Issuances");

                var (quarterLabels, filingValues, issuanceValues) = BuildGroupedSummaryData(filingReport.QuarterlySummaries, issuanceReport.QuarterlySummaries);

                using var chartStream = new MemoryStream();
                GenerateGroupedBarChartImage(quarterLabels, filingValues, issuanceValues, chartStream);
                chartStream.Position = 0;

                summarySlide.Shapes.AddPicture(chartStream);
                presentation.Slides.Add(summarySlide);
                _reporter("Summary slide added");
            }
            catch (Exception ex)
            {
                _reporter($"Warning: Could not add summary slide: {ex.Message}");
            }
        }

        private static (List<string> QuarterLabels, List<double> FilingValues, List<double> IssuanceValues) BuildGroupedSummaryData(IEnumerable<QuarterlySummaryData> filingSummaries, IEnumerable<QuarterlySummaryData> issuanceSummaries)
        {
            var allQuarters = filingSummaries.Select(q => q.Quarter)
                .Union(issuanceSummaries.Select(q => q.Quarter))
                .Distinct()
                .OrderBy(quarter => {
                    var (year, q) = ParseQuarter(quarter);
                    return year * 10 + q;
                })
                .ToList();

            var quarterLabels = allQuarters.Select(FormatQuarterLabel).ToList();
            var filingValues = allQuarters.Select(quarter => filingSummaries.FirstOrDefault(q => q.Quarter == quarter)?.CumulativeCount ?? 0d).ToList();
            var issuanceValues = allQuarters.Select(quarter => issuanceSummaries.FirstOrDefault(q => q.Quarter == quarter)?.CumulativeCount ?? 0d).ToList();

            return (quarterLabels, filingValues, issuanceValues);
        }

        private static void GenerateGroupedBarChartImage(List<string> quarterLabels, List<double> filingValues, List<double> issuanceValues, Stream outputStream)
        {
            var plot = new ScottPlot.Plot();
            var palette = new ScottPlot.Palettes.Category20();

            var bars = new List<ScottPlot.Bar>();
            var ticks = new List<ScottPlot.Tick>();

            for (int i = 0; i < quarterLabels.Count; i++)
            {
                double groupBase = i * 3.0;
                bars.Add(new ScottPlot.Bar { Position = groupBase + 1, Value = filingValues[i], FillColor = palette.GetColor(0) });
                bars.Add(new ScottPlot.Bar { Position = groupBase + 2, Value = issuanceValues[i], FillColor = palette.GetColor(1) });
                ticks.Add(new ScottPlot.Tick(groupBase + 1.5, quarterLabels[i]));
            }

            var barPlot = plot.Add.Bars(bars.ToArray());

            // Add value labels above each bar
            foreach (var bar in barPlot.Bars)
            {
                bar.Label = bar.Value.ToString();
            }

            // Customize label style
            barPlot.ValueLabelStyle.Bold = true;
            barPlot.ValueLabelStyle.FontSize = 18;

            plot.Legend.IsVisible = true;
            plot.Legend.Alignment = ScottPlot.Alignment.UpperCenter;
            plot.Legend.ManualItems.Add(new() { LabelText = "Cumulative Families", FillColor = palette.GetColor(0) });
            plot.Legend.ManualItems.Add(new() { LabelText = "Cumulative Patents Issued", FillColor = palette.GetColor(1) });
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
            plot.Axes.Bottom.MajorTickStyle.Length = 1;
            plot.Axes.Bottom.TickLabelStyle.Alignment = ScottPlot.Alignment.MiddleRight;
            plot.Axes.Bottom.TickLabelStyle.Rotation = -45;
            plot.Axes.Bottom.TickLabelStyle.OffsetY = 5;
            plot.Axes.Bottom.MinimumSize = 50;
            plot.HideGrid();
            plot.Axes.Margins(bottom: 0, top: .2);
            string tempFile = Path.GetTempFileName() + ".png";
            plot.SavePng(tempFile, 1200, 600);
            using (var fileStream = File.OpenRead(tempFile))
            {
                fileStream.CopyTo(outputStream);
            }
            File.Delete(tempFile);
        }

        private static string FormatQuarterLabel(string quarter)
        {
            // Convert "2025 Q1" to "Q1'25" format
            var parts = quarter.Split(' ');
            if (parts.Length == 2 && int.TryParse(parts[0], out var year) && parts[1].StartsWith('Q'))
            {
                return $"{parts[1]}-{year % 100}";
            }
            return quarter;
        }

        private static (int year, int quarter) ParseQuarter(string quarterString)
        {
            // Expected format: "YYYY QX" (e.g., "2015 Q3")
            var parts = quarterString.Split(' ');
            if (parts.Length == 2 && int.TryParse(parts[0], out int year) && parts[1].StartsWith("Q") && int.TryParse(parts[1].Substring(1), out int quarter))
            {
                return (year, quarter);
            }
            return (0, 0); // Fallback for invalid format
        }
    }
}