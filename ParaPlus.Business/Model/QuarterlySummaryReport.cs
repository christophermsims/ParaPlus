namespace ParaPlus.Business.Model
{
    public class QuarterlySummaryData
    {
        public string Quarter { get; set; } = string.Empty;
        public int QuarterCount { get; set; }
        public int CumulativeCount { get; set; }
    }

    public class QuarterlySummaryReport
    {
        public string ReportTitle { get; set; } = string.Empty;
        public List<QuarterlySummaryData> QuarterlySummaries { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
