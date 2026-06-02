using System.Collections.Generic;

namespace ParaPlus.Business.FileProcessing
{
    public class InventorAddressFileVerifier : IFileVerifier
    {
        public bool VerifyHeaders(string[]? headers)
        {
            if (headers == null)
            {
                return false;
            }

            List<string> headerList = [.. headers];

            // Check for required headers
            return headerList.Contains("Employee ID") &&
                   headerList.Contains("First Name") &&
                   headerList.Contains("Last Name") &&
                   headerList.Contains("Address");
        }
    }
}