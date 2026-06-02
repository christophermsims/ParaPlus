using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Helper;
using ParaPlus.Business.Jobs;
using ParaPlus.Business.Model;

namespace ParaPlus.Windows
{
	public partial class frmInventorAwardsIssued : Form
	{
		public frmInventorAwardsIssued()
		{
			InitializeComponent();
		}

		private void btnSelectQuarterlyIssuedAwardFile_Click(object sender, EventArgs e)
		{
			if (File.Exists(txtQuarterlyIssuedAwardsFile.Text))
			{
				ofdSelectFile.InitialDirectory = Path.GetDirectoryName(txtQuarterlyIssuedAwardsFile.Text);
				ofdSelectFile.FileName = Path.GetFileName(txtQuarterlyIssuedAwardsFile.Text);
			}

			if (ofdSelectFile.ShowDialog() == DialogResult.OK)
			{
				txtQuarterlyIssuedAwardsFile.Text = ofdSelectFile.FileName;
			}
		}

		private void btnSelectMasterFile_Click(object sender, EventArgs e)
		{
			if (File.Exists(txtMasterFile.Text))
			{
				ofdSelectFile.InitialDirectory = Path.GetDirectoryName(txtMasterFile.Text);
				ofdSelectFile.FileName = Path.GetFileName(txtMasterFile.Text);
			}

			if (ofdSelectFile.ShowDialog() == DialogResult.OK)
			{
				txtMasterFile.Text = ofdSelectFile.FileName;
			}
		}

		private void btnSelectAddressFile_Click(object sender, EventArgs e)
		{
			if (File.Exists(txtInventorAddresses.Text))
			{
				ofdSelectFile.InitialDirectory = Path.GetDirectoryName(txtInventorAddresses.Text);
				ofdSelectFile.FileName = Path.GetFileName(txtInventorAddresses.Text);
			}

			if (ofdSelectFile.ShowDialog() == DialogResult.OK)
			{
				txtInventorAddresses.Text = ofdSelectFile.FileName;
			}
		}

		private void btnSelectOutputFolder_Click(object sender, EventArgs e)
		{
			fbdSelectFolder.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

			if (fbdSelectFolder.ShowDialog() == DialogResult.OK)
			{
				txtOutputFolder.Text = fbdSelectFolder.SelectedPath;
			}
		}

		private async void btnProcessAwards_Click(object sender, EventArgs e)
		{
			if (!ValidateForm())
				return;

			IFileVerifier quarterlyIssuedAwardsFileVerifier = new QuarterlyIssuedAwardsFileVerifier();
			IFileProcessor<QuarterlyInventor> quarterlyIssuedAwardsFileProcessor = new QuarterlyIssuedAwardsFileProcessor(quarterlyIssuedAwardsFileVerifier);

			IFileVerifier masterFileVerifier = new MasterIssuedAwardsFileVerifier();
			IFileProcessor<MasterInventor> masterFileProcessor = new MasterIssuedAwardsFileProcessor(masterFileVerifier);

			IFileVerifier inventorAddressFileVerifier = new InventorAddressFileVerifier();
			IFileProcessor<InventorAddress> inventorAddressFileProcessor = new InventorAddressFileProcessor(inventorAddressFileVerifier);

			IssuedInventorAwardsJob issuedAwardsJob = new IssuedInventorAwardsJob(
				quarterlyIssuedAwardsFileProcessor,
				masterFileProcessor,
				inventorAddressFileProcessor,
				msg => BeginInvoke((Action)(() => txtLogs.AppendText($"{msg}{Environment.NewLine}")))
			);

			issuedAwardsJob.QuarterlyFilePath = txtQuarterlyIssuedAwardsFile.Text;
			issuedAwardsJob.MasterFilePath = txtMasterFile.Text;
			issuedAwardsJob.InventorAddressFilePath = txtInventorAddresses.Text;
			issuedAwardsJob.OutputFolder = txtOutputFolder.Text;

			tabIssuedAwards.SelectedIndex = 2;//Switch to Logs Tab

			// Disable the button to prevent accidental double-clicks while processing
			btnProcessAwards.Enabled = false;

			try
			{
				// Offload the heavy process to a background thread
				await Task.Run(() =>
				{
					issuedAwardsJob.ExecuteJob();
				});
			}
			finally
			{
				// Re-enable the button once the process finishes (even if it fails)
				btnProcessAwards.Enabled = true;
			}

		}

