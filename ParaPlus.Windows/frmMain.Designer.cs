namespace ParaPlus.Windows
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			menuMainMenu = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			quitToolStripMenuItem = new ToolStripMenuItem();
			actionsToolStripMenuItem = new ToolStripMenuItem();
			inventorAwardsToolStripMenuItem = new ToolStripMenuItem();
			issuedToolStripMenuItem = new ToolStripMenuItem();
			filedToolStripMenuItem = new ToolStripMenuItem();
			quarterlyOnePagersToolStripMenuItem = new ToolStripMenuItem();
			menuMainMenu.SuspendLayout();
			SuspendLayout();
			// 
			// menuMainMenu
			// 
			menuMainMenu.ImageScalingSize = new Size(24, 24);
			menuMainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, actionsToolStripMenuItem });
			menuMainMenu.Location = new Point(0, 0);
			menuMainMenu.Name = "menuMainMenu";
			menuMainMenu.Size = new Size(1108, 33);
			menuMainMenu.TabIndex = 1;
			menuMainMenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { quitToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new Size(54, 29);
			fileToolStripMenuItem.Text = "File";
			// 
			// quitToolStripMenuItem
			// 
			quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			quitToolStripMenuItem.Size = new Size(270, 34);
			quitToolStripMenuItem.Text = "Quit";
			quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
			// 
			// actionsToolStripMenuItem
			// 
			actionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { inventorAwardsToolStripMenuItem, quarterlyOnePagersToolStripMenuItem });
			actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
			actionsToolStripMenuItem.Size = new Size(87, 29);
			actionsToolStripMenuItem.Text = "Actions";
			// 
			// inventorAwardsToolStripMenuItem
			// 
			inventorAwardsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { issuedToolStripMenuItem, filedToolStripMenuItem });
			inventorAwardsToolStripMenuItem.Name = "inventorAwardsToolStripMenuItem";
			inventorAwardsToolStripMenuItem.Size = new Size(282, 34);
			inventorAwardsToolStripMenuItem.Text = "Inventor Awards";
			// 
			// issuedToolStripMenuItem
			// 
			issuedToolStripMenuItem.Name = "issuedToolStripMenuItem";
			issuedToolStripMenuItem.Size = new Size(270, 34);
			issuedToolStripMenuItem.Text = "Issued";
			issuedToolStripMenuItem.Click += issuedToolStripMenuItem_Click;
			// 
			// filedToolStripMenuItem
			// 
			filedToolStripMenuItem.Name = "filedToolStripMenuItem";
			filedToolStripMenuItem.Size = new Size(270, 34);
			filedToolStripMenuItem.Text = "Filed";
			// 
			// quarterlyOnePagersToolStripMenuItem
			// 
			quarterlyOnePagersToolStripMenuItem.Name = "quarterlyOnePagersToolStripMenuItem";
			quarterlyOnePagersToolStripMenuItem.Size = new Size(282, 34);
			quarterlyOnePagersToolStripMenuItem.Text = "Quarterly One Pagers";
			quarterlyOnePagersToolStripMenuItem.Click += quarterlyOnePagersToolStripMenuItem_Click;
			// 
			// frmMain
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1108, 949);
			Controls.Add(menuMainMenu);
			ForeColor = SystemColors.ControlText;
			IsMdiContainer = true;
			MainMenuStrip = menuMainMenu;
			Name = "frmMain";
			Text = "ParaPlus";
			menuMainMenu.ResumeLayout(false);
			menuMainMenu.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private MenuStrip menuMainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem actionsToolStripMenuItem;
        private ToolStripMenuItem inventorAwardsToolStripMenuItem;
        private ToolStripMenuItem issuedToolStripMenuItem;
        private ToolStripMenuItem filedToolStripMenuItem;
        private ToolStripMenuItem quarterlyOnePagersToolStripMenuItem;
    }
}
