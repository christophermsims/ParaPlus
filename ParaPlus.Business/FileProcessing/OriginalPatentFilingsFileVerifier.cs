namespace ParaPlus.Business.FileProcessing
{
    public class OriginalPatentFilingsFileVerifier : IFileVerifier
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
                    headerList.Contains("Filing Date Fiscal Quarter"))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
