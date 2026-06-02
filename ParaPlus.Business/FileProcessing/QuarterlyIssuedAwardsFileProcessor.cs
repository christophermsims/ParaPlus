using Microsoft.VisualBasic.FileIO;
using ParaPlus.Business.FileProcessing;

namespace ParaPlus.Business.Model
{
    public class QuarterlyIssuedAwardsFileProcessor(IFileVerifier verifier) : IFileProcessor<QuarterlyInventor>
    {
        IFileVerifier _fileVerifier = verifier;

        public IEnumerable<QuarterlyInventor> ProcessFile(string fileToProcess)
        {
            List<QuarterlyInventor> quarterlyInventors = [];
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

                    string inventorName = fieldData.GetValueOrDefault("Party: Party Name", string.Empty);
                    if (inventorName.Contains('('))
                    {
                        inventorName = inventorName[..inventorName.IndexOf('(')].Trim();
                    }

                    if (quarterlyInventors.Any(i => i.Name == inventorName))
                    {
                        var inventor = quarterlyInventors.First(i => i.Name == inventorName);
                        inventor.AddCube(fieldData);
                    }
                    else
                    {
                        quarterlyInventors.Add(new QuarterlyInventor(fieldData));
                    }
                }
            }
            
            return quarterlyInventors;
        }
    }
}