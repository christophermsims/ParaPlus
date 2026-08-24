using System.Runtime.CompilerServices;
using System.Text;
using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Helper;
using ParaPlus.Business.Model;

namespace ParaPlus.Business.Jobs
{
    public class IssuedInventorAwardsJob(
        IFileProcessor<QuarterlyInventor> quarterlyIssuedAwardsFileProcessor, 
        IFileProcessor<MasterInventor> masterFileProcessor,
		IFileProcessor<InventorAddress> inventorAddressFileProcessor,
		Action<string>? reportAction = null) : IJob
    {
        private IEnumerable<MasterInventor>? _masterInventors;
        private IEnumerable<QuarterlyInventor>? _quarterlyInventors;
		private IEnumerable<InventorAddress>? _inventorAddresses;

		private readonly IFileProcessor<QuarterlyInventor> _quarterlyIssuedAwardsFileProcessor = quarterlyIssuedAwardsFileProcessor;
		private readonly IFileProcessor<MasterInventor> _masterFileProcessor = masterFileProcessor;
		private readonly IFileProcessor<InventorAddress> _inventorAddressFileProcessor = inventorAddressFileProcessor;

		private readonly Action<string> _reporter = reportAction ?? Console.WriteLine;

		private string _quarterlyFilePath = @"";
		private string _masterFilePath = @"";
		private string _inventorAddressFilePath = @"";
        private string _outputFolder = @"";
		private string _quarterlyOutputPath = @"";
		private string _quarterlyChineseInventorOutputPath = @"";
		private string _masterOutputPath = @"";
		private string _vendorDomesticOutputPath = @"";
		private string _vendorInternationalOutputPath = @"";

		public string QuarterlyFilePath
		{
			get => _quarterlyFilePath;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					_reporter("Warning: Quarterly file path is empty. Retaining previous value.");
					return;
				}
				if (!File.Exists(value))
				{
					_reporter($"Warning: File '{value}' does not exist. Retaining previous value.");
					return;
				}
				_quarterlyFilePath = value;
			}
		}

		public string MasterFilePath
		{
			get => _masterFilePath;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					_reporter("Warning: Master file path is empty. Retaining previous value.");
					return;
				}
				if (!File.Exists(value))
				{
					_reporter($"Warning: File '{value}' does not exist. Retaining previous value.");
					return;
				}
				_masterFilePath = value;
			}
		}

		public string InventorAddressFilePath
		{
			get => _inventorAddressFilePath;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					_reporter("Warning: Inventor address file path is empty. Retaining previous value.");
					return;
				}
				if (!File.Exists(value))
				{
					_reporter($"Warning: File '{value}' does not exist. Retaining previous value.");
					return;
				}
				_inventorAddressFilePath = value;
			}
		}

		public string OutputFolder
		{
			get => _outputFolder;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					_reporter("Warning: Output folder path is empty. Retaining previous value.");
					return;
				}
				if (!Directory.Exists(value))
				{
					_reporter($"Warning: Folder '{value}' does not exist. Retaining previous value.");
					return;
				}

				_quarterlyOutputPath = Path.Combine(value, OutputFiles.QuarterlyIssuedAwardsOutputFile);
				_quarterlyChineseInventorOutputPath = Path.Combine(value, OutputFiles.QuarterlyChineseAwardIssuedAwardsOutputFile);
				_masterOutputPath = Path.Combine(value, OutputFiles.MasterInventorWardsOutputFile);
				_vendorDomesticOutputPath = Path.Combine(value, OutputFiles.VendorDomesticOutputFile);
				_vendorInternationalOutputPath = Path.Combine(value, OutputFiles.VendorInternationalOutputFile);
				_outputFolder = value;
			}
		}

		public void ExecuteJob()
        {
            _masterInventors = ParseMasterIssuedList();
            _quarterlyInventors = ParseQuarterlyIssuedList();
			_inventorAddresses = ParseInventorAddressList();

			_reporter($"Total Master Inventors: {_masterInventors.Count()}");
            _masterInventors = ProcessInventorIssuedAwardReports(_masterInventors, _quarterlyInventors);
			_reporter($"Total Master Inventors: {_masterInventors.Count()}");

            WriteQuarterlyOutputFile(_quarterlyInventors);
			WriteChineseInventorOutputFile(_quarterlyInventors);
			WriteMasterOutputFile(_masterInventors);
			WriteDomesticVendorInventorAwardFile(_quarterlyInventors, _inventorAddresses);
			WriteInternationalVendorInventorAwardFile(_quarterlyInventors, _inventorAddresses);
		}

        private IEnumerable<MasterInventor> ParseMasterIssuedList()
        {
            _reporter("Parsing master issued awards list...");
			IEnumerable<MasterInventor> masterInventors = _masterFileProcessor.ProcessFile(_masterFilePath);
			_reporter("Master issued awards list parsing complete.");

			return masterInventors;
		}

        private IEnumerable<QuarterlyInventor> ParseQuarterlyIssuedList()
        {
            _reporter("Parsing quarterly issued awards list...");
			IEnumerable<QuarterlyInventor> quarterlyInventors = _quarterlyIssuedAwardsFileProcessor.ProcessFile(_quarterlyFilePath);
            _reporter("Quarterly issued awards list parsing complete.");
			return quarterlyInventors;
        }

		private IEnumerable<InventorAddress> ParseInventorAddressList()
		{
			_reporter("Parsing inventor address list...");
			IEnumerable<InventorAddress> inventorAddresses = _inventorAddressFileProcessor.ProcessFile(_inventorAddressFilePath);
			_reporter("Inventor address list parsing complete.");
			return inventorAddresses;
		}

        private IEnumerable<MasterInventor> ProcessInventorIssuedAwardReports(IEnumerable<MasterInventor> masterInventors, IEnumerable<QuarterlyInventor> quarterlyInventors)
        {
            _reporter("Processing inventor issued awards reports...");

			foreach (var quarterlyInventor in quarterlyInventors)
            {
                var masterInventor = masterInventors.FirstOrDefault(i => i.EmployeeID == quarterlyInventor.EmployeeId);

                if (masterInventor != null)
                {
                    foreach (var cube in quarterlyInventor.Cubes)
                    {
                        masterInventor.NewCubes.Add(cube.Key, cube.Value);
                    }
                    
                    quarterlyInventor.NewPlaqueCount = masterInventor.NewPlaqueCount;
                }
                else
                {
					_reporter($"Inventor Not Found {quarterlyInventor.Name}, Employee ID: {quarterlyInventor.EmployeeId}");

                    masterInventor = new MasterInventor(new Dictionary<string, string>() { 
						{ "Inventors", quarterlyInventor.Name }, 
						{ "Employee ID", quarterlyInventor.EmployeeId } });

                    foreach (var cube in quarterlyInventor.Cubes)
                    {
                        masterInventor.NewCubes.Add(cube.Key, cube.Value);
                    }

                    quarterlyInventor.NewPlaqueCount = masterInventor.NewPlaqueCount;
                    
					_reporter($"Adding new Master Inventor: {masterInventor.Name}, Employee ID: {masterInventor.EmployeeID}");
                    masterInventors = masterInventors.Append(masterInventor);
					_reporter($"Total Master Inventors: {masterInventors.Count()}");
                }
            }

            _reporter("Inventor issued awards reports processing complete.");
			return masterInventors;
		}

        private void WriteQuarterlyOutputFile(IEnumerable<QuarterlyInventor> quarterlyInventors)
        {
            _reporter("Writing quarterly output file...");

			using StreamWriter writer = new(_quarterlyOutputPath);
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

            _reporter("Quarterly output file writing complete.");
		}

		private void WriteChineseInventorOutputFile(IEnumerable<QuarterlyInventor> quarterlyInventors)
		{
			_reporter("Writing quarterly chinese inventor output file...");

			using StreamWriter writer = new(_quarterlyChineseInventorOutputPath);
			StringBuilder header = new("Inventor Name,Office Location,Office Address,Phone Number,Email Address,Base Needed");
			var chineseInventors = quarterlyInventors.Where(i => i.OfficeLocation.Contains("(CHN)"));

			int maxCubes = chineseInventors.Max(i => i.CurrentAwardCount);

			for (int i = 0; i < maxCubes; i++)
			{
				header.Append(", Cube " + (i + 1));
			}

			writer.WriteLine(header.ToString());

			foreach (var inventor in chineseInventors)
			{
				bool plaqueNeeded = inventor.NewPlaqueCount > 0;
				StringBuilder line = new($"{inventor.Name},{inventor.OfficeLocation},,,,{(plaqueNeeded ? "Yes" : "No")},");

				foreach (var cube in inventor.Cubes)
				{
					line.Append($"\"{cube.Value}\",");
				}

				for (int i = inventor.CurrentAwardCount; i < maxCubes; i++)
				{
					line.Append(',');
				}

				writer.WriteLine(line.ToString().Trim(','));
			}

			_reporter("Quarterly chinese inventor output file writing complete.");
		}

		private void WriteMasterOutputFile(IEnumerable<MasterInventor> masterInventors)
        {
            _reporter("Writing master output file...");

			using StreamWriter writer = new(_masterOutputPath);

            int maxCubes = masterInventors.Max(i => i.CurrentAwardCount + i.NewAwardCount);

            StringBuilder header = new("Inventors,Employee ID,Plaques,");
            header.Append(string.Join(',', Enumerable.Range(1, maxCubes).Select(i => $"Cube {i}")));
            writer.WriteLine(header.ToString());

            foreach (var inventor in masterInventors.OrderBy(i => i.Name))
            {
                StringBuilder line = new($"{inventor.Name},{inventor.EmployeeID},{inventor.CurrentPlaqueCount + inventor.NewPlaqueCount},");

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

            _reporter("Master output file writing complete.");
		}

		private void WriteDomesticVendorInventorAwardFile(IEnumerable<QuarterlyInventor> quarterlyInventors, IEnumerable<InventorAddress> inventorAddresses)
		{
			_reporter("Writing vendor domestic inventor award file...");

			using StreamWriter writer = new(_vendorDomesticOutputPath);
			writer.WriteLine("Name,Address Line 1,Address Line 2,City,State,Zip,Country,Patent Number,Plaque,Cube");

			var domesticInventors = quarterlyInventors.Where(qi => !qi.OfficeLocation.Contains("CHN"));

			foreach (var quarterlyInventor in domesticInventors.Where(qi => qi.Cubes.Any()))
			{
				var inventorAddress = inventorAddresses.FirstOrDefault(a => a.EmployeeId == quarterlyInventor.EmployeeId);

				if (inventorAddress != null 
					&& inventorAddress.Country.Equals("United States", StringComparison.OrdinalIgnoreCase	))
				{
					bool plaqueNeeded = quarterlyInventor.NewPlaqueCount > 0;
					bool isFirstCube = true;

					foreach (var cube in quarterlyInventor.Cubes)
					{
						string countryValue = inventorAddress.Country;
						if (!string.IsNullOrEmpty(countryValue) && CountryCodes.Codes.TryGetValue(countryValue, out var code))
						{
							countryValue = code;
						}
						else if (!string.IsNullOrEmpty(countryValue))
						{
							_reporter($"Warning: Country '{countryValue}' for inventor '{quarterlyInventor.Name}' not found in CountryCodes. Using original value.");
						}

						var line = new StringBuilder();
						line.Append($"\"{quarterlyInventor.Name.Replace(",","")}\",");
						line.Append($"\"{inventorAddress.AddressLine1}\",");
						line.Append($"\"{inventorAddress.AddressLine2}\",");
						line.Append($"\"{inventorAddress.City}\",");
						line.Append($"\"{inventorAddress.State}\",");
						line.Append($"\"{inventorAddress.ZipCode}\",");
						line.Append($"\"{countryValue}\",");
						line.Append($"\"{cube.Value}\","); // Patent Number

						line.Append(plaqueNeeded && isFirstCube ? "Yes," : "No,");
						line.Append("Yes"); // Cube

						writer.WriteLine(line.ToString());
						isFirstCube = false;
					}
				}
				else if (inventorAddress == null)
				{
					_reporter($"Warning: No address found for inventor '{quarterlyInventor.Name}' with Employee ID '{quarterlyInventor.EmployeeId}'. Skipping from vendor file.");
				}
			}

			_reporter("Vendor domestic inventor award file writing complete.");
		}

		private void WriteInternationalVendorInventorAwardFile(IEnumerable<QuarterlyInventor> quarterlyInventors, IEnumerable<InventorAddress> inventorAddresses)
		{
			_reporter("Writing vendor international inventor award file...");

			using StreamWriter writer = new(_vendorInternationalOutputPath);
			writer.WriteLine("Name,Address Line 1,Address Line 2,City,State,Zip,Country,Patent Number,Plaque,Cube,Phone,Email");

			var inventors = quarterlyInventors.Where(qi => !qi.OfficeLocation.Contains("CHN"));

			foreach (var quarterlyInventor in inventors.Where(qi => qi.Cubes.Any()))
			{
				var inventorAddress = inventorAddresses.FirstOrDefault(a => a.EmployeeId == quarterlyInventor.EmployeeId);

				if (inventorAddress != null
					&& !inventorAddress.Country.Equals("United States", StringComparison.OrdinalIgnoreCase))
				{
					bool plaqueNeeded = quarterlyInventor.NewPlaqueCount > 0;
					bool isFirstCube = true;

					foreach (var cube in quarterlyInventor.Cubes)
					{
						string countryValue = inventorAddress.Country;
						if (!string.IsNullOrEmpty(countryValue) && CountryCodes.Codes.TryGetValue(countryValue, out var code))
						{
							countryValue = code;
						}
						else if (!string.IsNullOrEmpty(countryValue))
						{
							_reporter($"Warning: Country '{countryValue}' for inventor '{quarterlyInventor.Name}' not found in CountryCodes. Using original value.");
						}

						var line = new StringBuilder();
						line.Append($"\"{quarterlyInventor.Name.Replace(",", "")}\",");
						line.Append($"\"{inventorAddress.AddressLine1}\",");
						line.Append($"\"{inventorAddress.AddressLine2}\",");
						line.Append($"\"{inventorAddress.City}\",");
						line.Append($"\"{inventorAddress.State}\",");
						line.Append($"\"{inventorAddress.ZipCode}\",");
						line.Append($"\"{countryValue}\",");
						line.Append($"\"{cube.Value}\","); // Patent Number

						line.Append(plaqueNeeded && isFirstCube ? "Yes," : "No,");
						line.Append("Yes,"); // Cube
						line.Append($"\"\",");
						line.Append($"\"{inventorAddress.EmailWork}\""); // Email

						writer.WriteLine(line.ToString());
						isFirstCube = false;
					}
				}
				else if (inventorAddress == null)
				{
					_reporter($"Warning: No address found for inventor '{quarterlyInventor.Name}' with Employee ID '{quarterlyInventor.EmployeeId}'. Skipping from vendor file.");
				}
			}

			_reporter("Vendor international inventor award file writing complete.");
		}
	}
}