		private void lnkIssueInventorAwardsDue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Use UseShellExecute = true for .NET Core / .NET 5+ compatibility
			var psi = new System.Diagnostics.ProcessStartInfo
			{
				FileName = LinkConstants.QuarterlyIssuedInvetorAwardsDue,
				UseShellExecute = true
			};
			System.Diagnostics.Process.Start(psi);
		}

		private void lnkMasterFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Use UseShellExecute = true for .NET Core / .NET 5+ compatibility
			var psi = new System.Diagnostics.ProcessStartInfo
			{
				FileName = LinkConstants.MasterAwardsFile,
				UseShellExecute = true
			};
			System.Diagnostics.Process.Start(psi);
		}

		private bool ValidateForm()
		{
			var errors = new List<string>();

			// Text boxes that must reference existing files
			if (string.IsNullOrWhiteSpace(txtQuarterlyIssuedAwardsFile.Text) || !File.Exists(txtQuarterlyIssuedAwardsFile.Text))
				errors.Add("Quarterly Issued Awards file is missing or does not exist.");

			if (string.IsNullOrWhiteSpace(txtMasterFile.Text) || !File.Exists(txtMasterFile.Text))
				errors.Add("Master file is missing or does not exist.");

			if (string.IsNullOrWhiteSpace(txtInventorAddresses.Text) || !File.Exists(txtInventorAddresses.Text))
				errors.Add("Inventor Address file is missing or does not exist.");

			// Output directory must exist
			if (string.IsNullOrWhiteSpace(txtOutputFolder.Text) || !Directory.Exists(txtOutputFolder.Text))
				errors.Add("Output Folder is missing or does not exist.");

			if (errors.Any())
			{
				MessageBox.Show(string.Join(Environment.NewLine, errors), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return true;
		}

		private void btnSelectChineseInventors_Click(object sender, EventArgs e)
		{
			if (File.Exists(txtChineseInventor.Text))
			{
				ofdSelectFile.InitialDirectory = Path.GetDirectoryName(txtChineseInventor.Text);
				ofdSelectFile.FileName = Path.GetFileName(txtChineseInventor.Text);
			}

			if (ofdSelectFile.ShowDialog() == DialogResult.OK)
			{
				txtChineseInventor.Text = ofdSelectFile.FileName;
			}
		}

		private void btnSelectChineseOutputFolder_Click(object sender, EventArgs e)
		{
			fbdSelectFolder.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

			if (fbdSelectFolder.ShowDialog() == DialogResult.OK)
			{
				txtChineseOutputFolder.Text = fbdSelectFolder.SelectedPath;
			}
		}

		private bool ValidateChineseForm()
		{
			var errors = new List<string>();

			// Text boxes that must reference existing files
			if (string.IsNullOrWhiteSpace(txtChineseInventor.Text) || !File.Exists(txtChineseInventor.Text))
				errors.Add("Chinese Inventor Awards file is missing or does not exist.");

			// Output directory must exist
			if (string.IsNullOrWhiteSpace(txtChineseOutputFolder.Text) || !Directory.Exists(txtChineseOutputFolder.Text))
				errors.Add("Output Folder is missing or does not exist.");

			if (errors.Any())
			{
				MessageBox.Show(string.Join(Environment.NewLine, errors), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return true;
		}

		private async void btnProcessChineseAwards_Click(object sender, EventArgs e)
		{
			if (!ValidateChineseForm())
				return;

			IFileVerifier chineseInventorFileVerifier = new ChineseInventorFileVerifier();
			IFileProcessor<ChineseInventor> chineseIventorFileProcessor = new ChineseInventorFileProcessor(chineseInventorFileVerifier);

			IssuedChineseInventorAwardsJob issuedAwardsJob = new IssuedChineseInventorAwardsJob(
				chineseIventorFileProcessor,
				msg => BeginInvoke((Action)(() => txtLogs.AppendText($"{msg}{Environment.NewLine}")))
			);

			issuedAwardsJob.ChineseInventorFile = txtChineseInventor.Text;
			issuedAwardsJob.OutputFolder = txtChineseOutputFolder.Text;

			tabIssuedAwards.SelectedIndex = 2;//Switch to Logs Tab

			// Disable the button to prevent accidental double-clicks while processing
			btnProcessChineseAwards.Enabled = false;

			try
			{
				// Offload the heavy process to a background thread
				await Task.Run(() =>
				{
					issuedAwardsJob.ExecuteJob();
				});
			}
			finally
			{
				// Re-enable the button once the process finishes (even if it fails)
				btnProcessChineseAwards.Enabled = true;
			}
		}
	}
}