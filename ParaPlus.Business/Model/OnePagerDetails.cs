using System.Diagnostics.Contracts;

namespace ParaPlus.Business.Model
{
    public class OnePagerDetails
    {
        public string ZOOM_REF {get;set;}
        public string LAWFIRM_REF {get;set;}
        public string Title {get;set;}
        public string GeneralSubjectMatter {get; set;}
        public string IllustrativeUseCase {get;set;}
        public string FrontPageImage {get;set;}
        public string FilingDate {get;set;}
        public string Product {get;set;}
        public string Technology {get;set;}
        public string AllInventors {get;set;}
        public string DesignatedProduct {get;set;}
        public string FilingDateFiscalQuarter {get;set;}

        public OnePagerDetails(Dictionary<string, string> row)
        {
            ZOOM_REF = row["Patent: Patent ID"];
            LAWFIRM_REF = row["Law Firm Reference"];
            Title = row["Title"];
            GeneralSubjectMatter = row["General Subject Matter"];
            IllustrativeUseCase = row["Illustrative Use Case"];
            FilingDate = row["Filing Date"];
            Product = row["Product"];
            Technology = row["Technology"];
            AllInventors = row["All Inventors"];
            DesignatedProduct = row["Designated Product"];
            FilingDateFiscalQuarter = row["Filing Date Fiscal Quarter"];
            FrontPageImage = row["Front Page Image"];
        }
    }
}
