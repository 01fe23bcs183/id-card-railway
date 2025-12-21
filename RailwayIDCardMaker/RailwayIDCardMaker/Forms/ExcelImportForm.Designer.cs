namespace RailwayIDCardMaker.Forms
{
    partial class ExcelImportForm
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
            this.grpFile = new System.Windows.Forms.GroupBox();
            this.btnDownloadTemplate = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.grpFile.SuspendLayout();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFile
            // 
            this.grpFile.Controls.Add(this.btnDownloadTemplate);
            this.grpFile.Controls.Add(this.btnPreview);
            this.grpFile.Controls.Add(this.btnBrowse);
            this.grpFile.Controls.Add(this.txtFilePath);
            this.grpFile.Controls.Add(this.lblFile);
            this.grpFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFile.Location = new System.Drawing.Point(0, 0);
            this.grpFile.Padding = new System.Windows.Forms.Padding(10);
            this.grpFile.Size = new System.Drawing.Size(800, 85);
            this.grpFile.Text = "Excel File Selection";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(13, 25);
            this.lblFile.Text = "Excel File:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(85, 22);
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(500, 23);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(590, 20);
            this.btnBrowse.Size = new System.Drawing.Size(90, 27);
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
            this.btnPreview.Enabled = false;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(685, 20);
            this.btnPreview.Size = new System.Drawing.Size(100, 27);
            this.btnPreview.Text = "Preview Data";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnDownloadTemplate
            // 
            this.btnDownloadTemplate.Location = new System.Drawing.Point(85, 52);
            this.btnDownloadTemplate.Size = new System.Drawing.Size(150, 25);
            this.btnDownloadTemplate.Text = "📥 Download Template";
            this.btnDownloadTemplate.Click += new System.EventHandler(this.btnDownloadTemplate_Click);
            // 
            // grpPreview
            // 
            this.grpPreview.Controls.Add(this.dgvPreview);
            this.grpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPreview.Location = new System.Drawing.Point(0, 85);
            this.grpPreview.Padding = new System.Windows.Forms.Padding(10);
            this.grpPreview.Size = new System.Drawing.Size(800, 365);
            this.grpPreview.Text = "Data Preview";
            // 
            // dgvPreview
            // 
            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.AllowUserToDeleteRows = false;
            this.dgvPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPreview.Location = new System.Drawing.Point(10, 26);
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.lblStatus);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnImport);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 450);
            this.pnlButtons.Size = new System.Drawing.Size(800, 50);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Location = new System.Drawing.Point(12, 17);
            this.lblStatus.Text = "Select an Excel file to import";
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(0, 102, 51);
            this.btnImport.Enabled = false;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(560, 8);
            this.btnImport.Size = new System.Drawing.Size(110, 35);
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(680, 8);
            this.btnCancel.Size = new System.Drawing.Size(110, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ExcelImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpFile);
            this.MinimumSize = new System.Drawing.Size(816, 539);
            this.Name = "ExcelImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import from Excel";
            this.grpFile.ResumeLayout(false);
            this.grpFile.PerformLayout();
            this.grpPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpFile;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnDownloadTemplate;
        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnCancel;
    }
}
