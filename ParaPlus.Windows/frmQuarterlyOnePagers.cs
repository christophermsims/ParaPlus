using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Helper;
using ParaPlus.Business.Model;
using ParaPlus.Business.Presentations;

namespace ParaPlus.Windows
{
	public partial class frmQuarterlyOnePagers : Form
	{
		public frmQuarterlyOnePagers()
		{
			InitializeComponent();

			txtTemplateFileName.Text = $"{Directory.GetCurrentDirectory()}\\Templates\\QuarterlyPatentApplications_Template.pptx";

			int qStartIndex = cboStartFiscalQuarter.Items.IndexOf("Q1");
			if (qStartIndex >= 0)
				cboStartFiscalQuarter.SelectedIndex = qStartIndex;

			int yStartIndex = cboStartFiscalYear.Items.IndexOf("2022");
			if (yStartIndex >= 0)
				cboStartFiscalYear.SelectedIndex = yStartIndex;

			int qIndex = cboEndFiscalQuarter.Items.IndexOf(FiscalYear.LastFiscalQuarter);
			if (qIndex >= 0)
				cboEndFiscalQuarter.SelectedIndex = qIndex;

			int yIndex = cboEndFiscalYear.Items.IndexOf(FiscalYear.LastFiscalYear);
			if (yIndex >= 0)
				cboEndFiscalYear.SelectedIndex = yIndex;
		}

		private void btnTemplateSelector_Click(object sender, EventArgs e)
		{
			if (File.Exists(txtTemplateFileName.Text))
			{
				ofdFileSelector.InitialDirectory = Path.GetDirectoryName(txtTemplateFileName.Text);
				ofdFileSelector.FileName = Path.GetFileName(txtTemplateFileName.Text);
			}

			if (ofdFileSelector.ShowDialog() == DialogResult.OK)
			{
				txtTemplateFileName.Text = ofdFileSelector.FileName;
			}
		}

		private void btnQuarterlyFilingsSelector_Click(object sender, EventArgs e)
		{
			ofdFileSelector.InitialDirectory =
				Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

			if (ofdFileSelector.ShowDialog() == DialogResult.OK)
			{
				txtQuarterlyPatentFilings.Text = ofdFileSelector.FileName;
			}
		}

		private void btnQuarterlyIssuedSelectors_Click(object sender, EventArgs e)
		{
			ofdFileSelector.InitialDirectory =
				Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

			if (ofdFileSelector.ShowDialog() == DialogResult.OK)
			{
				txtQuarterlyPatentsIssued.Text = ofdFileSelector.FileName;
			}
		}

		private void lnkPatentFilings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Use UseShellExecute = true for .NET Core / .NET 5+ compatibility
			var psi = new System.Diagnostics.ProcessStartInfo
			{
				FileName = LinkConstants.QuaterlyPatentFilings,
				UseShellExecute = true
			};
			System.Diagnostics.Process.Start(psi);
		}

