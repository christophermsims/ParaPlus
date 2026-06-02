namespace ParaPlus.Business.FileProcessing
{
    public class OnePagerFileVerifier : IFileVerifier
    {
        public bool VerifyHeaders(string[]? headers)
        {
            bool result = false;

            if (headers != null)
            {
                List<string> headerList = [.. headers];

                if (headerList.Contains("Patent: Patent ID")
                 && headerList.Contains("Law Firm Reference")
                 && headerList.Contains("Title")
                 && headerList.Contains("General Subject Matter")
                 && headerList.Contains("Illustrative Use Case")
                 && headerList.Contains("Front Page Image")
                 && headerList.Contains("Filing Date")
                 && headerList.Contains("Product")
                 && headerList.Contains("Technology")
                 && headerList.Contains("All Inventors")
                 && headerList.Contains("Designated Product")
                 && headerList.Contains("Filing Date Fiscal Quarter"))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}