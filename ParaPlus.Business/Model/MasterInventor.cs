namespace ParaPlus.Business.Model
{
   public class MasterInventor
    {
        public string Name { get; set; } = string.Empty;
        public string EmployeeID { get; set; } = string.Empty;
		public int CurrentAwardCount { get { return Cubes.Count; } }
        public int CurrentPlaqueCount { get { return (CurrentAwardCount + 7) / 8; } }

        public Dictionary<string, string> Cubes { get; set; } = [];
        public Dictionary<string, string> NewCubes { get; set; } = [];

        public int NewAwardCount { get{ return NewCubes.Count; } }
        public int NewPlaqueCount { get { return ((CurrentAwardCount + NewAwardCount + 7) / 8) - CurrentPlaqueCount; } }

        public MasterInventor(Dictionary<string, string> row)
        {
            Name = row.GetValueOrDefault("Inventors", string.Empty);

            if (Name.Contains('('))
            {
                Name = Name[..Name.IndexOf('(')].Trim();
            }

            EmployeeID = row.GetValueOrDefault("Employee ID", string.Empty);

			foreach (var key in row.Keys)
            {
                if (key.Contains("Cube") && !String.IsNullOrEmpty(row[key]))
                {
                    Cubes.Add(key, row[key]);
                }
            }
        }
    }
}