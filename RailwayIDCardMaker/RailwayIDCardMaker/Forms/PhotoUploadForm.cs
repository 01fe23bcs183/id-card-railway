using System;
using System.Drawing;
using System.Windows.Forms;

namespace RailwayIDCardMaker.Forms
{
    /// <summary>
    /// Form for uploading photos from disk
    /// </summary>
    public partial class PhotoUploadForm : Form
    {
        public Image SelectedPhoto { get; private set; }
        public bool PhotoNotAvailable { get; private set; }

        public PhotoUploadForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Photo";
                dialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All Files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        SelectedPhoto = Image.FromFile(dialog.FileName);
                        picPreview.Image = SelectedPhoto;
                        txtFilePath.Text = dialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            picPreview.Image = null;
            txtFilePath.Clear();
            SelectedPhoto = null;
        }

        private void chkPhotoNotAvailable_CheckedChanged(object sender, EventArgs e)
        {
            PhotoNotAvailable = chkPhotoNotAvailable.Checked;
            btnBrowse.Enabled = !PhotoNotAvailable;
            btnClear.Enabled = !PhotoNotAvailable;

            if (PhotoNotAvailable)
            {
                btnClear_Click(sender, e);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!PhotoNotAvailable && SelectedPhoto == null)
            {
                MessageBox.Show("Please select a photo or check 'Photo Not Available'",
                    "Photo Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
