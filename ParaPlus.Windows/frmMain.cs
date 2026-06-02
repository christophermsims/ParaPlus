using System.Runtime.CompilerServices;

namespace ParaPlus.Windows
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void quarterlyOnePagersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmQuarterlyOnePagers quarterlyOnePagers = new frmQuarterlyOnePagers();
			quarterlyOnePagers.MdiParent = this;
			quarterlyOnePagers.Show();
		}

		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var child in this.MdiChildren)
			{
				child.Close();
			}

			this.Close();
		}

		private void issuedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmInventorAwardsIssued inventorAwardsIssued = new frmInventorAwardsIssued();
			inventorAwardsIssued.MdiParent = this;
			inventorAwardsIssued.Show();
		}
	}
}
