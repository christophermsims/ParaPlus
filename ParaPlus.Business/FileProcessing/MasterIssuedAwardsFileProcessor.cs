using Microsoft.VisualBasic.FileIO;
using ParaPlus.Business.FileProcessing;

namespace ParaPlus.Business.Model
{
    public class MasterIssuedAwardsFileProcessor(IFileVerifier verifier) : IFileProcessor<MasterInventor>
    {
        IFileVerifier _fileVerifier = verifier;

        public IEnumerable<MasterInventor> ProcessFile(string fileToProcess)
        {
            List<MasterInventor> masterInventors = [];
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

                    masterInventors.Add(new MasterInventor(fieldData));
                }
            }
            
            return masterInventors;
        }
    }
}