using System.Collections.Generic;
using System.Linq;

namespace ParaPlus.Business.FileProcessing
{
    public class ChineseInventorFileVerifier : IFileVerifier
    {
        private readonly string[] _requiredHeaders = {
            "Inventor Name",
            "Office Location",
            "Office Address",
            "Phone Number",
            "Email Address",
            "Base Needed"
        };

        public bool VerifyHeaders(string[]? headers)
        {
            if (headers == null || !headers.Any()) return false;

            List<string> headerList = headers.Select(h => h.Trim()).ToList();
            return !_requiredHeaders.Except(headerList, System.StringComparer.OrdinalIgnoreCase).Any();
        }
    }
}