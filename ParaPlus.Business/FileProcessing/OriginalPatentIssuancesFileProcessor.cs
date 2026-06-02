using Microsoft.VisualBasic.FileIO;
using ParaPlus.Business.Model;
using ParaPlus.Business.FileProcessing;

namespace ParaPlus.Business.FileProcessing
{
    public class OriginalPatentIssuancesFileProcessor(IFileVerifier verifier) : IFileProcessor<OriginalPatentIssuance>
    {
        IFileVerifier _fileVerifier = verifier;

        public IEnumerable<OriginalPatentIssuance> ProcessFile(string fileToProcess)
        {
            List<OriginalPatentIssuance> patentIssuances = [];
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

                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(headers![i]))
                        {
                            fieldData.Add(headers[i], fields[i]);
                        }
                    }

                    patentIssuances.Add(new OriginalPatentIssuance(fieldData));
                }
            }

            return patentIssuances;
        }
    }
}
