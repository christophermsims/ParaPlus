using System;
using System.Collections.Generic;
using System.Text;
using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Helper;
using ParaPlus.Business.Model;

namespace ParaPlus.Business.Jobs
{
	public class IssuedChineseInventorAwardsJob(
		IFileProcessor<ChineseInventor> chineseInventorFileProcessor,
		Action<string>? reportAction = null) : IJob
	{
		IEnumerable<ChineseInventor>? _chineseInventors;
		private readonly IFileProcessor<ChineseInventor> _chineseInventorFileProcessor = chineseInventorFileProcessor;

		private readonly Action<string> _reporter = reportAction ?? Console.WriteLine;

		private string _chineseInventorFile = @"";
		private string _outputFolder = @"";
		private string _chineseInventorOutputPath = @"";

		public string ChineseInventorFile
		{
			get => _chineseInventorFile;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					_reporter("Warning: Chinese inventor awards file path is empty. Retaining previous value.");
					return;
				}
				if (!File.Exists(value))
				{
					_reporter($"Warning: File '{value}' does not exist. Retaining previous value.");
					return;
				}
				_chineseInventorFile = value;
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

				_chineseInventorOutputPath = Path.Combine(value, OutputFiles.VendorChineseOutputFile);
			}
		}

		public void ExecuteJob()
		{
			_chineseInventors = ParseChineseInventorList();
			WriteChineseInventorOutputFile(_chineseInventors);
		}

		private IEnumerable<ChineseInventor> ParseChineseInventorList()
        {
            _reporter("Parsing chinese inventor awards list...");
			IEnumerable<ChineseInventor> chineseInventors = _chineseInventorFileProcessor.ProcessFile(_chineseInventorFile);
			_reporter("Chinese inventor awards list parsing complete.");

			return chineseInventors;
		}
		
		private void WriteChineseInventorOutputFile(IEnumerable<ChineseInventor> chineseInventors)
		{
			_reporter("Writing vendor Chinese inventor award file...");

			using StreamWriter writer = new(_chineseInventorOutputPath);
			writer.WriteLine("Name,Address Line 1,Address Line 2,City,State,Zip,Country,Patent Number,Plaque,Cube,Phone,Email");

			foreach (var inventor in chineseInventors)
			{
				bool plaqueNeeded = inventor.BaseNeeded.Equals("Yes", StringComparison.OrdinalIgnoreCase);
				bool isFirstCube = true;

				foreach (var cubeEntry in inventor.Cubes)
				{
					var line = new StringBuilder();
					line.Append($"\"{inventor.Name.Replace(",", "")}\",");
					line.Append($"\"{inventor.AddressLine1}\",");
					line.Append($"\"{inventor.AddressLine2}\",");
					line.Append($"\"{inventor.City}\",");
					line.Append($"\"{inventor.State}\",");
					line.Append($"\"{inventor.ZipCode}\",");
					line.Append($"\"{inventor.Country}\",");
					line.Append($"\"{cubeEntry.Value}\","); // Patent Number

					line.Append(plaqueNeeded && isFirstCube ? "Yes," : "No,");
					line.Append("Yes,"); // Cube
					line.Append($"\"{inventor.PhoneNumber}\",");
					line.Append($"\"{inventor.EmailAddress}\"");

					writer.WriteLine(line.ToString());
					isFirstCube = false;
				}
			}

			_reporter("Vendor Chinese inventor award file writing complete.");
		}
	}
}