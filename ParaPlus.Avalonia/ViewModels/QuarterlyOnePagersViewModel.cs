using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Platform;
using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Helper;
using ParaPlus.Business.Jobs;
using ParaPlus.Business.Model;
using ParaPlus.Business.Presentations;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParaPlus.Avalonia.ViewModels
{
    public partial class QuarterlyOnePagersViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private string? quarterlyPatentFilings;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private string? quarterlyPatentsIssued;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private string? quarterlyOnePagersFile;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private string? outputDirectory;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private string? startFiscalYear;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private string? startFiscalQuarter;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private string? endFiscalYear;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private string? endFiscalQuarter;

        public ObservableCollection<string> FiscalYears { get; }
        public ObservableCollection<string> FiscalQuarters { get; }

        [ObservableProperty]
        private string logs = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GeneratePresentationCommand))]
        private bool isProcessing;

        public QuarterlyOnePagersViewModel()
        {
            FiscalYears = new ObservableCollection<string>(Enumerable.Range(2021, 10).Select(y => y.ToString()));
            FiscalQuarters = new ObservableCollection<string> { "Q1", "Q2", "Q3", "Q4" };

            StartFiscalQuarter = "Q1";
            StartFiscalYear = "2022";
            EndFiscalQuarter = FiscalYear.LastFiscalQuarter;
            EndFiscalYear = FiscalYear.LastFiscalYear;
        }

        private void Log(string message)
        {
            Dispatcher.UIThread.Post(() =>
            {
                Logs += $"{message}{Environment.NewLine}";
            });
        }

        private bool CanGeneratePresentation()
        {
            if (IsProcessing) return false;

            // File and directory checks
            if (string.IsNullOrWhiteSpace(QuarterlyPatentFilings) || !File.Exists(QuarterlyPatentFilings)) return false;
            if (string.IsNullOrWhiteSpace(QuarterlyPatentsIssued) || !File.Exists(QuarterlyPatentsIssued)) return false;
            if (string.IsNullOrWhiteSpace(QuarterlyOnePagersFile) || !File.Exists(QuarterlyOnePagersFile)) return false;
            if (string.IsNullOrWhiteSpace(OutputDirectory) || !Directory.Exists(OutputDirectory)) return false;

            // Combo box selections
            if (string.IsNullOrWhiteSpace(StartFiscalYear)) return false;
            if (string.IsNullOrWhiteSpace(StartFiscalQuarter)) return false;
            if (string.IsNullOrWhiteSpace(EndFiscalYear)) return false;
            if (string.IsNullOrWhiteSpace(EndFiscalQuarter)) return false;

            return true;
        }

        [RelayCommand(CanExecute = nameof(CanGeneratePresentation))]
        private async Task GeneratePresentationAsync()
        {
            IsProcessing = true;
            Logs = string.Empty;
            Log("Starting presentation generation...");

            string tempTemplatePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}_QuarterlyPatentApplications_Template.pptx");

            try
            {
                // Extract the embedded resource to a temporary file
                Log("Loading template from application resources...");
                await using (var assetStream = AssetLoader.Open(new Uri("avares://ParaPlus.Avalonia/Templates/QuarterlyPatentApplications_Template.pptx")))
                await using (var fileStream = File.Create(tempTemplatePath))
                {
                    await assetStream.CopyToAsync(fileStream);
                }
                Log("Template loaded.");


                await Task.Run(() => // Offload business logic to a background thread
                {
                    IFileVerifier onePagerFileVerifier = new OnePagerFileVerifier();
                    IFileProcessor<OnePagerDetails> onePagerFileProcessor = new OnePagerFileProcessor(onePagerFileVerifier);
                    IQuarterlySummaryService<OriginalPatentFiling> filingsSummaryService = new QuarterlyPatentFilingsSummaryService(QuarterlyPatentFilings);
                    IQuarterlySummaryService<OriginalPatentIssuance> issuanceSummaryService = new QuarterlyPatentIssuancesSummaryService(QuarterlyPatentsIssued);
                    OnePagerPresentationBuilder presentationBuilder = new(
                        onePagerFileProcessor,
                        filingsSummaryService,
                        issuanceSummaryService,
                        Log
                    );

                    string startingQuarter = $"{StartFiscalYear} {StartFiscalQuarter}";
                    string endingQuarter = $"{EndFiscalYear} {EndFiscalQuarter}";

                    presentationBuilder.TemplateFilePath = tempTemplatePath;
                    presentationBuilder.QuarterlyOnePagersFilePath = QuarterlyOnePagersFile;
                    presentationBuilder.OutputFolder = OutputDirectory;

                    presentationBuilder.BuildPresentation(startingQuarter, endingQuarter);
                });
                Log("Presentation generation complete.");
            }
            catch (Exception ex)
            {
                Log($"An error occurred: {ex.Message}");
            }
            finally
            {
                IsProcessing = false;
                // Clean up the temporary file
                if (File.Exists(tempTemplatePath))
                {
                    File.Delete(tempTemplatePath);
                    Log("Temporary template file cleaned up.");
                }
            }
        }

        [RelayCommand]
        private void OpenLink(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;

            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Log($"Could not open link: {ex.Message}");
            }
        }

        public string PatentFilingsLink => LinkConstants.QuaterlyPatentFilings;
        public string IssuedPatentsLink => LinkConstants.QuaterlyPatentIssuances;
        public string QuarterlyOnePagersLink => LinkConstants.QuaterlyOnePagers;
    }
}