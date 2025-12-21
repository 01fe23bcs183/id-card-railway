namespace RailwayIDCardMaker.Forms
{
    partial class PhotoUploadForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.chkPhotoNotAvailable = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.grpControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPreview
            // 
            this.grpPreview.Controls.Add(this.picPreview);
            this.grpPreview.Location = new System.Drawing.Point(12, 12);
            this.grpPreview.Size = new System.Drawing.Size(360, 280);
            this.grpPreview.Text = "Photo Preview";
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.LightGray;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(3, 19);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.chkPhotoNotAvailable);
            this.grpControls.Controls.Add(this.btnClear);
            this.grpControls.Controls.Add(this.btnBrowse);
            this.grpControls.Controls.Add(this.txtFilePath);
            this.grpControls.Controls.Add(this.lblFile);
            this.grpControls.Location = new System.Drawing.Point(12, 298);
            this.grpControls.Size = new System.Drawing.Size(360, 100);
            this.grpControls.Text = "Photo Selection";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(10, 25);
            this.lblFile.Text = "File:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(50, 22);
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(295, 23);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(50, 55);
            this.btnBrowse.Size = new System.Drawing.Size(90, 30);
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(150, 55);
            this.btnClear.Size = new System.Drawing.Size(90, 30);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkPhotoNotAvailable
            // 
            this.chkPhotoNotAvailable.AutoSize = true;
            this.chkPhotoNotAvailable.Location = new System.Drawing.Point(255, 62);
            this.chkPhotoNotAvailable.Text = "Photo Not Available";
            this.chkPhotoNotAvailable.CheckedChanged += new System.EventHandler(this.chkPhotoNotAvailable_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(0, 102, 51);
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(115, 410);
            this.btnOK.Size = new System.Drawing.Size(100, 35);
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(225, 410);
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PhotoUploadForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpControls);
            this.Controls.Add(this.grpPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PhotoUploadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upload Photo";
            this.grpPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.grpControls.ResumeLayout(false);
            this.grpControls.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkPhotoNotAvailable;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
