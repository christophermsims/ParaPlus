namespace ParaPlus.Windows
{
    partial class frmQuarterlyOnePagers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			cboStartFiscalYear = new ComboBox();
			cboStartFiscalQuarter = new ComboBox();
			label1 = new Label();
			label2 = new Label();
			txtTemplateFileName = new TextBox();
			lblTemplate = new Label();
			btnTemplateSelector = new Button();
			grpStartQuarter = new GroupBox();
			grpEndQuarter = new GroupBox();
			label3 = new Label();
			cboEndFiscalYear = new ComboBox();
			cboEndFiscalQuarter = new ComboBox();
			label4 = new Label();
			btnQuarterlyFilingsSelector = new Button();
			txtQuarterlyPatentFilings = new TextBox();
			label5 = new Label();
			label6 = new Label();
			btnQuarterlyIssuedSelectors = new Button();
			txtQuarterlyPatentsIssued = new TextBox();
			groupBox2 = new GroupBox();
			lnkIssuedPatents = new LinkLabel();
			lnkPatentFilings = new LinkLabel();
			btnSelectQuarterlyOnePagers = new Button();
			txtQuarterlyOnePagersFile = new TextBox();
			btnSelectOutputDirectory = new Button();
			txtOutputDirectory = new TextBox();
			btnGeneratePresentation = new Button();
			label7 = new Label();
			label8 = new Label();
			lnkQuarterlyOnePagers = new LinkLabel();
			fbdSelectFile = new FolderBrowserDialog();
			ofdFileSelector = new OpenFileDialog();
			tabGenerateOnePagers = new TabControl();
			tabSettingsPage = new TabPage();
			tabLogsPage = new TabPage();
			txtLogs = new RichTextBox();
			grpStartQuarter.SuspendLayout();
			grpEndQuarter.SuspendLayout();
			groupBox2.SuspendLayout();
			tabGenerateOnePagers.SuspendLayout();
			tabSettingsPage.SuspendLayout();
			tabLogsPage.SuspendLayout();
			SuspendLayout();
			// 
			// cboStartFiscalYear
			// 
			cboStartFiscalYear.FormattingEnabled = true;
			cboStartFiscalYear.Items.AddRange(new object[] { "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030" });
			cboStartFiscalYear.Location = new Point(20, 67);
			cboStartFiscalYear.Name = "cboStartFiscalYear";
			cboStartFiscalYear.Size = new Size(182, 33);
			cboStartFiscalYear.TabIndex = 1;
			// 
			// cboStartFiscalQuarter
			// 
			cboStartFiscalQuarter.FormattingEnabled = true;
			cboStartFiscalQuarter.Items.AddRange(new object[] { "Q1", "Q2", "Q3", "Q4" });
			cboStartFiscalQuarter.Location = new Point(225, 67);
			cboStartFiscalQuarter.Name = "cboStartFiscalQuarter";
			cboStartFiscalQuarter.Size = new Size(182, 33);
			cboStartFiscalQuarter.TabIndex = 3;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(21, 39);
			label1.Name = "label1";
			label1.Size = new Size(91, 25);
			label1.TabIndex = 0;
			label1.Text = "Fiscal Year";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(226, 37);
			label2.Name = "label2";
			label2.Size = new Size(119, 25);
			label2.TabIndex = 2;
			label2.Text = "Fiscal Quarter";
			// 
			// txtTemplateFileName
			// 
			txtTemplateFileName.Location = new Point(9, 49);
			txtTemplateFileName.Name = "txtTemplateFileName";
			txtTemplateFileName.ReadOnly = true;
			txtTemplateFileName.Size = new Size(767, 31);
			txtTemplateFileName.TabIndex = 1;
			// 
			// lblTemplate
			// 
			lblTemplate.AutoSize = true;
			lblTemplate.Location = new Point(14, 10);
			lblTemplate.Name = "lblTemplate";
			lblTemplate.Size = new Size(186, 25);
			lblTemplate.TabIndex = 0;
			lblTemplate.Text = "Presentation Template";
			// 
			// btnTemplateSelector
			// 
			btnTemplateSelector.Location = new Point(782, 42);
			btnTemplateSelector.Name = "btnTemplateSelector";
			btnTemplateSelector.Size = new Size(124, 44);
			btnTemplateSelector.TabIndex = 2;
			btnTemplateSelector.Text = "Select";
			btnTemplateSelector.UseVisualStyleBackColor = true;
			btnTemplateSelector.Click += btnTemplateSelector_Click;
			// 
			// grpStartQuarter
			// 
			grpStartQuarter.Controls.Add(label1);
			grpStartQuarter.Controls.Add(cboStartFiscalYear);
			grpStartQuarter.Controls.Add(cboStartFiscalQuarter);
			grpStartQuarter.Controls.Add(label2);
			grpStartQuarter.Location = new Point(9, 114);
			grpStartQuarter.Name = "grpStartQuarter";
			grpStartQuarter.Size = new Size(430, 126);
			grpStartQuarter.TabIndex = 3;
			grpStartQuarter.TabStop = false;
			grpStartQuarter.Text = "Starting Fiscal Quarter";
			// 
			// grpEndQuarter
			// 
			grpEndQuarter.Controls.Add(label3);
			grpEndQuarter.Controls.Add(cboEndFiscalYear);
			grpEndQuarter.Controls.Add(cboEndFiscalQuarter);
			grpEndQuarter.Controls.Add(label4);
			grpEndQuarter.Location = new Point(477, 114);
			grpEndQuarter.Name = "grpEndQuarter";
			grpEndQuarter.Size = new Size(430, 126);
			grpEndQuarter.TabIndex = 4;
			grpEndQuarter.TabStop = false;
			grpEndQuarter.Text = "Ending Fiscal Quarter";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(21, 39);
			label3.Name = "label3";
			label3.Size = new Size(91, 25);
			label3.TabIndex = 0;
			label3.Text = "Fiscal Year";
			// 
			// cboEndFiscalYear
			// 
			cboEndFiscalYear.FormattingEnabled = true;
			cboEndFiscalYear.Items.AddRange(new object[] { "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030" });
			cboEndFiscalYear.Location = new Point(20, 67);
			cboEndFiscalYear.Name = "cboEndFiscalYear";
			cboEndFiscalYear.Size = new Size(182, 33);
			cboEndFiscalYear.TabIndex = 1;
			// 
			// cboEndFiscalQuarter
			// 
			cboEndFiscalQuarter.FormattingEnabled = true;
			cboEndFiscalQuarter.Items.AddRange(new object[] { "Q1", "Q2", "Q3", "Q4" });
			cboEndFiscalQuarter.Location = new Point(225, 67);
			cboEndFiscalQuarter.Name = "cboEndFiscalQuarter";
			cboEndFiscalQuarter.Size = new Size(182, 33);
			cboEndFiscalQuarter.TabIndex = 3;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(226, 37);
			label4.Name = "label4";
			label4.Size = new Size(119, 25);
			label4.TabIndex = 2;
			label4.Text = "Fiscal Quarter";
			// 
			// btnQuarterlyFilingsSelector
			// 
			btnQuarterlyFilingsSelector.Location = new Point(755, 69);
			btnQuarterlyFilingsSelector.Name = "btnQuarterlyFilingsSelector";
			btnQuarterlyFilingsSelector.Size = new Size(124, 44);
			btnQuarterlyFilingsSelector.TabIndex = 3;
			btnQuarterlyFilingsSelector.Text = "Select";
			btnQuarterlyFilingsSelector.UseVisualStyleBackColor = true;
			btnQuarterlyFilingsSelector.Click += btnQuarterlyFilingsSelector_Click;
			// 
			// txtQuarterlyPatentFilings
			// 
			txtQuarterlyPatentFilings.Location = new Point(21, 76);
			txtQuarterlyPatentFilings.Name = "txtQuarterlyPatentFilings";
			txtQuarterlyPatentFilings.ReadOnly = true;
			txtQuarterlyPatentFilings.Size = new Size(728, 31);
			txtQuarterlyPatentFilings.TabIndex = 2;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(21, 39);
			label5.Name = "label5";
			label5.Size = new Size(252, 25);
			label5.TabIndex = 0;
			label5.Text = "Quarterly Patent Filings Report";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(21, 130);
			label6.Name = "label6";
			label6.Size = new Size(277, 25);
			label6.TabIndex = 4;
			label6.Text = "Quarterly Patent Issuances Report";
			// 
			// btnQuarterlyIssuedSelectors
			// 
			btnQuarterlyIssuedSelectors.Location = new Point(755, 160);
			btnQuarterlyIssuedSelectors.Name = "btnQuarterlyIssuedSelectors";
			btnQuarterlyIssuedSelectors.Size = new Size(124, 44);
			btnQuarterlyIssuedSelectors.TabIndex = 7;
			btnQuarterlyIssuedSelectors.Text = "Select";
			btnQuarterlyIssuedSelectors.UseVisualStyleBackColor = true;
			btnQuarterlyIssuedSelectors.Click += btnQuarterlyIssuedSelectors_Click;
			// 
			// txtQuarterlyPatentsIssued
			// 
			txtQuarterlyPatentsIssued.Location = new Point(21, 167);
			txtQuarterlyPatentsIssued.Name = "txtQuarterlyPatentsIssued";
			txtQuarterlyPatentsIssued.ReadOnly = true;
			txtQuarterlyPatentsIssued.Size = new Size(728, 31);
			txtQuarterlyPatentsIssued.TabIndex = 6;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(lnkIssuedPatents);
			groupBox2.Controls.Add(lnkPatentFilings);
			groupBox2.Controls.Add(label5);
			groupBox2.Controls.Add(label6);
			groupBox2.Controls.Add(txtQuarterlyPatentFilings);
			groupBox2.Controls.Add(btnQuarterlyIssuedSelectors);
			groupBox2.Controls.Add(btnQuarterlyFilingsSelector);
			groupBox2.Controls.Add(txtQuarterlyPatentsIssued);
			groupBox2.Location = new Point(9, 261);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(902, 231);
			groupBox2.TabIndex = 5;
			groupBox2.TabStop = false;
			groupBox2.Text = "Cumulative Chart";
			// 
			// lnkIssuedPatents
			// 
			lnkIssuedPatents.AutoSize = true;
			lnkIssuedPatents.Location = new Point(304, 130);
			lnkIssuedPatents.Name = "lnkIssuedPatents";
			lnkIssuedPatents.Size = new Size(197, 25);
			lnkIssuedPatents.TabIndex = 5;
			lnkIssuedPatents.TabStop = true;
			lnkIssuedPatents.Text = "!Original Issued Patents";
			lnkIssuedPatents.LinkClicked += lnkIssuedPatents_LinkClicked;
			// 
			// lnkPatentFilings
			// 
			lnkPatentFilings.AutoSize = true;
			lnkPatentFilings.Location = new Point(279, 39);
			lnkPatentFilings.Name = "lnkPatentFilings";
			lnkPatentFilings.Size = new Size(188, 25);
			lnkPatentFilings.TabIndex = 1;
			lnkPatentFilings.TabStop = true;
			lnkPatentFilings.Text = "!Original Patent Filings";
			lnkPatentFilings.LinkClicked += lnkPatentFilings_LinkClicked;
			// 
			// btnSelectQuarterlyOnePagers
			// 
			btnSelectQuarterlyOnePagers.Location = new Point(787, 551);
			btnSelectQuarterlyOnePagers.Name = "btnSelectQuarterlyOnePagers";
			btnSelectQuarterlyOnePagers.Size = new Size(124, 44);
			btnSelectQuarterlyOnePagers.TabIndex = 9;
			btnSelectQuarterlyOnePagers.Text = "Select";
			btnSelectQuarterlyOnePagers.UseVisualStyleBackColor = true;
			btnSelectQuarterlyOnePagers.Click += btnSelectQuarterlyOnePagers_Click;
			// 
			// txtQuarterlyOnePagersFile
			// 
			txtQuarterlyOnePagersFile.Location = new Point(14, 558);
			txtQuarterlyOnePagersFile.Name = "txtQuarterlyOnePagersFile";
			txtQuarterlyOnePagersFile.ReadOnly = true;
			txtQuarterlyOnePagersFile.Size = new Size(767, 31);
			txtQuarterlyOnePagersFile.TabIndex = 8;
			// 
			// btnSelectOutputDirectory
			// 
			btnSelectOutputDirectory.Location = new Point(787, 631);
			btnSelectOutputDirectory.Name = "btnSelectOutputDirectory";
			btnSelectOutputDirectory.Size = new Size(124, 44);
			btnSelectOutputDirectory.TabIndex = 12;
			btnSelectOutputDirectory.Text = "Select";
			btnSelectOutputDirectory.UseVisualStyleBackColor = true;
			btnSelectOutputDirectory.Click += btnSelectOutputDirectory_Click;
			// 
			// txtOutputDirectory
			// 
			txtOutputDirectory.Location = new Point(14, 638);
			txtOutputDirectory.Name = "txtOutputDirectory";
			txtOutputDirectory.ReadOnly = true;
			txtOutputDirectory.Size = new Size(767, 31);
			txtOutputDirectory.TabIndex = 11;
			// 
			// btnGeneratePresentation
			// 
			btnGeneratePresentation.Location = new Point(702, 702);
			btnGeneratePresentation.Name = "btnGeneratePresentation";
			btnGeneratePresentation.Size = new Size(209, 44);
			btnGeneratePresentation.TabIndex = 13;
			btnGeneratePresentation.Text = "Generate Presentation";
			btnGeneratePresentation.UseVisualStyleBackColor = true;
			btnGeneratePresentation.Click += btnGeneratePresentation_Click;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(14, 528);
			label7.Name = "label7";
			label7.Size = new Size(198, 25);
			label7.TabIndex = 6;
			label7.Text = "Filings Report (CSV File)";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(6, 603);
			label8.Name = "label8";
			label8.Size = new Size(124, 25);
			label8.TabIndex = 10;
			label8.Text = "Output Folder";
			// 
			// lnkQuarterlyOnePagers
			// 
			lnkQuarterlyOnePagers.AutoSize = true;
			lnkQuarterlyOnePagers.Location = new Point(218, 528);
			lnkQuarterlyOnePagers.Name = "lnkQuarterlyOnePagers";
			lnkQuarterlyOnePagers.Size = new Size(301, 25);
			lnkQuarterlyOnePagers.TabIndex = 7;
			lnkQuarterlyOnePagers.TabStop = true;
			lnkQuarterlyOnePagers.Text = "Quarterly Patent Filings - One Pagers";
			lnkQuarterlyOnePagers.LinkClicked += lnkQuarterlyOnePagers_LinkClicked;
			// 
			// ofdFileSelector
			// 
			ofdFileSelector.FileName = "ofdFileSelector";
			// 
			// tabGenerateOnePagers
			// 
			tabGenerateOnePagers.Controls.Add(tabSettingsPage);
			tabGenerateOnePagers.Controls.Add(tabLogsPage);
			tabGenerateOnePagers.Location = new Point(12, 12);
			tabGenerateOnePagers.Name = "tabGenerateOnePagers";
			tabGenerateOnePagers.SelectedIndex = 0;
			tabGenerateOnePagers.Size = new Size(939, 805);
			tabGenerateOnePagers.TabIndex = 14;
			// 
			// tabSettingsPage
			// 
			tabSettingsPage.Controls.Add(lblTemplate);
			tabSettingsPage.Controls.Add(lnkQuarterlyOnePagers);
			tabSettingsPage.Controls.Add(txtTemplateFileName);
			tabSettingsPage.Controls.Add(label8);
			tabSettingsPage.Controls.Add(btnTemplateSelector);
			tabSettingsPage.Controls.Add(label7);
			tabSettingsPage.Controls.Add(grpStartQuarter);
			tabSettingsPage.Controls.Add(btnGeneratePresentation);
			tabSettingsPage.Controls.Add(grpEndQuarter);
			tabSettingsPage.Controls.Add(btnSelectOutputDirectory);
			tabSettingsPage.Controls.Add(groupBox2);
			tabSettingsPage.Controls.Add(txtOutputDirectory);
			tabSettingsPage.Controls.Add(txtQuarterlyOnePagersFile);
			tabSettingsPage.Controls.Add(btnSelectQuarterlyOnePagers);
			tabSettingsPage.Location = new Point(4, 34);
			tabSettingsPage.Name = "tabSettingsPage";
			tabSettingsPage.Padding = new Padding(3);
			tabSettingsPage.Size = new Size(931, 767);
			tabSettingsPage.TabIndex = 0;
			tabSettingsPage.Text = "Settings";
			tabSettingsPage.UseVisualStyleBackColor = true;
			// 
			// tabLogsPage
			// 
			tabLogsPage.Controls.Add(txtLogs);
			tabLogsPage.Location = new Point(4, 34);
			tabLogsPage.Name = "tabLogsPage";
			tabLogsPage.Padding = new Padding(3);
			tabLogsPage.Size = new Size(931, 767);
			tabLogsPage.TabIndex = 1;
			tabLogsPage.Text = "Logs";
			tabLogsPage.UseVisualStyleBackColor = true;
			// 
			// txtLogs
			// 
			txtLogs.Location = new Point(6, 6);
			txtLogs.Name = "txtLogs";
			txtLogs.ReadOnly = true;
			txtLogs.Size = new Size(919, 755);
			txtLogs.TabIndex = 0;
			txtLogs.Text = "";
			// 
			// frmQuarterlyOnePagers
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(965, 844);
			Controls.Add(tabGenerateOnePagers);
			Name = "frmQuarterlyOnePagers";
			Text = "Generate Quarterly One Pager Presentation";
			grpStartQuarter.ResumeLayout(false);
			grpStartQuarter.PerformLayout();
			grpEndQuarter.ResumeLayout(false);
			grpEndQuarter.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			tabGenerateOnePagers.ResumeLayout(false);
			tabSettingsPage.ResumeLayout(false);
			tabSettingsPage.PerformLayout();
			tabLogsPage.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private ComboBox cboStartFiscalYear;
        private ComboBox cboStartFiscalQuarter;
        private Label label1;
        private Label label2;
        private TextBox txtTemplateFileName;
        private Label lblTemplate;
        private Button btnTemplateSelector;
        private GroupBox grpStartQuarter;
        private GroupBox grpEndQuarter;
        private Label label3;
        private ComboBox cboEndFiscalYear;
        private ComboBox cboEndFiscalQuarter;
        private Label label4;
        private Button btnQuarterlyFilingsSelector;
        private TextBox txtQuarterlyPatentFilings;
        private Label label5;
        private Label label6;
        private Button btnQuarterlyIssuedSelectors;
        private TextBox txtQuarterlyPatentsIssued;
        private GroupBox groupBox2;
        private LinkLabel lnkPatentFilings;
        private LinkLabel lnkIssuedPatents;
        private Button btnSelectQuarterlyOnePagers;
        private TextBox txtQuarterlyOnePagersFile;
        private Button btnSelectOutputDirectory;
        private TextBox txtOutputDirectory;
        private Button btnGeneratePresentation;
        private Label label7;
        private Label label8;
        private LinkLabel lnkQuarterlyOnePagers;
        private FolderBrowserDialog fbdSelectFile;
        private OpenFileDialog ofdFileSelector;
		private TabControl tabGenerateOnePagers;
		private TabPage tabSettingsPage;
		private TabPage tabLogsPage;
		private RichTextBox txtLogs;
	}
}