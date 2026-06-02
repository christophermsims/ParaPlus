using Microsoft.VisualBasic.FileIO;
using ParaPlus.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParaPlus.Business.FileProcessing
{
    public class ChineseInventorFileProcessor(IFileVerifier verifier) : IFileProcessor<ChineseInventor>
    {
        private readonly IFileVerifier _fileVerifier = verifier;

        public IEnumerable<ChineseInventor> ProcessFile(string fileToProcess)
        {
            List<ChineseInventor> chineseInventors = [];
            using (TextFieldParser parser = new(fileToProcess))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.Delimiters = [","];
                parser.HasFieldsEnclosedInQuotes = true;

                // The provided sample CSV "Chinese Inventors - Q4FY26.csv" has headers on the first line.
                // Unlike some other processors in ParaPlus.Business that skip the first line (assuming a report title),
                // this processor reads the first line as headers to correctly parse the sample.
                string[]? headers = parser.ReadFields();

                if (headers == null || !_fileVerifier.VerifyHeaders(headers))
                {
                    throw new Exception($"Invalid or missing headers in file: {fileToProcess}");
                }

                while (!parser.EndOfData)
                {
                    string[]? fields = parser.ReadFields();

                    if (fields != null)
                    {
                        var fieldData = new Dictionary<string, string>();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            if (i < fields.Length && !string.IsNullOrEmpty(headers[i]))
                            {
                                fieldData.Add(headers[i], fields[i]);
                            }
                        }
                        chineseInventors.Add(new ChineseInventor(fieldData));
                    }
                }
            }
            return chineseInventors;
        }
    }
}