		private void lnkIssuedPatents_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Use UseShellExecute = true for .NET Core / .NET 5+ compatibility
			var psi = new System.Diagnostics.ProcessStartInfo
			{
				FileName = LinkConstants.QuaterlyPatentIssuances,
				UseShellExecute = true
			};
			System.Diagnostics.Process.Start(psi);
		}

		private void lnkQuarterlyOnePagers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Use UseShellExecute = true for .NET Core / .NET 5+ compatibility
			var psi = new System.Diagnostics.ProcessStartInfo
			{
				FileName = LinkConstants.QuaterlyOnePagers,
				UseShellExecute = true
			};
			System.Diagnostics.Process.Start(psi);
		}

		private void btnSelectQuarterlyOnePagers_Click(object sender, EventArgs e)
		{
			ofdFileSelector.InitialDirectory =
				Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

			if (ofdFileSelector.ShowDialog() == DialogResult.OK)
			{
				txtQuarterlyOnePagersFile.Text = ofdFileSelector.FileName;
			}
		}

		private void btnSelectOutputDirectory_Click(object sender, EventArgs e)
		{
			fbdSelectFile.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

			if (fbdSelectFile.ShowDialog() == DialogResult.OK)
			{
				txtOutputDirectory.Text = fbdSelectFile.SelectedPath;
			}
		}

        private async void btnGeneratePresentation_Click(object sender, EventArgs e)
		{
			if (!ValidateForm())
				return;

			IFileVerifier _onePagerFileVerifier = new OnePagerFileVerifier();
			IFileProcessor<OnePagerDetails> _onePagerFileProcessor = new OnePagerFileProcessor(_onePagerFileVerifier);
			IQuarterlySummaryService<OriginalPatentFiling> _filingsSummaryService = new QuarterlyPatentFilingsSummaryService(txtQuarterlyPatentFilings.Text);
			IQuarterlySummaryService<OriginalPatentIssuance> _issuanceSummaryService = new QuarterlyPatentIssuancesSummaryService(txtQuarterlyPatentsIssued.Text);
            OnePagerPresentationBuilder _presentationBuilder = new(
				_onePagerFileProcessor,
				_filingsSummaryService,
				_issuanceSummaryService,
				msg => BeginInvoke((Action)(() => txtLogs.AppendText($"{msg}{Environment.NewLine}")))
			);

			string startingQuarter = $"{cboStartFiscalYear.SelectedItem} {cboStartFiscalQuarter.SelectedItem}";
			string endingQuarter = $"{cboEndFiscalYear.SelectedItem} {cboEndFiscalQuarter.SelectedItem}";

			_presentationBuilder.TemplateFilePath = txtTemplateFileName.Text;
			_presentationBuilder.QuarterlyOnePagersFilePath = txtQuarterlyOnePagersFile.Text;
			_presentationBuilder.OutputFolder = txtOutputDirectory.Text;

			tabGenerateOnePagers.SelectedIndex = 1;//Switch to Logs Tab

			// Disable the button to prevent accidental double-clicks while processing
			btnGeneratePresentation.Enabled = false;

			try
			{
				// Offload the heavy process to a background thread
				await Task.Run(() =>
				{
					_presentationBuilder.BuildPresentation(startingQuarter, endingQuarter);
				});
			}
			finally
			{
				// Re-enable the button once the process finishes (even if it fails)
				btnGeneratePresentation.Enabled = true;
			}
		}

		private bool ValidateForm()
		{
			var errors = new List<string>();

			// Text boxes that must reference existing files
			if (string.IsNullOrWhiteSpace(txtTemplateFileName.Text) || !File.Exists(txtTemplateFileName.Text))
				errors.Add("Template file is missing or does not exist.");

			if (string.IsNullOrWhiteSpace(txtQuarterlyPatentFilings.Text) || !File.Exists(txtQuarterlyPatentFilings.Text))
				errors.Add("Quarterly patent filings file is missing or does not exist.");

			if (string.IsNullOrWhiteSpace(txtQuarterlyPatentsIssued.Text) || !File.Exists(txtQuarterlyPatentsIssued.Text))
				errors.Add("Quarterly patents issued file is missing or does not exist.");

			if (string.IsNullOrWhiteSpace(txtQuarterlyOnePagersFile.Text) || !File.Exists(txtQuarterlyOnePagersFile.Text))
				errors.Add("Quarterly one-pagers file is missing or does not exist.");

			// Output directory must exist
			if (string.IsNullOrWhiteSpace(txtOutputDirectory.Text) || !Directory.Exists(txtOutputDirectory.Text))
				errors.Add("Output directory is missing or does not exist.");

			// Combo boxes must have selections
			if (cboStartFiscalYear.SelectedIndex < 0)
				errors.Add("Start fiscal year must be selected.");
			if (cboStartFiscalQuarter.SelectedIndex < 0)
				errors.Add("Start fiscal quarter must be selected.");
			if (cboEndFiscalYear.SelectedIndex < 0)
				errors.Add("End fiscal year must be selected.");
			if (cboEndFiscalQuarter.SelectedIndex < 0)
				errors.Add("End fiscal quarter must be selected.");

			if (errors.Any())
			{
				MessageBox.Show(string.Join(Environment.NewLine, errors), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return true;
		}
	}
}
