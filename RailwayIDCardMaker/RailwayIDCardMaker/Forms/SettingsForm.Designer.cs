namespace RailwayIDCardMaker.Forms
{
    partial class SettingsForm
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
            this.grpDefaults = new System.Windows.Forms.GroupBox();
            this.numValidityYears = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDefaultUnit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDefaultZone = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAuthorityDesignation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIssuingAuthority = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpPrinter = new System.Windows.Forms.GroupBox();
            this.chkPrintBothSides = new System.Windows.Forms.CheckBox();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblLastSerial = new System.Windows.Forms.Label();
            this.grpDefaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValidityYears)).BeginInit();
            this.grpPrinter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDefaults
            // 
            this.grpDefaults.Controls.Add(this.numValidityYears);
            this.grpDefaults.Controls.Add(this.label5);
            this.grpDefaults.Controls.Add(this.txtDefaultUnit);
            this.grpDefaults.Controls.Add(this.label4);
            this.grpDefaults.Controls.Add(this.cmbDefaultZone);
            this.grpDefaults.Controls.Add(this.label3);
            this.grpDefaults.Controls.Add(this.txtAuthorityDesignation);
            this.grpDefaults.Controls.Add(this.label2);
            this.grpDefaults.Controls.Add(this.txtIssuingAuthority);
            this.grpDefaults.Controls.Add(this.label1);
            this.grpDefaults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDefaults.Location = new System.Drawing.Point(15, 15);
            this.grpDefaults.Size = new System.Drawing.Size(420, 180);
            this.grpDefaults.Text = "Default Values";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Text = "Issuing Authority:";
            // 
            // txtIssuingAuthority
            // 
            this.txtIssuingAuthority.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtIssuingAuthority.Location = new System.Drawing.Point(130, 27);
            this.txtIssuingAuthority.Size = new System.Drawing.Size(270, 23);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Text = "Authority Designation:";
            // 
            // txtAuthorityDesignation
            // 
            this.txtAuthorityDesignation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAuthorityDesignation.Location = new System.Drawing.Point(150, 57);
            this.txtAuthorityDesignation.Size = new System.Drawing.Size(250, 23);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(15, 90);
            this.label3.Text = "Default Zone:";
            // 
            // cmbDefaultZone
            // 
            this.cmbDefaultZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultZone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbDefaultZone.Location = new System.Drawing.Point(130, 87);
            this.cmbDefaultZone.Size = new System.Drawing.Size(270, 23);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(15, 120);
            this.label4.Text = "Default Unit Code:";
            // 
            // txtDefaultUnit
            // 
            this.txtDefaultUnit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDefaultUnit.Location = new System.Drawing.Point(130, 117);
            this.txtDefaultUnit.MaxLength = 2;
            this.txtDefaultUnit.Size = new System.Drawing.Size(50, 23);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(200, 120);
            this.label5.Text = "Card Validity (Years):";
            // 
            // numValidityYears
            // 
            this.numValidityYears.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numValidityYears.Location = new System.Drawing.Point(330, 117);
            this.numValidityYears.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numValidityYears.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numValidityYears.Size = new System.Drawing.Size(60, 23);
            this.numValidityYears.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // grpPrinter
            // 
            this.grpPrinter.Controls.Add(this.chkPrintBothSides);
            this.grpPrinter.Controls.Add(this.cmbPrinter);
            this.grpPrinter.Controls.Add(this.label6);
            this.grpPrinter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpPrinter.Location = new System.Drawing.Point(15, 200);
            this.grpPrinter.Size = new System.Drawing.Size(420, 90);
            this.grpPrinter.Text = "Printer Settings";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(15, 30);
            this.label6.Text = "Default Printer:";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPrinter.Location = new System.Drawing.Point(120, 27);
            this.cmbPrinter.Size = new System.Drawing.Size(280, 23);
            // 
            // chkPrintBothSides
            // 
            this.chkPrintBothSides.AutoSize = true;
            this.chkPrintBothSides.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkPrintBothSides.Location = new System.Drawing.Point(18, 58);
            this.chkPrintBothSides.Text = "Print both sides (Front and Back)";
            // 
            // lblLastSerial
            // 
            this.lblLastSerial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblLastSerial.ForeColor = System.Drawing.Color.Gray;
            this.lblLastSerial.Location = new System.Drawing.Point(15, 300);
            this.lblLastSerial.Size = new System.Drawing.Size(200, 20);
            this.lblLastSerial.Text = "Last Serial: 0";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 102, 51);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(240, 330);
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 330);
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(450, 380);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblLastSerial);
            this.Controls.Add(this.grpPrinter);
            this.Controls.Add(this.grpDefaults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.grpDefaults.ResumeLayout(false);
            this.grpDefaults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValidityYears)).EndInit();
            this.grpPrinter.ResumeLayout(false);
            this.grpPrinter.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpDefaults;
        private System.Windows.Forms.GroupBox grpPrinter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIssuingAuthority;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAuthorityDesignation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDefaultZone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDefaultUnit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numValidityYears;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.CheckBox chkPrintBothSides;
        private System.Windows.Forms.Label lblLastSerial;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
