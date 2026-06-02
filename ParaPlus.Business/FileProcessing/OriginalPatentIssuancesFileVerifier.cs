namespace ParaPlus.Business.FileProcessing
{
    public class OriginalPatentIssuancesFileVerifier : IFileVerifier
    {
        public bool VerifyHeaders(string[]? headers)
        {
            bool result = false;

            if (headers != null)
            {
                List<string> headerList = [.. headers];

                // Check for required headers
                if (headerList.Contains("Patent: Patent ID") && 
                    headerList.Contains("Law Firm Reference") &&
                    headerList.Contains("Application Number") &&
                    headerList.Contains("Patent Number") &&
                    headerList.Contains("Issue Date Fiscal Quarter"))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
