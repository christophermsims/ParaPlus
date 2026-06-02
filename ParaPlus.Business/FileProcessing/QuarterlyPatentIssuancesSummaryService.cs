using ParaPlus.Business.Model;

namespace ParaPlus.Business.FileProcessing
{
    public class QuarterlyPatentIssuancesSummaryService : QuarterlySummaryServiceBase<OriginalPatentIssuance>, IQuarterlySummaryService<OriginalPatentIssuance>
    {
        private readonly string _filePath;

        public QuarterlyPatentIssuancesSummaryService(string? filePath = null)
        {
            _filePath = filePath ?? @"C:\Users\chris.sims\Downloads\Quarterly-OnePagers\OriginalPatentIssuances-ByQuarter.csv";
        }

        protected override string GetFilePath() => _filePath;

        protected override string GetReportTitle() => "Quarterly Patent Issuances Summary";

        protected override string GetQuarterValue(OriginalPatentIssuance item) => item.IssueDateFiscalQuarter;

        protected override IEnumerable<OriginalPatentIssuance> ParsePatentData()
        {
            IFileVerifier fileVerifier = new OriginalPatentIssuancesFileVerifier();
            IFileProcessor<OriginalPatentIssuance> fileProcessor = new OriginalPatentIssuancesFileProcessor(fileVerifier);

            IEnumerable<OriginalPatentIssuance> patentIssuances = fileProcessor.ProcessFile(GetFilePath());
            return patentIssuances;
        }
    }
}
