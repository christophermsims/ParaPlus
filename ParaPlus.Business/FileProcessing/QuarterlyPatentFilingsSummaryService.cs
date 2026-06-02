using ParaPlus.Business.Model;

namespace ParaPlus.Business.FileProcessing
{
    public class QuarterlyPatentFilingsSummaryService : QuarterlySummaryServiceBase<OriginalPatentFiling>, IQuarterlySummaryService<OriginalPatentFiling>
    {
        private readonly string _filePath;

        public QuarterlyPatentFilingsSummaryService(string filePath)
        {
            _filePath = filePath;
        }

        protected override string GetFilePath() => _filePath;

        protected override string GetReportTitle() => "Quarterly Patent Filings Summary";

        protected override string GetQuarterValue(OriginalPatentFiling item) => item.FilingDateFiscalQuarter;

        protected override IEnumerable<OriginalPatentFiling> ParsePatentData()
        {
            IFileVerifier fileVerifier = new OriginalPatentFilingsFileVerifier();
            IFileProcessor<OriginalPatentFiling> fileProcessor = new OriginalPatentFilingsFileProcessor(fileVerifier);

            IEnumerable<OriginalPatentFiling> patentFilings = fileProcessor.ProcessFile(GetFilePath());
            return patentFilings;
        }
    }
}
