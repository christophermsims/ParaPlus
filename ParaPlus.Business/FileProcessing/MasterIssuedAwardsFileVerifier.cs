namespace ParaPlus.Business.FileProcessing
{
    public class MasterIssuedAwardsFileVerifier : IFileVerifier
    {
        public bool VerifyHeaders(string[]? headers)
        {
            bool result = false;

            if (headers != null)
            {
                List<string> headerList = [.. headers];

                if (headerList.Contains("Inventors"))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}