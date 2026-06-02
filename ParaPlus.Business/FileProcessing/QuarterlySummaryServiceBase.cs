using System;
using ParaPlus.Business.Model;

namespace ParaPlus.Business.FileProcessing
{
    public abstract class QuarterlySummaryServiceBase<T>
    {
        protected abstract string GetFilePath();
        protected abstract IEnumerable<T> ParsePatentData();
        protected abstract string GetQuarterValue(T item);
        protected abstract string GetReportTitle();

        public QuarterlySummaryReport GetQuarterlySummary()
        {
            return GetQuarterlySummary(startQuarter: null, endQuarter: null);
        }

        public QuarterlySummaryReport GetQuarterlySummary(string? startQuarter, string? endQuarter)
        {
            FiscalQuarter? start = null;
            FiscalQuarter? end = null;

            if (!string.IsNullOrWhiteSpace(startQuarter))
            {
                if (!FiscalQuarter.TryParse(startQuarter, out var parsedStart))
                {
                    throw new ArgumentException($"Invalid start quarter format: '{startQuarter}'", nameof(startQuarter));
                }

                start = parsedStart;
            }

            if (!string.IsNullOrWhiteSpace(endQuarter))
            {
                if (!FiscalQuarter.TryParse(endQuarter, out var parsedEnd))
                {
                    throw new ArgumentException($"Invalid end quarter format: '{endQuarter}'", nameof(endQuarter));
                }

                end = parsedEnd;
            }

            if (start.HasValue && end.HasValue && start.Value.CompareTo(end.Value) > 0)
            {
                throw new ArgumentException($"The start quarter '{startQuarter}' must be earlier than or equal to the end quarter '{endQuarter}'.");
            }

            var patentData = ParsePatentData();
            return GenerateQuarterlySummary(patentData, start, end);
        }

        protected QuarterlySummaryReport GenerateQuarterlySummary(IEnumerable<T> patentData, FiscalQuarter? startQuarter, FiscalQuarter? endQuarter)
        {
            // Group by quarter - include all data for cumulative calculation
            var quarterlyCounts = patentData
                .Where(item => !string.IsNullOrEmpty(GetQuarterValue(item)))
                .GroupBy(item => GetQuarterValue(item))
                .Select(g => new { Quarter = g.Key, Count = g.Count() })
                .Select(q => new { q.Quarter, q.Count, Parsed = ParseQuarterValue(q.Quarter) })
                .Where(q => q.Parsed is not null)
                .OrderBy(q => q.Parsed!.Value)
                .ToList();

            var summaries = new List<QuarterlySummaryData>();
            int cumulativeCount = 0;
            int cumulativeBeforeRange = 0;
            bool hasAddedStartQuarter = false;

            foreach (var quarterGroup in quarterlyCounts)
            {
                cumulativeCount += quarterGroup.Count;

                bool isInRange = IsInRange(quarterGroup.Parsed!.Value, startQuarter, endQuarter);

                if (isInRange)
                {
                    // If this is the first quarter in range and it's after startQuarter, add startQuarter first
                    if (!hasAddedStartQuarter && startQuarter.HasValue && quarterGroup.Parsed.Value.CompareTo(startQuarter.Value) > 0)
                    {
                        summaries.Add(new QuarterlySummaryData
                        {
                            Quarter = startQuarter.Value.ToString(),
                            QuarterCount = 0,
                            CumulativeCount = cumulativeBeforeRange
                        });
                        hasAddedStartQuarter = true;
                    }

                    summaries.Add(new QuarterlySummaryData
                    {
                        Quarter = quarterGroup.Quarter,
                        QuarterCount = quarterGroup.Count,
                        CumulativeCount = cumulativeCount
                    });

                    hasAddedStartQuarter = true;
                }
                else
                {
                    cumulativeBeforeRange = cumulativeCount;
                }
            }

            // If we haven't added the start quarter yet and it's within range, add it
            if (!hasAddedStartQuarter && startQuarter.HasValue && IsInRange(startQuarter.Value, startQuarter, endQuarter))
            {
                summaries.Insert(0, new QuarterlySummaryData
                {
                    Quarter = startQuarter.Value.ToString(),
                    QuarterCount = 0,
                    CumulativeCount = cumulativeBeforeRange
                });
            }

            return new QuarterlySummaryReport
            {
                ReportTitle = GetReportTitle(),
                QuarterlySummaries = summaries,
                TotalCount = cumulativeCount
            };
        }

        protected bool IsInRange(FiscalQuarter quarter, FiscalQuarter? startQuarter, FiscalQuarter? endQuarter)
        {
            if (startQuarter.HasValue && quarter.CompareTo(startQuarter.Value) < 0)
            {
                return false;
            }

            if (endQuarter.HasValue && quarter.CompareTo(endQuarter.Value) > 0)
            {
                return false;
            }

            return true;
        }

        protected FiscalQuarter? ParseQuarterValue(string quarterString)
        {
            if (FiscalQuarter.TryParse(quarterString, out var quarter))
            {
                return quarter;
            }

            return null;
        }

        protected (int year, int quarter) ParseQuarter(string quarterString)
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
