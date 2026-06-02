using System.Text;
using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Model;

namespace ParaPlus.Console.Jobs
{
    public class IssuedInventorAwardsJob : IJob
    {
        private IEnumerable<MasterInventor>? _masterInventors;
        private IEnumerable<QuarterlyInventor>? _quarterlyInventors;

        public void ExecuteJob()
        {
            _masterInventors = ParseMasterIssuedList();
            _quarterlyInventors = ParseQuarterlyIssuedList();

            ProcessInventorIssuedAwardReports(_masterInventors, _quarterlyInventors);
            WriteQuarterlyOutputFile(_quarterlyInventors);
            WriteMasterOutputFile(_masterInventors);
        }

        private IEnumerable<MasterInventor> ParseMasterIssuedList()
        {
            IFileVerifier fileVerifier = new MasterIssuedAwardsFileVerifier();
            IFileProcessor<MasterInventor> fileProcessor = new MasterIssuedAwardsFileProcessor(fileVerifier);
            String masterFilePath = @"C:\Users\chris.sims\Downloads\Master Issue Awards Spreadsheet.xlsx - Overall.csv";

            IEnumerable<MasterInventor> masterInventors = fileProcessor.ProcessFile(masterFilePath);
            return masterInventors;
        }

        private IEnumerable<QuarterlyInventor> ParseQuarterlyIssuedList()
        {
            IFileVerifier fileVerifier = new QuarterlyIssuedAwardsFileVerifier();
            IFileProcessor<QuarterlyInventor> fileProcessor = new QuarterlyIssuedAwardsFileProcessor(fileVerifier);
            String quarterlyFilePath = @"C:\Users\chris.sims\Downloads\report1775570132865.csv";

            IEnumerable<QuarterlyInventor> quarterlyInventors = fileProcessor.ProcessFile(quarterlyFilePath);
            return quarterlyInventors;
        }

        private void ProcessInventorIssuedAwardReports(IEnumerable<MasterInventor> masterInventors, IEnumerable<QuarterlyInventor> quarterlyInventors)
        {
            foreach (var quarterlyInventor in quarterlyInventors)
            {
                var masterInventor = masterInventors.FirstOrDefault(i => i.Name == quarterlyInventor.Name);

                if (masterInventor != null)
                {
                    foreach (var cube in quarterlyInventor.Cubes)
                    {
                        masterInventor.NewCubes.Add(cube.Key, cube.Value);
                    }

                    masterInventor.NewCubes = quarterlyInventor.Cubes;
                    
                    quarterlyInventor.NewPlaqueCount = masterInventor.NewPlaqueCount;
                }
                else
                {
                    masterInventor = new MasterInventor(new Dictionary<string, string>() { { "Inventors", quarterlyInventor.Name } });

                    foreach (var cube in quarterlyInventor.Cubes)
                    {
                        masterInventor.NewCubes.Add(cube.Key, cube.Value);
                    }

                    quarterlyInventor.NewPlaqueCount = masterInventor.NewPlaqueCount;
                    
                    masterInventors = masterInventors.Append(masterInventor);
                }
            }
        }

        private void WriteQuarterlyOutputFile(IEnumerable<QuarterlyInventor> quarterlyInventors)
        {
            string outputPath = @"C:\Users\chris.sims\Downloads\Quarterly Output.csv";

            using StreamWriter writer = new(outputPath);
            writer.WriteLine($"{quarterlyInventors.Sum(i=>i.NewPlaqueCount)} Bases and {quarterlyInventors.Sum(i=>i.CurrentAwardCount)} Cubes needed");

            int maxCubes = quarterlyInventors.Max(i => i.CurrentAwardCount);

            foreach (var inventor in quarterlyInventors)
            {
                bool plaqueNeeded = inventor.NewPlaqueCount > 0;
                StringBuilder line = new($"{inventor.Name},{inventor.OfficeLocation},{(plaqueNeeded ? "Yes" : "No")},");

                foreach(var cube in inventor.Cubes)
                {
                    line.Append($"\"{cube.Value}\",");
                }

                for(int i = inventor.CurrentAwardCount; i < maxCubes; i++)
                {
                    line.Append(',');
                }

                writer.WriteLine(line.ToString().Trim(','));
            }
        }

        private void WriteMasterOutputFile(IEnumerable<MasterInventor> masterInventors)
        {
            string outputPath = @"C:\Users\chris.sims\Downloads\Master Output.csv";

            using StreamWriter writer = new(outputPath);

            int maxCubes = masterInventors.Max(i => i.CurrentAwardCount + i.NewAwardCount);

            StringBuilder header = new("Inventors,Plaques,");
            header.Append(string.Join(',', Enumerable.Range(1, maxCubes).Select(i => $"Cube {i}")));
            writer.WriteLine(header.ToString());

            foreach (var inventor in masterInventors.OrderBy(i => i.Name))
            {
                StringBuilder line = new($"{inventor.Name},{inventor.CurrentPlaqueCount + inventor.NewPlaqueCount},");

                foreach(var cube in inventor.Cubes)
                {
                    line.Append($"\"{cube.Value}\",");
                }

                foreach(var cube in inventor.NewCubes)
                {
                    line.Append($"\"{cube.Value}\",");
                }

                for(int i = inventor.CurrentAwardCount + inventor.NewAwardCount; i < maxCubes; i++)
                {
                    line.Append(',');
                }

                writer.WriteLine(line.ToString().Trim(','));
            }
        }
    }
}