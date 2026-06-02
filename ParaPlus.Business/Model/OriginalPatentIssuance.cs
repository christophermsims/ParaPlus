namespace ParaPlus.Business.Model
{
    public class OriginalPatentIssuance
    {
        public string PatentId { get; set; } = string.Empty;
        public string LawFirmReference { get; set; } = string.Empty;
        public string ApplicationNumber { get; set; } = string.Empty;
        public string PatentNumber { get; set; } = string.Empty;
        public string PatentNumberPlain { get; set; } = string.Empty;
        public string FilingDateFiscalQuarter { get; set; } = string.Empty;
        public string FilingDate { get; set; } = string.Empty;
        public string IssueDateFiscalQuarter { get; set; } = string.Empty;
        public string InHouseCounsel { get; set; } = string.Empty;
        public string LawFirm { get; set; } = string.Empty;
        public string FirmAttorney { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string AllInventors { get; set; } = string.Empty;
        public string AllCategories { get; set; } = string.Empty;
        public string DesignatedProduct { get; set; } = string.Empty;
        public string IssueDate { get; set; } = string.Empty;

        public OriginalPatentIssuance(Dictionary<string, string> row)
        {
            PatentId = row.GetValueOrDefault("Patent: Patent ID", string.Empty);
            LawFirmReference = row.GetValueOrDefault("Law Firm Reference", string.Empty);
            ApplicationNumber = row.GetValueOrDefault("Application Number", string.Empty);
            PatentNumber = row.GetValueOrDefault("Patent Number", string.Empty);
            PatentNumberPlain = row.GetValueOrDefault("Patent Number (plain)", string.Empty);
            FilingDateFiscalQuarter = row.GetValueOrDefault("Filing Date Fiscal Quarter", string.Empty);
            FilingDate = row.GetValueOrDefault("Filing Date", string.Empty);
            IssueDateFiscalQuarter = row.GetValueOrDefault("Issue Date Fiscal Quarter", string.Empty);
            InHouseCounsel = row.GetValueOrDefault("In-House Counsel", string.Empty);
            LawFirm = row.GetValueOrDefault("Law Firm", string.Empty);
            FirmAttorney = row.GetValueOrDefault("Firm Attorney", string.Empty);
            Title = row.GetValueOrDefault("Title", string.Empty);
            AllInventors = row.GetValueOrDefault("All Inventors", string.Empty);
            AllCategories = row.GetValueOrDefault("All Categories", string.Empty);
            DesignatedProduct = row.GetValueOrDefault("Designated Product", string.Empty);
            IssueDate = row.GetValueOrDefault("Issue Date", string.Empty);
        }
    }
}
