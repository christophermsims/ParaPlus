using Microsoft.VisualBasic.FileIO;
using ParaPlus.Business.Model;

namespace ParaPlus.Business.FileProcessing
{
    public class OnePagerFileProcessor(IFileVerifier verifier) : IFileProcessor<OnePagerDetails>
    {
        IFileVerifier _fileVerifier = verifier;

        public IEnumerable<OnePagerDetails> ProcessFile(string fileToProcess)
        {
            List<OnePagerDetails> onePagerDetails = [];
            TextFieldParser parser = new(fileToProcess)
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = [","]
            };

            string[]? headers = parser.ReadFields();

            if (!_fileVerifier.VerifyHeaders(headers!))
            {
                throw new Exception($"Invalid Headers in File: {fileToProcess}");
            }

            while (!parser.EndOfData)
            {
                string[]? fields = parser.ReadFields();

                if (fields != null)
                {
                    Dictionary<string, string> fieldData = [];

                    for(int i = 0; i < fields.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(headers![i]))
                        {
                            fieldData.Add(headers[i], fields[i]);
                        }
                    }

                    onePagerDetails.Add(new OnePagerDetails(fieldData));
                }
            }
            
            return onePagerDetails;
        }
    }
}