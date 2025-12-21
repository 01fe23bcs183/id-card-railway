using System;
using System.Windows.Forms;
using RailwayIDCardMaker.Utils;

namespace RailwayIDCardMaker.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = $"Version: {Constants.APP_VERSION}";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
