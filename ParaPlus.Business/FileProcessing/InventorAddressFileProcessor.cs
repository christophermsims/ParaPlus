using Microsoft.VisualBasic.FileIO;
using ParaPlus.Business.Model;
using System;
using System.Collections.Generic;

namespace ParaPlus.Business.FileProcessing
{
    public class InventorAddressFileProcessor(IFileVerifier verifier) : IFileProcessor<InventorAddress>
    {
        private readonly IFileVerifier _fileVerifier = verifier;

        public IEnumerable<InventorAddress> ProcessFile(string fileToProcess)
        {
            List<InventorAddress> inventorAddresses = [];
            using (TextFieldParser parser = new(fileToProcess))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.Delimiters = [","];
                parser.HasFieldsEnclosedInQuotes = true;
                
                parser.ReadLine();//skip first line.

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
                        inventorAddresses.Add(new InventorAddress(fieldData));
                    }
                }
            }
            return inventorAddresses;
        }
    }
}