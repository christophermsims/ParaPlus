namespace ParaPlus.Business.FileProcessing
{
    public class QuarterlyIssuedAwardsFileVerifier : IFileVerifier
    {
        public bool VerifyHeaders(string[]? headers)
        {
            bool result = false;

            if (headers != null)
            {
                List<string> headerList = [.. headers];

                if (headerList.Contains("Party: Party Name")
                    && headerList.Contains("Party: Contact Unique Employee ID Lookup")
                    && headerList.Contains("Patent Number"))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}