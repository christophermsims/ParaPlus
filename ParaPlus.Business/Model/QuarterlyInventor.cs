namespace ParaPlus.Business.Model
{
   public class QuarterlyInventor
    {
        public string EmployeeId { get; set; } = string.Empty;
        public String Name { get; set; } = string.Empty;
        public string OfficeLocation { get; set; } = string.Empty;
        public int CurrentAwardCount { get { return Cubes.Count; } }
        public int NewPlaqueCount { get; set; } = 0;

        public Dictionary<string, string> Cubes { get; set; } = [];

        public QuarterlyInventor(Dictionary<string, string> row)
        {
            EmployeeId = row.GetValueOrDefault("Party: Contact Unique Employee ID Lookup", string.Empty);
            Name = row.GetValueOrDefault("Party: Party Name", string.Empty);

            if (Name.Contains('('))
            {
                Name = Name[..Name.IndexOf('(')].Trim();
            }

            OfficeLocation = row.GetValueOrDefault("Party: Contact Office Location Lookup", string.Empty);

            string patentNumber = row.GetValueOrDefault("Patent Number", string.Empty);

            Cubes.Add($"Cube {Cubes.Count+1}", patentNumber);
        }

        public void AddCube(Dictionary<string, string> row)
        {
            string patentNumber = row.GetValueOrDefault("Patent Number", string.Empty);

            Cubes.Add($"Cube {Cubes.Count+1}", patentNumber);
        }
    }
}