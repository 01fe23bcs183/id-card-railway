namespace RailwayIDCardMaker.Forms
{
    partial class BackupRestoreForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.grpBackup = new System.Windows.Forms.GroupBox();
            this.lblBackupInfo = new System.Windows.Forms.Label();
            this.btnBackup = new System.Windows.Forms.Button();
            this.grpRestore = new System.Windows.Forms.GroupBox();
            this.lblRestoreInfo = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.grpBackup.SuspendLayout();
            this.grpRestore.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(500, 70);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 10);
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(500, 35);
            this.lblTitle.Text = "Backup & Restore";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gold;
            this.lblSubtitle.Location = new System.Drawing.Point(0, 45);
            this.lblSubtitle.Size = new System.Drawing.Size(500, 25);
            this.lblSubtitle.Text = "Database Management";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBackup
            // 
            this.grpBackup.Controls.Add(this.lblBackupInfo);
            this.grpBackup.Controls.Add(this.btnBackup);
            this.grpBackup.Location = new System.Drawing.Point(20, 85);
            this.grpBackup.Padding = new System.Windows.Forms.Padding(15);
            this.grpBackup.Size = new System.Drawing.Size(460, 120);
            this.grpBackup.Text = "Create Backup";
            // 
            // lblBackupInfo
            // 
            this.lblBackupInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblBackupInfo.Location = new System.Drawing.Point(15, 25);
            this.lblBackupInfo.Size = new System.Drawing.Size(430, 50);
            this.lblBackupInfo.Text = "Create a backup copy of your database. This will save all employee records, settings, and configuration to a file that you can restore later.";
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.Color.FromArgb(0, 102, 51);
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackup.ForeColor = System.Drawing.Color.White;
            this.btnBackup.Location = new System.Drawing.Point(150, 75);
            this.btnBackup.Size = new System.Drawing.Size(160, 35);
            this.btnBackup.Text = "💾 Create Backup";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // grpRestore
            // 
            this.grpRestore.Controls.Add(this.lblRestoreInfo);
            this.grpRestore.Controls.Add(this.btnRestore);
            this.grpRestore.Location = new System.Drawing.Point(20, 215);
            this.grpRestore.Padding = new System.Windows.Forms.Padding(15);
            this.grpRestore.Size = new System.Drawing.Size(460, 120);
            this.grpRestore.Text = "Restore from Backup";
            // 
            // lblRestoreInfo
            // 
            this.lblRestoreInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblRestoreInfo.Location = new System.Drawing.Point(15, 25);
            this.lblRestoreInfo.Size = new System.Drawing.Size(430, 50);
            this.lblRestoreInfo.Text = "Restore your database from a previously created backup file. WARNING: This will replace all current data with the data from the backup file.";
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(153, 51, 0);
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(150, 75);
            this.btnRestore.Size = new System.Drawing.Size(160, 35);
            this.btnRestore.Text = "📂 Restore Backup";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblStatus);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 345);
            this.pnlBottom.Size = new System.Drawing.Size(500, 55);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Location = new System.Drawing.Point(20, 18);
            this.lblStatus.Text = "Ready";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(380, 10);
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BackupRestoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpRestore);
            this.Controls.Add(this.grpBackup);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackupRestoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Backup & Restore";
            this.pnlHeader.ResumeLayout(false);
            this.grpBackup.ResumeLayout(false);
            this.grpRestore.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.GroupBox grpBackup;
        private System.Windows.Forms.Label lblBackupInfo;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.GroupBox grpRestore;
        private System.Windows.Forms.Label lblRestoreInfo;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnClose;
    }
}
