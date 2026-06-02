namespace ParaPlus.Windows
{
    partial class frmInventorAwardsIssued
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
			tabIssuedAwards = new TabControl();
			tabSettings = new TabPage();
			btnProcessAwards = new Button();
			grpOutputs = new GroupBox();
			lblOutputFolder = new Label();
			txtOutputFolder = new TextBox();
			btnSelectOutputFolder = new Button();
			grpInputs = new GroupBox();
			btnSelectAddressFile = new Button();
			txtInventorAddresses = new TextBox();
			lblInventorAddresses = new Label();
			txtQuarterlyIssuedAwardsFile = new TextBox();
			lnkMasterFile = new LinkLabel();
			btnSelectQuarterlyIssuedAwardFile = new Button();
			btnSelectMasterFile = new Button();
			lnkIssueInventorAwardsDue = new LinkLabel();
			txtMasterFile = new TextBox();
			lblMasterFile = new Label();
			lblQuarterlyIssuedAwardsFile = new Label();
			tabStep2 = new TabPage();
			btnProcessChineseAwards = new Button();
			groupBox2 = new GroupBox();
			lblChineseInventorOutput = new Label();
			txtChineseOutputFolder = new TextBox();
			btnSelectChineseOutputFolder = new Button();
			groupBox1 = new GroupBox();
			txtChineseInventor = new TextBox();
			btnSelectChineseInventors = new Button();
			lblChineseInventors = new Label();
			tabLogs = new TabPage();
			txtLogs = new RichTextBox();
			ofdSelectFile = new OpenFileDialog();
			fbdSelectFolder = new FolderBrowserDialog();
			tabIssuedAwards.SuspendLayout();
			tabSettings.SuspendLayout();
			grpOutputs.SuspendLayout();
			grpInputs.SuspendLayout();
			tabStep2.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox1.SuspendLayout();
			tabLogs.SuspendLayout();
			SuspendLayout();
			// 
			// tabIssuedAwards
			// 
			tabIssuedAwards.Controls.Add(tabSettings);
			tabIssuedAwards.Controls.Add(tabStep2);
			tabIssuedAwards.Controls.Add(tabLogs);
			tabIssuedAwards.Location = new Point(18, 14);
			tabIssuedAwards.Name = "tabIssuedAwards";
			tabIssuedAwards.SelectedIndex = 0;
			tabIssuedAwards.Size = new Size(891, 551);
			tabIssuedAwards.TabIndex = 0;
			// 
			// tabSettings
			// 
			tabSettings.Controls.Add(btnProcessAwards);
			tabSettings.Controls.Add(grpOutputs);
			tabSettings.Controls.Add(grpInputs);
			tabSettings.Location = new Point(4, 34);
			tabSettings.Name = "tabSettings";
			tabSettings.Padding = new Padding(3);
			tabSettings.Size = new Size(883, 513);
			tabSettings.TabIndex = 0;
			tabSettings.Text = "Initial Processing";
			tabSettings.UseVisualStyleBackColor = true;
			// 
			// btnProcessAwards
			// 
			btnProcessAwards.Location = new Point(630, 462);
			btnProcessAwards.Name = "btnProcessAwards";
			btnProcessAwards.Size = new Size(237, 34);
			btnProcessAwards.TabIndex = 13;
			btnProcessAwards.Text = "Process Awards";
			btnProcessAwards.UseVisualStyleBackColor = true;
			btnProcessAwards.Click += btnProcessAwards_Click;
			// 
			// grpOutputs
			// 
			grpOutputs.Controls.Add(lblOutputFolder);
			grpOutputs.Controls.Add(txtOutputFolder);
			grpOutputs.Controls.Add(btnSelectOutputFolder);
			grpOutputs.Location = new Point(13, 312);
			grpOutputs.Name = "grpOutputs";
			grpOutputs.Size = new Size(854, 125);
			grpOutputs.TabIndex = 9;
			grpOutputs.TabStop = false;
			grpOutputs.Text = "Outputs";
			// 
			// lblOutputFolder
			// 
			lblOutputFolder.AutoSize = true;
			lblOutputFolder.Location = new Point(19, 29);
			lblOutputFolder.Name = "lblOutputFolder";
			lblOutputFolder.Size = new Size(124, 25);
			lblOutputFolder.TabIndex = 7;
			lblOutputFolder.Text = "Output Folder";
			// 
			// txtOutputFolder
			// 
			txtOutputFolder.Location = new Point(19, 60);
			txtOutputFolder.Name = "txtOutputFolder";
			txtOutputFolder.Size = new Size(695, 31);
			txtOutputFolder.TabIndex = 8;
			// 
			// btnSelectOutputFolder
			// 
			btnSelectOutputFolder.Location = new Point(719, 58);
			btnSelectOutputFolder.Name = "btnSelectOutputFolder";
			btnSelectOutputFolder.Size = new Size(112, 34);
			btnSelectOutputFolder.TabIndex = 9;
			btnSelectOutputFolder.Text = "Select";
			btnSelectOutputFolder.UseVisualStyleBackColor = true;
			btnSelectOutputFolder.Click += btnSelectOutputFolder_Click;
			// 
			// grpInputs
			// 
			grpInputs.Controls.Add(btnSelectAddressFile);
			grpInputs.Controls.Add(txtInventorAddresses);
			grpInputs.Controls.Add(lblInventorAddresses);
			grpInputs.Controls.Add(txtQuarterlyIssuedAwardsFile);
			grpInputs.Controls.Add(lnkMasterFile);
			grpInputs.Controls.Add(btnSelectQuarterlyIssuedAwardFile);
			grpInputs.Controls.Add(btnSelectMasterFile);
			grpInputs.Controls.Add(lnkIssueInventorAwardsDue);
			grpInputs.Controls.Add(txtMasterFile);
			grpInputs.Controls.Add(lblMasterFile);
			grpInputs.Controls.Add(lblQuarterlyIssuedAwardsFile);
			grpInputs.Location = new Point(13, 12);
			grpInputs.Name = "grpInputs";
			grpInputs.Size = new Size(854, 278);
			grpInputs.TabIndex = 8;
			grpInputs.TabStop = false;
			grpInputs.Text = "Inputs";
			// 
			// btnSelectAddressFile
			// 
			btnSelectAddressFile.Location = new Point(723, 218);
			btnSelectAddressFile.Name = "btnSelectAddressFile";
			btnSelectAddressFile.Size = new Size(112, 34);
			btnSelectAddressFile.TabIndex = 10;
			btnSelectAddressFile.Text = "Select";
			btnSelectAddressFile.UseVisualStyleBackColor = true;
			btnSelectAddressFile.Click += btnSelectAddressFile_Click;
			// 
			// txtInventorAddresses
			// 
			txtInventorAddresses.Location = new Point(19, 220);
			txtInventorAddresses.Name = "txtInventorAddresses";
			txtInventorAddresses.Size = new Size(695, 31);
			txtInventorAddresses.TabIndex = 9;
			// 
			// lblInventorAddresses
			// 
			lblInventorAddresses.AutoSize = true;
			lblInventorAddresses.Location = new Point(19, 189);
			lblInventorAddresses.Name = "lblInventorAddresses";
			lblInventorAddresses.Size = new Size(165, 25);
			lblInventorAddresses.TabIndex = 8;
			lblInventorAddresses.Text = "Inventor Addresses";
			// 
			// txtQuarterlyIssuedAwardsFile
			// 
			txtQuarterlyIssuedAwardsFile.Location = new Point(19, 67);
			txtQuarterlyIssuedAwardsFile.Name = "txtQuarterlyIssuedAwardsFile";
			txtQuarterlyIssuedAwardsFile.Size = new Size(695, 31);
			txtQuarterlyIssuedAwardsFile.TabIndex = 1;
			// 
			// lnkMasterFile
			// 
			lnkMasterFile.AutoSize = true;
			lnkMasterFile.Location = new Point(122, 112);
			lnkMasterFile.Name = "lnkMasterFile";
			lnkMasterFile.Size = new Size(97, 25);
			lnkMasterFile.TabIndex = 7;
			lnkMasterFile.TabStop = true;
			lnkMasterFile.Text = "Master File";
			lnkMasterFile.LinkClicked += lnkMasterFile_LinkClicked;
			// 
			// btnSelectQuarterlyIssuedAwardFile
			// 
			btnSelectQuarterlyIssuedAwardFile.Location = new Point(723, 65);
			btnSelectQuarterlyIssuedAwardFile.Name = "btnSelectQuarterlyIssuedAwardFile";
			btnSelectQuarterlyIssuedAwardFile.Size = new Size(112, 34);
			btnSelectQuarterlyIssuedAwardFile.TabIndex = 2;
			btnSelectQuarterlyIssuedAwardFile.Text = "Select";
			btnSelectQuarterlyIssuedAwardFile.UseVisualStyleBackColor = true;
			btnSelectQuarterlyIssuedAwardFile.Click += btnSelectQuarterlyIssuedAwardFile_Click;
			// 
			// btnSelectMasterFile
			// 
			btnSelectMasterFile.Location = new Point(723, 141);
			btnSelectMasterFile.Name = "btnSelectMasterFile";
			btnSelectMasterFile.Size = new Size(112, 34);
			btnSelectMasterFile.TabIndex = 6;
			btnSelectMasterFile.Text = "Select";
			btnSelectMasterFile.UseVisualStyleBackColor = true;
			btnSelectMasterFile.Click += btnSelectMasterFile_Click;
			// 
			// lnkIssueInventorAwardsDue
			// 
			lnkIssueInventorAwardsDue.AutoSize = true;
			lnkIssueInventorAwardsDue.Location = new Point(261, 36);
			lnkIssueInventorAwardsDue.Name = "lnkIssueInventorAwardsDue";
			lnkIssueInventorAwardsDue.Size = new Size(224, 25);
			lnkIssueInventorAwardsDue.TabIndex = 3;
			lnkIssueInventorAwardsDue.TabStop = true;
			lnkIssueInventorAwardsDue.Text = "Issue Inventor Awards Due";
			lnkIssueInventorAwardsDue.LinkClicked += lnkIssueInventorAwardsDue_LinkClicked;
			// 
			// txtMasterFile
			// 
			txtMasterFile.Location = new Point(19, 143);
			txtMasterFile.Name = "txtMasterFile";
			txtMasterFile.Size = new Size(695, 31);
			txtMasterFile.TabIndex = 5;
			// 
			// lblMasterFile
			// 
			lblMasterFile.AutoSize = true;
			lblMasterFile.Location = new Point(19, 112);
			lblMasterFile.Name = "lblMasterFile";
			lblMasterFile.Size = new Size(97, 25);
			lblMasterFile.TabIndex = 4;
			lblMasterFile.Text = "Master File";
			// 
			// lblQuarterlyIssuedAwardsFile
			// 
			lblQuarterlyIssuedAwardsFile.AutoSize = true;
			lblQuarterlyIssuedAwardsFile.Location = new Point(19, 36);
			lblQuarterlyIssuedAwardsFile.Name = "lblQuarterlyIssuedAwardsFile";
			lblQuarterlyIssuedAwardsFile.Size = new Size(236, 25);
			lblQuarterlyIssuedAwardsFile.TabIndex = 0;
			lblQuarterlyIssuedAwardsFile.Text = "Quarterly Issued Awards File";
			// 
			// tabStep2
			// 
			tabStep2.Controls.Add(btnProcessChineseAwards);
			tabStep2.Controls.Add(groupBox2);
			tabStep2.Controls.Add(groupBox1);
			tabStep2.Location = new Point(4, 34);
			tabStep2.Name = "tabStep2";
			tabStep2.Size = new Size(883, 513);
			tabStep2.TabIndex = 2;
			tabStep2.Text = "Chinese Processing";
			tabStep2.UseVisualStyleBackColor = true;
			// 
			// btnProcessChineseAwards
			// 
			btnProcessChineseAwards.Location = new Point(633, 460);
			btnProcessChineseAwards.Name = "btnProcessChineseAwards";
			btnProcessChineseAwards.Size = new Size(237, 34);
			btnProcessChineseAwards.TabIndex = 14;
			btnProcessChineseAwards.Text = "Process Chinese Awards";
			btnProcessChineseAwards.UseVisualStyleBackColor = true;
			btnProcessChineseAwards.Click += btnProcessChineseAwards_Click;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(lblChineseInventorOutput);
			groupBox2.Controls.Add(txtChineseOutputFolder);
			groupBox2.Controls.Add(btnSelectChineseOutputFolder);
			groupBox2.Location = new Point(16, 174);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(854, 125);
			groupBox2.TabIndex = 10;
			groupBox2.TabStop = false;
			groupBox2.Text = "Outputs";
			// 
			// lblChineseInventorOutput
			// 
			lblChineseInventorOutput.AutoSize = true;
			lblChineseInventorOutput.Location = new Point(19, 29);
			lblChineseInventorOutput.Name = "lblChineseInventorOutput";
			lblChineseInventorOutput.Size = new Size(124, 25);
			lblChineseInventorOutput.TabIndex = 7;
			lblChineseInventorOutput.Text = "Output Folder";
			// 
			// txtChineseOutputFolder
			// 
			txtChineseOutputFolder.Location = new Point(19, 60);
			txtChineseOutputFolder.Name = "txtChineseOutputFolder";
			txtChineseOutputFolder.Size = new Size(695, 31);
			txtChineseOutputFolder.TabIndex = 8;
			// 
			// btnSelectChineseOutputFolder
			// 
			btnSelectChineseOutputFolder.Location = new Point(719, 58);
			btnSelectChineseOutputFolder.Name = "btnSelectChineseOutputFolder";
			btnSelectChineseOutputFolder.Size = new Size(112, 34);
			btnSelectChineseOutputFolder.TabIndex = 9;
			btnSelectChineseOutputFolder.Text = "Select";
			btnSelectChineseOutputFolder.UseVisualStyleBackColor = true;
			btnSelectChineseOutputFolder.Click += btnSelectChineseOutputFolder_Click;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(txtChineseInventor);
			groupBox1.Controls.Add(btnSelectChineseInventors);
			groupBox1.Controls.Add(lblChineseInventors);
			groupBox1.Location = new Point(16, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(854, 135);
			groupBox1.TabIndex = 9;
			groupBox1.TabStop = false;
			groupBox1.Text = "Inputs";
			// 
			// txtChineseInventor
			// 
			txtChineseInventor.Location = new Point(19, 67);
			txtChineseInventor.Name = "txtChineseInventor";
			txtChineseInventor.Size = new Size(695, 31);
			txtChineseInventor.TabIndex = 1;
			// 
			// btnSelectChineseInventors
			// 
			btnSelectChineseInventors.Location = new Point(723, 65);
			btnSelectChineseInventors.Name = "btnSelectChineseInventors";
			btnSelectChineseInventors.Size = new Size(112, 34);
			btnSelectChineseInventors.TabIndex = 2;
			btnSelectChineseInventors.Text = "Select";
			btnSelectChineseInventors.UseVisualStyleBackColor = true;
			btnSelectChineseInventors.Click += btnSelectChineseInventors_Click;
			// 
			// lblChineseInventors
			// 
			lblChineseInventors.AutoSize = true;
			lblChineseInventors.Location = new Point(19, 36);
			lblChineseInventors.Name = "lblChineseInventors";
			lblChineseInventors.Size = new Size(236, 25);
			lblChineseInventors.TabIndex = 0;
			lblChineseInventors.Text = "Quarterly Issued Awards File";
			// 
			// tabLogs
			// 
			tabLogs.Controls.Add(txtLogs);
			tabLogs.Location = new Point(4, 34);
			tabLogs.Name = "tabLogs";
			tabLogs.Padding = new Padding(3);
			tabLogs.Size = new Size(883, 513);
			tabLogs.TabIndex = 1;
			tabLogs.Text = "Logs";
			tabLogs.UseVisualStyleBackColor = true;
			// 
			// txtLogs
			// 
			txtLogs.Location = new Point(6, 6);
			txtLogs.Name = "txtLogs";
			txtLogs.ReadOnly = true;
			txtLogs.Size = new Size(871, 501);
			txtLogs.TabIndex = 0;
			txtLogs.Text = "";
			// 
			// frmInventorAwardsIssued
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(927, 584);
			Controls.Add(tabIssuedAwards);
			Name = "frmInventorAwardsIssued";
			Text = "Inventor Awards - Issued";
			tabIssuedAwards.ResumeLayout(false);
			tabSettings.ResumeLayout(false);
			grpOutputs.ResumeLayout(false);
			grpOutputs.PerformLayout();
			grpInputs.ResumeLayout(false);
			grpInputs.PerformLayout();
			tabStep2.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			tabLogs.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TabControl tabIssuedAwards;
		private TabPage tabSettings;
		private LinkLabel lnkMasterFile;
		private Button btnSelectMasterFile;
		private TextBox txtMasterFile;
		private Label lblMasterFile;
		private LinkLabel lnkIssueInventorAwardsDue;
		private Button btnSelectQuarterlyIssuedAwardFile;
		private TextBox txtQuarterlyIssuedAwardsFile;
		private Label lblQuarterlyIssuedAwardsFile;
		private TabPage tabLogs;
		private GroupBox grpInputs;
		private GroupBox grpOutputs;
		private Label lblOutputFolder;
		private TextBox txtOutputFolder;
		private Button btnSelectOutputFolder;
		private Button btnProcessAwards;
		private Button btnSelectAddressFile;
		private TextBox txtInventorAddresses;
		private Label lblInventorAddresses;
		private OpenFileDialog ofdSelectFile;
		private FolderBrowserDialog fbdSelectFolder;
		private RichTextBox txtLogs;
		private TabPage tabStep2;
		private Button btnProcessChineseAwards;
		private GroupBox groupBox2;
		private Label lblChineseInventorOutput;
		private TextBox txtChineseOutputFolder;
		private Button btnSelectChineseOutputFolder;
		private GroupBox groupBox1;
		private TextBox txtChineseInventor;
		private Button btnSelectChineseInventors;
		private Label lblChineseInventors;
	}
}