namespace RailwayIDCardMaker.Forms
{
    partial class WebcamCaptureForm
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
            this.grpCamera = new System.Windows.Forms.GroupBox();
            this.picWebcam = new System.Windows.Forms.PictureBox();
            this.pnlCameraControls = new System.Windows.Forms.Panel();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.cmbCameras = new System.Windows.Forms.ComboBox();
            this.lblCamera = new System.Windows.Forms.Label();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWebcam)).BeginInit();
            this.pnlCameraControls.SuspendLayout();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCamera
            // 
            this.grpCamera.Controls.Add(this.picWebcam);
            this.grpCamera.Controls.Add(this.pnlCameraControls);
            this.grpCamera.Location = new System.Drawing.Point(12, 12);
            this.grpCamera.Size = new System.Drawing.Size(500, 400);
            this.grpCamera.Text = "Camera Feed";
            // 
            // picWebcam
            // 
            this.picWebcam.BackColor = System.Drawing.Color.Black;
            this.picWebcam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWebcam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWebcam.Location = new System.Drawing.Point(3, 70);
            this.picWebcam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // pnlCameraControls
            // 
            this.pnlCameraControls.Controls.Add(this.btnCapture);
            this.pnlCameraControls.Controls.Add(this.btnStop);
            this.pnlCameraControls.Controls.Add(this.btnStart);
            this.pnlCameraControls.Controls.Add(this.cmbCameras);
            this.pnlCameraControls.Controls.Add(this.lblCamera);
            this.pnlCameraControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCameraControls.Location = new System.Drawing.Point(3, 19);
            this.pnlCameraControls.Size = new System.Drawing.Size(494, 51);
            // 
            // lblCamera
            // 
            this.lblCamera.AutoSize = true;
            this.lblCamera.Location = new System.Drawing.Point(5, 10);
            this.lblCamera.Text = "Camera:";
            // 
            // cmbCameras
            // 
            this.cmbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCameras.Location = new System.Drawing.Point(65, 7);
            this.cmbCameras.Size = new System.Drawing.Size(250, 23);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(0, 102, 51);
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(325, 5);
            this.btnStart.Size = new System.Drawing.Size(80, 28);
            this.btnStart.Text = "▶ Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(153, 0, 0);
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(325, 5);
            this.btnStop.Size = new System.Drawing.Size(80, 28);
            this.btnStop.Text = "⏹ Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnCapture.Enabled = false;
            this.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCapture.ForeColor = System.Drawing.Color.White;
            this.btnCapture.Location = new System.Drawing.Point(410, 5);
            this.btnCapture.Size = new System.Drawing.Size(80, 28);
            this.btnCapture.Text = "📷 Capture";
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // grpPreview
            // 
            this.grpPreview.Controls.Add(this.picPreview);
            this.grpPreview.Location = new System.Drawing.Point(520, 12);
            this.grpPreview.Size = new System.Drawing.Size(260, 400);
            this.grpPreview.Text = "Captured Photo";
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.LightGray;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(3, 19);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(0, 102, 51);
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(320, 425);
            this.btnOK.Size = new System.Drawing.Size(100, 35);
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(430, 425);
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WebcamCaptureForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(792, 472);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this.grpCamera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "WebcamCaptureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Webcam Capture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebcamCaptureForm_FormClosing);
            this.Load += new System.EventHandler(this.WebcamCaptureForm_Load);
            this.grpCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWebcam)).EndInit();
            this.pnlCameraControls.ResumeLayout(false);
            this.pnlCameraControls.PerformLayout();
            this.grpPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpCamera;
        private System.Windows.Forms.PictureBox picWebcam;
        private System.Windows.Forms.Panel pnlCameraControls;
        private System.Windows.Forms.Label lblCamera;
        private System.Windows.Forms.ComboBox cmbCameras;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
