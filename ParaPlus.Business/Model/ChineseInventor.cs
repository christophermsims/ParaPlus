using System.Collections.Generic;
using System.Linq;

namespace ParaPlus.Business.Model
{
    public class ChineseInventor
    {
        public string Name { get; set; }
        public string OfficeLocation { get; set; }
        public string OfficeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string BaseNeeded { get; set; } // "Yes" or "No"

        // New address properties
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; } // No zip code in samples, initialize to empty
        public string Country { get; set; }
        public Dictionary<string, string> Cubes { get; set; } = new Dictionary<string, string>();

        public ChineseInventor(Dictionary<string, string> fieldData)
        {
            Name = fieldData.GetValueOrDefault("Inventor Name", string.Empty);
            OfficeLocation = fieldData.GetValueOrDefault("Office Location", string.Empty);
            OfficeAddress = fieldData.GetValueOrDefault("Office Address", string.Empty);
            PhoneNumber = fieldData.GetValueOrDefault("Phone Number", string.Empty);
            EmailAddress = fieldData.GetValueOrDefault("Email Address", string.Empty);
            BaseNeeded = fieldData.GetValueOrDefault("Base Needed", string.Empty);

            // Extract Cube data, assuming keys start with "Cube "
            foreach (var entry in fieldData.Where(e => e.Key.StartsWith("Cube ") && !string.IsNullOrWhiteSpace(e.Value)))
            {
                Cubes.Add(entry.Key, entry.Value);
            }

            // Parse the OfficeAddress into the new granular fields
            ParseChineseOfficeAddress(OfficeAddress);
        }

        private void ParseChineseOfficeAddress(string fullAddress)
        {
            // Initialize to empty
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;
            Country = string.Empty;

            if (string.IsNullOrWhiteSpace(fullAddress))
            {
                return;
            }

            // Split by comma, then trim and filter out empty entries
            var parts = fullAddress.Split(',')
                                   .Select(p => p.Trim())
                                   .Where(p => !string.IsNullOrWhiteSpace(p))
                                   .ToList();

            if (parts.Count == 0) return;

            // Process from the end based on the sample format: Address Lines, City, Province, Country
            if (parts.Count >= 1) { Country = parts.Last(); parts.RemoveAt(parts.Count - 1); }
            if (parts.Count >= 1) { State = parts.Last(); parts.RemoveAt(parts.Count - 1); }
            if (parts.Count >= 1) { City = parts.Last(); parts.RemoveAt(parts.Count - 1); }

            // Remaining parts are address lines
            if (parts.Count > 0)
            {
                AddressLine1 = parts[0];
                if (parts.Count > 1)
                {
                    AddressLine2 = string.Join(", ", parts.Skip(1));
                }
            }
            // ZipCode is not present in the samples, so it remains empty.
        }
    }
}