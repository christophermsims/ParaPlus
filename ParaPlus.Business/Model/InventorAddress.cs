using System.Collections.Generic;
using ParaPlus.Business.Helper;

namespace ParaPlus.Business.Model
{
    public class InventorAddress
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CurrentlyEmployed { get; set; }
        public string Citizenship { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string EmailHome { get; set; }
        public string EmailWork { get; set; }
        public string OfficeLocation { get; set; }
        public string ExecutiveAndAbove { get; set; }
        public string EmployeeType { get; set; }
        public string ContingentWorkerType { get; set; }
        public string JobTitle { get; set; }
        public string JobProfile { get; set; }
        public string JobDescription { get; set; }
        public string Manager { get; set; }
        public string Department { get; set; }
        public string CostCenter { get; set; }
        public string Company { get; set; }

        public InventorAddress(Dictionary<string, string> fieldData)
        {
            EmployeeId = fieldData.GetValueOrDefault("Employee ID", string.Empty);
            FirstName = fieldData.GetValueOrDefault("First Name", string.Empty);
            MiddleName = fieldData.GetValueOrDefault("Middle Name", string.Empty);
            LastName = fieldData.GetValueOrDefault("Last Name", string.Empty);
            PreferredName = fieldData.GetValueOrDefault("Preferred Name", string.Empty);
            StartDate = fieldData.GetValueOrDefault("Start Date", string.Empty);
            EndDate = fieldData.GetValueOrDefault("End Date", string.Empty);
            CurrentlyEmployed = fieldData.GetValueOrDefault("Currently Employed", string.Empty);
            Citizenship = fieldData.GetValueOrDefault("Citizenship", string.Empty);
            OfficeLocation = fieldData.GetValueOrDefault("Office Location", string.Empty); // Ensure OfficeLocation is set before parsing address
            ParseHomeAddress(fieldData.GetValueOrDefault("Address", string.Empty));
            EmailHome = fieldData.GetValueOrDefault("Email - Home", string.Empty);
            EmailWork = fieldData.GetValueOrDefault("Email - Work", string.Empty);
            ExecutiveAndAbove = fieldData.GetValueOrDefault("Executive and Above", string.Empty);
            EmployeeType = fieldData.GetValueOrDefault("Employee Type", string.Empty);
            ContingentWorkerType = fieldData.GetValueOrDefault("Contingent Worker Type", string.Empty);
            JobTitle = fieldData.GetValueOrDefault("Job Title", string.Empty);
            JobProfile = fieldData.GetValueOrDefault("Job Profile", string.Empty);
            JobDescription = fieldData.GetValueOrDefault("Job Description", string.Empty);
            Manager = fieldData.GetValueOrDefault("Manager", string.Empty);
            Department = fieldData.GetValueOrDefault("Department", string.Empty);
            CostCenter = fieldData.GetValueOrDefault("Cost Center", string.Empty);
            Company = fieldData.GetValueOrDefault("Company", string.Empty);
        }

        private string ExtractCountryCodeFromOfficeLocation(string officeLocation)
        {
            if (string.IsNullOrWhiteSpace(officeLocation))
            {
                return string.Empty;
            }

            int startIndex = officeLocation.LastIndexOf('(');
            int endIndex = officeLocation.LastIndexOf(')');

            if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
            {
                string codeInParenthesis = officeLocation.Substring(startIndex + 1, endIndex - startIndex - 1).Trim().ToUpperInvariant();

                // Handle regional codes like CAN-ON, IND-Delhi by taking the primary country code
                if (codeInParenthesis.Contains('-'))
                {
                    codeInParenthesis = codeInParenthesis.Split('-')[0];
                }

                // Special case for "US" if it appears, map it to "USA" for consistency with ISO3
                if (codeInParenthesis.Length == 2)
                {
                    return "USA";
                }
                
                // If it's a known ISO3 code or special case like "DUBAI", return it.
                if (CountryCodes.Codes.ContainsValue(codeInParenthesis) || codeInParenthesis == "DUBAI")
                {
                    return codeInParenthesis;
                }
            }
            return string.Empty; // No recognizable country code found
        }

        private void ParseHomeAddress(string fullAddress)
        {
            // Initialize properties to ensure they are clean before parsing.
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;
            Country = string.Empty; // Will be set by specific parsing methods

            if (string.IsNullOrWhiteSpace(fullAddress))
            {
                return;
            }

            string countryCode = ExtractCountryCodeFromOfficeLocation(OfficeLocation);

            if (countryCode == "CHN")
            {
                ParseChineseHomeAddress(fullAddress);
                Country = "China"; // Set country explicitly for Chinese addresses";
            }
            else if (countryCode == "USA")
            {
                ParseUSHomeAddress(fullAddress);
                Country = "United States"; // Set country explicitly for US addresses
            }
            else if (!string.IsNullOrEmpty(countryCode)) // Other international addresses
            {
                ParseInternationalHomeAddress(fullAddress, countryCode);
                // Set the full country name from the helper, or use the code if not found.
                Country = CountryCodes.Codes.FirstOrDefault(x => x.Value == countryCode).Key ?? countryCode;
            }
            else
            {
                // Fallback if no country code or unknown code, treat as US default
                ParseUSHomeAddress(fullAddress);
                Country = "United States";
            }
        }

        private void ParseUSHomeAddress(string fullAddress)
        {
            // This parsing logic is based on common US address formats.
            // It assumes address components are separated by newlines, from specific to general (bottom-up).
            var lines = fullAddress.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(line => line.Trim())
                                   .Where(line => !string.IsNullOrWhiteSpace(line))
                                   .ToList();

            if (lines.Count == 0) return;

            // Assumption: Last line is country, if it doesn't contain digits and there are multiple lines.
            // This part might be removed if Country is always set from OfficeLocation.
            if (lines.Count > 1 && !lines.Last().Any(char.IsDigit))
            {
                // If the country is explicitly in the address, use it. Otherwise, rely on OfficeLocation.
                // For US addresses, we assume Country is "United States" from OfficeLocation.
                // Country = lines.Last();
                // lines.RemoveAt(lines.Count - 1);
            }

            // Assumption: The new last line is for city, state, and zip.
            if (lines.Count > 0)
            {
                var cityLine = lines.Last();
                lines.RemoveAt(lines.Count - 1);

                var cityParts = cityLine.Split(new[] { ',' }, 2).Select(p => p.Trim()).ToList();
                City = cityParts.FirstOrDefault() ?? string.Empty;

                if (cityParts.Count > 1)
                {
                    var stateZipPart = cityParts[1];
                    var lastSpace = stateZipPart.LastIndexOf(' ');
                    if (lastSpace > 0)
                    {
                        State = stateZipPart.Substring(0, lastSpace).Trim();
                        ZipCode = stateZipPart.Substring(lastSpace + 1).Trim();
                    }
                    else
                    {
                        State = stateZipPart; // No zip found
                    }
                }
            }

            // Assumption: Remaining lines are address lines 1 and 2.
            if (lines.Count > 0)
            {
                AddressLine1 = lines[0];
            }
            if (lines.Count > 1)
            {
                AddressLine2 = string.Join(", ", lines.Skip(1));
            }

            // If AddressLine1 contains a comma, split it into AddressLine2.
            if (!string.IsNullOrEmpty(AddressLine1) && AddressLine1.Contains(','))
            {
                var address1Parts = AddressLine1.Split(new[] { ',' }, 2);
                AddressLine1 = address1Parts[0].Trim();
                string address1Remainder = address1Parts.Length > 1 ? address1Parts[1].Trim() : string.Empty;

                if (!string.IsNullOrEmpty(address1Remainder))
                {
                    if (!string.IsNullOrEmpty(AddressLine2))
                    {
                        AddressLine2 = $"{address1Remainder}, {AddressLine2}";
                    }
                    else
                    {
                        AddressLine2 = address1Remainder;
                    }
                }
            }
        }

        private void ParseChineseHomeAddress(string fullAddress)
        {
            // Parsing logic for Chinese addresses based on provided samples.
            // Assumes newline-separated components, processed from bottom-up:
            // ... Address Line(s)
            // ... City
            // ... ZipCode State
            var lines = fullAddress.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(line => line.Trim())
                                   .Where(line => !string.IsNullOrWhiteSpace(line))
                                   .ToList();

            if (lines.Count == 0) return;

            // Process State and Zip from the last line
            if (lines.Count > 0)
            {
                var cityLine = lines.Last();
                lines.RemoveAt(lines.Count - 1);
                // Use regex to find a 6-digit postal code
                var zipMatch = System.Text.RegularExpressions.Regex.Match(cityLine, @"\b\d{6}\b");
                if (zipMatch.Success)
                {
                    ZipCode = zipMatch.Value;
                    State = cityLine.Replace(ZipCode, "").Trim();
                }
                else
                {
                    State = cityLine; // Assume the line is just the state if no zip
                }
            }

            // Process City from the new last line
            if (lines.Count > 0)
            {
                City = lines.Last();
                lines.RemoveAt(lines.Count - 1);
            }

            // The rest is address lines
            if (lines.Count > 0)
            {
                AddressLine1 = lines[0];
            }
            if (lines.Count > 1)
            {
                AddressLine2 = string.Join(", ", lines.Skip(1));
            }
        }

        private void ParseInternationalHomeAddress(string fullAddress, string countryCode)
        {
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;

            var lines = fullAddress.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(line => line.Trim())
                                   .Where(line => !string.IsNullOrWhiteSpace(line))
                                   .ToList();

            if (lines.Count == 0) return;

            lines.Reverse(); // Process from bottom up

            // Specific handling for India
            if (countryCode == "IND")
            {
                // Try to get State from the very last line if it's just a state name
                if (lines.Count > 0 && !lines[0].Any(char.IsDigit) && lines[0].Length < 30) // Heuristic for state name
                {
                    State = lines[0];
                    lines.RemoveAt(0);
                }

                // Now process the new last line (which might be City-ZipCode or just City/ZipCode)
                if (lines.Count > 0)
                {
                    string currentLine = lines[0];
                    lines.RemoveAt(0);

                    System.Text.RegularExpressions.Match zipMatch = System.Text.RegularExpressions.Regex.Match(currentLine, @"\b\d{6}\b");
                    if (zipMatch.Success)
                    {
                        ZipCode = zipMatch.Value.Trim();
                        currentLine = System.Text.RegularExpressions.Regex.Replace(currentLine, System.Text.RegularExpressions.Regex.Escape(ZipCode), "").Trim();
                    }

                    // Remove any trailing hyphens or "City-" patterns
                    currentLine = currentLine.TrimEnd('-').Trim();

                    // If State is still empty, try to extract it from currentLine if it contains a comma
                    if (string.IsNullOrWhiteSpace(State) && currentLine.Contains(','))
                    {
                        var parts = currentLine.Split(new[] { ',' }, StringSplitOptions.TrimEntries);
                        if (parts.Length > 1)
                        {
                            City = parts[0];
                            State = parts[1];
                        }
                        else
                        {
                            City = currentLine;
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(City))
                    {
                        City = currentLine;
                    }
                }
            }
            else // Generic international parsing for other countries
            {
                if (lines.Count > 0)
                {
                    string currentLine = lines[0];
                    lines.RemoveAt(0);

                    // Attempt to extract ZipCode using a more comprehensive regex
                    // This regex tries to cover various formats:
                    // - 5 digits (e.g., DEU, USA)
                    // - 6 digits (e.g., IND)
                    // - 5 digits with hyphen (e.g., BRA)
                    // - Alphanumeric with space (e.g., CAN: A1A 4T7, NLD: 1016TG)
                    // - 5 digits followed by 2 letters (e.g., ITA: 20096 MI)
                    // - 7 digits (e.g., ISR)
                    System.Text.RegularExpressions.Match zipMatch = System.Text.RegularExpressions.Regex.Match(currentLine,
                        @"\b(\d{5}(?:-\d{4})?|\d{6}|\d{5}(?:\s?[A-Z]{2})?|[A-Z]\d[A-Z]\s?\d[A-Z]\d|\d{4}\s?[A-Z]{2}|\d{7})\b",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    if (zipMatch.Success)
                    {
                        ZipCode = zipMatch.Value.Trim();
                        currentLine = System.Text.RegularExpressions.Regex.Replace(currentLine, System.Text.RegularExpressions.Regex.Escape(ZipCode), "").Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(currentLine))
                    {
                        var parts = currentLine.Split(new[] { ',' }, StringSplitOptions.TrimEntries);
                        if (parts.Length > 1)
                        {
                            City = parts[0];
                            State = parts[1];
                        }
                        else
                        {
                            var spaceParts = currentLine.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                            if (spaceParts.Length > 1 && spaceParts.Last().Length <= 3) // Heuristic: state codes are usually short
                            {
                                City = spaceParts[0];
                                State = spaceParts[1];
                            }
                            else
                            {
                                City = currentLine;
                            }
                        }
                    }
                }

                // If City/State are still empty, check the next line
                if (lines.Count > 0 && string.IsNullOrWhiteSpace(City) && string.IsNullOrWhiteSpace(State))
                {
                    string nextLine = lines[0];
                    lines.RemoveAt(0);

                    if (!nextLine.Any(char.IsDigit) && nextLine.Length < 20) // Heuristic for state name
                    {
                        State = nextLine;
                    }
                    else
                    {
                        City = nextLine;
                    }
                }
            }

            // The remaining lines are AddressLine1 and AddressLine2
            lines.Reverse();

            if (lines.Count > 0)
            {
                AddressLine1 = lines[0];
            }
            if (lines.Count > 1)
            {
                AddressLine2 = string.Join(", ", lines.Skip(1));
            }

            // Special handling for Dubai, as it's often just "Dubai" as the city/country.
            if (countryCode == "DUBAI" && string.IsNullOrWhiteSpace(City))
            {
                City = "Dubai";
            }
        }
    }
}