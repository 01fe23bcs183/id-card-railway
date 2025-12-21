namespace RailwayIDCardMaker.Forms
{
    partial class EmployeeForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlFormContainer = new System.Windows.Forms.Panel();
            this.grpCardInfo = new System.Windows.Forms.GroupBox();
            this.btnGenerateID = new System.Windows.Forms.Button();
            this.txtIDCardNumber = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtIssuingAuthorityDesig = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtIssuingAuthority = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.dtpValidityDate = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.dtpDateOfIssue = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.grpEmployment = new System.Windows.Forms.GroupBox();
            this.dtpDateOfRetirement = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpDateOfJoining = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPFNumber = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbZone = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPlaceOfPosting = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDesignation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grpPersonal = new System.Windows.Forms.GroupBox();
            this.txtAadhaar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBloodGroup = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFatherName = new System.Windows.Forms.TextBox();
            this.lblFatherName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.grpRemarks = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSaveAndPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpCardPreview = new System.Windows.Forms.GroupBox();
            this.lblBack = new System.Windows.Forms.Label();
            this.lblFront = new System.Windows.Forms.Label();
            this.picCardBack = new System.Windows.Forms.PictureBox();
            this.picCardFront = new System.Windows.Forms.PictureBox();
            this.grpSignature = new System.Windows.Forms.GroupBox();
            this.btnClearSignature = new System.Windows.Forms.Button();
            this.btnBrowseSignature = new System.Windows.Forms.Button();
            this.picSignature = new System.Windows.Forms.PictureBox();
            this.grpPhoto = new System.Windows.Forms.GroupBox();
            this.btnClearPhoto = new System.Windows.Forms.Button();
            this.btnBrowsePhoto = new System.Windows.Forms.Button();
            this.btnCameraCapture = new System.Windows.Forms.Button();
            this.picPhoto = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlFormContainer.SuspendLayout();
            this.grpCardInfo.SuspendLayout();
            this.grpEmployment.SuspendLayout();
            this.grpPersonal.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.grpRemarks.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.grpCardPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCardBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCardFront)).BeginInit();
            this.grpSignature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).BeginInit();
            this.grpPhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.pnlFormContainer);
            this.splitContainer.Panel2.Controls.Add(this.pnlPreview);
            this.splitContainer.Size = new System.Drawing.Size(1100, 650);
            this.splitContainer.SplitterDistance = 550;
            this.splitContainer.TabIndex = 0;
            // 
            // pnlFormContainer
            // 
            this.pnlFormContainer.AutoScroll = true;
            this.pnlFormContainer.Controls.Add(this.grpCardInfo);
            this.pnlFormContainer.Controls.Add(this.grpEmployment);
            this.pnlFormContainer.Controls.Add(this.grpPersonal);
            this.pnlFormContainer.Controls.Add(this.lblFormTitle);
            this.pnlFormContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlFormContainer.Name = "pnlFormContainer";
            this.pnlFormContainer.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFormContainer.Size = new System.Drawing.Size(550, 650);
            this.pnlFormContainer.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(10, 10);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(530, 40);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "New Employee ID Card";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpPersonal
            // 
            this.grpPersonal.Controls.Add(this.txtAadhaar);
            this.grpPersonal.Controls.Add(this.label7);
            this.grpPersonal.Controls.Add(this.txtMobile);
            this.grpPersonal.Controls.Add(this.label6);
            this.grpPersonal.Controls.Add(this.txtAddress);
            this.grpPersonal.Controls.Add(this.label5);
            this.grpPersonal.Controls.Add(this.cmbGender);
            this.grpPersonal.Controls.Add(this.label4);
            this.grpPersonal.Controls.Add(this.cmbBloodGroup);
            this.grpPersonal.Controls.Add(this.label3);
            this.grpPersonal.Controls.Add(this.dtpDateOfBirth);
            this.grpPersonal.Controls.Add(this.label2);
            this.grpPersonal.Controls.Add(this.txtFatherName);
            this.grpPersonal.Controls.Add(this.lblFatherName);
            this.grpPersonal.Controls.Add(this.txtName);
            this.grpPersonal.Controls.Add(this.label1);
            this.grpPersonal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpPersonal.Location = new System.Drawing.Point(13, 55);
            this.grpPersonal.Name = "grpPersonal";
            this.grpPersonal.Size = new System.Drawing.Size(520, 200);
            this.grpPersonal.TabIndex = 1;
            this.grpPersonal.TabStop = false;
            this.grpPersonal.Text = "Personal Information";
            // 
            // label1 - Name
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.Text = "Name:*";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtName.Location = new System.Drawing.Point(100, 22);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 23);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblFatherName
            // 
            this.lblFatherName.AutoSize = true;
            this.lblFatherName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFatherName.Location = new System.Drawing.Point(10, 55);
            this.lblFatherName.Name = "lblFatherName";
            this.lblFatherName.Size = new System.Drawing.Size(80, 15);
            this.lblFatherName.Text = "Father Name:";
            // 
            // txtFatherName
            // 
            this.txtFatherName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFatherName.Location = new System.Drawing.Point(100, 52);
            this.txtFatherName.Name = "txtFatherName";
            this.txtFatherName.Size = new System.Drawing.Size(200, 23);
            this.txtFatherName.TabIndex = 3;
            // 
            // label2 - DOB
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(310, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.Text = "Date of Birth:";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(395, 22);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(115, 23);
            this.dtpDateOfBirth.TabIndex = 2;
            // 
            // label3 - Blood Group
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(310, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.Text = "Blood Group:";
            // 
            // cmbBloodGroup
            // 
            this.cmbBloodGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBloodGroup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbBloodGroup.Location = new System.Drawing.Point(395, 52);
            this.cmbBloodGroup.Name = "cmbBloodGroup";
            this.cmbBloodGroup.Size = new System.Drawing.Size(70, 23);
            this.cmbBloodGroup.TabIndex = 4;
            this.cmbBloodGroup.SelectedIndexChanged += new System.EventHandler(this.cmbBloodGroup_SelectedIndexChanged);
            // 
            // label4 - Gender
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(460, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.Text = "Gender:";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbGender.Location = new System.Drawing.Point(460, 158);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(50, 23);
            this.cmbGender.TabIndex = 9;
            this.cmbGender.Visible = true;
            // 
            // label5 - Address
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(10, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.Text = "Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAddress.Location = new System.Drawing.Point(100, 82);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(410, 45);
            this.txtAddress.TabIndex = 6;
            // 
            // label6 - Mobile
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(10, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 15);
            this.label6.Text = "Mobile:";
            // 
            // txtMobile
            // 
            this.txtMobile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMobile.Location = new System.Drawing.Point(100, 137);
            this.txtMobile.MaxLength = 10;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(120, 23);
            this.txtMobile.TabIndex = 7;
            // 
            // label7 - Aadhaar
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.Location = new System.Drawing.Point(240, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.Text = "Aadhaar:";
            // 
            // txtAadhaar
            // 
            this.txtAadhaar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAadhaar.Location = new System.Drawing.Point(300, 137);
            this.txtAadhaar.MaxLength = 14;
            this.txtAadhaar.Name = "txtAadhaar";
            this.txtAadhaar.Size = new System.Drawing.Size(150, 23);
            this.txtAadhaar.TabIndex = 8;
            // 
            // grpEmployment
            // 
            this.grpEmployment.Controls.Add(this.dtpDateOfRetirement);
            this.grpEmployment.Controls.Add(this.label15);
            this.grpEmployment.Controls.Add(this.dtpDateOfJoining);
            this.grpEmployment.Controls.Add(this.label14);
            this.grpEmployment.Controls.Add(this.txtPFNumber);
            this.grpEmployment.Controls.Add(this.label13);
            this.grpEmployment.Controls.Add(this.txtUnit);
            this.grpEmployment.Controls.Add(this.label12);
            this.grpEmployment.Controls.Add(this.cmbZone);
            this.grpEmployment.Controls.Add(this.label11);
            this.grpEmployment.Controls.Add(this.txtPlaceOfPosting);
            this.grpEmployment.Controls.Add(this.label10);
            this.grpEmployment.Controls.Add(this.cmbDepartment);
            this.grpEmployment.Controls.Add(this.label9);
            this.grpEmployment.Controls.Add(this.txtDesignation);
            this.grpEmployment.Controls.Add(this.label8);
            this.grpEmployment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpEmployment.Location = new System.Drawing.Point(13, 260);
            this.grpEmployment.Name = "grpEmployment";
            this.grpEmployment.Size = new System.Drawing.Size(520, 170);
            this.grpEmployment.TabIndex = 2;
            this.grpEmployment.TabStop = false;
            this.grpEmployment.Text = "Employment Information";
            // 
            // label8 - Designation
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.Location = new System.Drawing.Point(10, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 15);
            this.label8.Text = "Designation:*";
            // 
            // txtDesignation
            // 
            this.txtDesignation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDesignation.Location = new System.Drawing.Point(100, 22);
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Size = new System.Drawing.Size(200, 23);
            this.txtDesignation.TabIndex = 1;
            // 
            // label9 - Department
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label9.Location = new System.Drawing.Point(310, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 15);
            this.label9.Text = "Department:*";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbDepartment.Location = new System.Drawing.Point(395, 22);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(115, 23);
            this.cmbDepartment.TabIndex = 2;
            this.cmbDepartment.SelectedIndexChanged += new System.EventHandler(this.cmbDepartment_SelectedIndexChanged);
            // 
            // label10 - Place
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label10.Location = new System.Drawing.Point(10, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 15);
            this.label10.Text = "Place of Posting:";
            // 
            // txtPlaceOfPosting
            // 
            this.txtPlaceOfPosting.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPlaceOfPosting.Location = new System.Drawing.Point(100, 52);
            this.txtPlaceOfPosting.Name = "txtPlaceOfPosting";
            this.txtPlaceOfPosting.Size = new System.Drawing.Size(200, 23);
            this.txtPlaceOfPosting.TabIndex = 3;
            // 
            // label11 - Zone
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label11.Location = new System.Drawing.Point(10, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 15);
            this.label11.Text = "Zone:*";
            // 
            // cmbZone
            // 
            this.cmbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbZone.Location = new System.Drawing.Point(100, 82);
            this.cmbZone.Name = "cmbZone";
            this.cmbZone.Size = new System.Drawing.Size(250, 23);
            this.cmbZone.TabIndex = 4;
            // 
            // label12 - Unit
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label12.Location = new System.Drawing.Point(360, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 15);
            this.label12.Text = "Unit Code:";
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUnit.Location = new System.Drawing.Point(430, 82);
            this.txtUnit.MaxLength = 2;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(50, 23);
            this.txtUnit.TabIndex = 5;
            // 
            // label13 - PF
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label13.Location = new System.Drawing.Point(310, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 15);
            this.label13.Text = "PF Number:";
            // 
            // txtPFNumber
            // 
            this.txtPFNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPFNumber.Location = new System.Drawing.Point(395, 52);
            this.txtPFNumber.Name = "txtPFNumber";
            this.txtPFNumber.Size = new System.Drawing.Size(115, 23);
            this.txtPFNumber.TabIndex = 6;
            // 
            // label14 - Joining
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label14.Location = new System.Drawing.Point(10, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 15);
            this.label14.Text = "Date of Joining:";
            // 
            // dtpDateOfJoining
            // 
            this.dtpDateOfJoining.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDateOfJoining.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfJoining.Location = new System.Drawing.Point(100, 112);
            this.dtpDateOfJoining.Name = "dtpDateOfJoining";
            this.dtpDateOfJoining.Size = new System.Drawing.Size(115, 23);
            this.dtpDateOfJoining.TabIndex = 7;
            // 
            // label15 - Retirement
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label15.Location = new System.Drawing.Point(240, 115);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 15);
            this.label15.Text = "Date of Retirement:";
            // 
            // dtpDateOfRetirement
            // 
            this.dtpDateOfRetirement.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDateOfRetirement.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfRetirement.Location = new System.Drawing.Point(350, 112);
            this.dtpDateOfRetirement.Name = "dtpDateOfRetirement";
            this.dtpDateOfRetirement.Size = new System.Drawing.Size(115, 23);
            this.dtpDateOfRetirement.TabIndex = 8;
            // 
            // grpCardInfo
            // 
            this.grpCardInfo.Controls.Add(this.btnGenerateID);
            this.grpCardInfo.Controls.Add(this.txtIDCardNumber);
            this.grpCardInfo.Controls.Add(this.label20);
            this.grpCardInfo.Controls.Add(this.txtIssuingAuthorityDesig);
            this.grpCardInfo.Controls.Add(this.label19);
            this.grpCardInfo.Controls.Add(this.txtIssuingAuthority);
            this.grpCardInfo.Controls.Add(this.label18);
            this.grpCardInfo.Controls.Add(this.dtpValidityDate);
            this.grpCardInfo.Controls.Add(this.label17);
            this.grpCardInfo.Controls.Add(this.dtpDateOfIssue);
            this.grpCardInfo.Controls.Add(this.label16);
            this.grpCardInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpCardInfo.Location = new System.Drawing.Point(13, 435);
            this.grpCardInfo.Name = "grpCardInfo";
            this.grpCardInfo.Size = new System.Drawing.Size(520, 140);
            this.grpCardInfo.TabIndex = 3;
            this.grpCardInfo.TabStop = false;
            this.grpCardInfo.Text = "ID Card Information";
            // 
            // label16 - Issue Date
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label16.Location = new System.Drawing.Point(10, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 15);
            this.label16.Text = "Date of Issue:";
            // 
            // dtpDateOfIssue
            // 
            this.dtpDateOfIssue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDateOfIssue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfIssue.Location = new System.Drawing.Point(100, 22);
            this.dtpDateOfIssue.Name = "dtpDateOfIssue";
            this.dtpDateOfIssue.Size = new System.Drawing.Size(115, 23);
            this.dtpDateOfIssue.TabIndex = 1;
            // 
            // label17 - Validity
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label17.Location = new System.Drawing.Point(240, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 15);
            this.label17.Text = "Valid Till:";
            // 
            // dtpValidityDate
            // 
            this.dtpValidityDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpValidityDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpValidityDate.Location = new System.Drawing.Point(300, 22);
            this.dtpValidityDate.Name = "dtpValidityDate";
            this.dtpValidityDate.Size = new System.Drawing.Size(115, 23);
            this.dtpValidityDate.TabIndex = 2;
            // 
            // label18 - Authority
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label18.Location = new System.Drawing.Point(10, 55);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(95, 15);
            this.label18.Text = "Issuing Authority:";
            // 
            // txtIssuingAuthority
            // 
            this.txtIssuingAuthority.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtIssuingAuthority.Location = new System.Drawing.Point(110, 52);
            this.txtIssuingAuthority.Name = "txtIssuingAuthority";
            this.txtIssuingAuthority.Size = new System.Drawing.Size(180, 23);
            this.txtIssuingAuthority.TabIndex = 3;
            // 
            // label19 - Auth Desig
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label19.Location = new System.Drawing.Point(300, 55);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 15);
            this.label19.Text = "Designation:";
            // 
            // txtIssuingAuthorityDesig
            // 
            this.txtIssuingAuthorityDesig.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtIssuingAuthorityDesig.Location = new System.Drawing.Point(375, 52);
            this.txtIssuingAuthorityDesig.Name = "txtIssuingAuthorityDesig";
            this.txtIssuingAuthorityDesig.Size = new System.Drawing.Size(135, 23);
            this.txtIssuingAuthorityDesig.TabIndex = 4;
            // 
            // label20 - ID Number
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(10, 85);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(90, 15);
            this.label20.Text = "ID Card Number:";
            // 
            // txtIDCardNumber
            // 
            this.txtIDCardNumber.BackColor = System.Drawing.Color.LightYellow;
            this.txtIDCardNumber.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.txtIDCardNumber.Location = new System.Drawing.Point(110, 80);
            this.txtIDCardNumber.Name = "txtIDCardNumber";
            this.txtIDCardNumber.ReadOnly = true;
            this.txtIDCardNumber.Size = new System.Drawing.Size(180, 26);
            this.txtIDCardNumber.TabIndex = 5;
            // 
            // btnGenerateID
            // 
            this.btnGenerateID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(51)))));
            this.btnGenerateID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerateID.ForeColor = System.Drawing.Color.White;
            this.btnGenerateID.Location = new System.Drawing.Point(300, 78);
            this.btnGenerateID.Name = "btnGenerateID";
            this.btnGenerateID.Size = new System.Drawing.Size(100, 28);
            this.btnGenerateID.TabIndex = 6;
            this.btnGenerateID.Text = "Generate ID";
            this.btnGenerateID.UseVisualStyleBackColor = false;
            this.btnGenerateID.Click += new System.EventHandler(this.btnGenerateID_Click);
            // 
            // pnlPreview
            // 
            this.pnlPreview.AutoScroll = true;
            this.pnlPreview.Controls.Add(this.grpRemarks);
            this.pnlPreview.Controls.Add(this.pnlButtons);
            this.pnlPreview.Controls.Add(this.grpCardPreview);
            this.pnlPreview.Controls.Add(this.grpSignature);
            this.pnlPreview.Controls.Add(this.grpPhoto);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(0, 0);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Padding = new System.Windows.Forms.Padding(10);
            this.pnlPreview.Size = new System.Drawing.Size(546, 650);
            this.pnlPreview.TabIndex = 0;
            // 
            // grpPhoto
            // 
            this.grpPhoto.Controls.Add(this.btnCameraCapture);
            this.grpPhoto.Controls.Add(this.btnClearPhoto);
            this.grpPhoto.Controls.Add(this.btnBrowsePhoto);
            this.grpPhoto.Controls.Add(this.picPhoto);
            this.grpPhoto.Location = new System.Drawing.Point(13, 10);
            this.grpPhoto.Name = "grpPhoto";
            this.grpPhoto.Size = new System.Drawing.Size(180, 200);
            this.grpPhoto.TabIndex = 0;
            this.grpPhoto.TabStop = false;
            this.grpPhoto.Text = "Photo";
            // 
            // picPhoto
            // 
            this.picPhoto.BackColor = System.Drawing.Color.LightGray;
            this.picPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPhoto.Location = new System.Drawing.Point(15, 20);
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.Size = new System.Drawing.Size(150, 110);
            this.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPhoto.TabIndex = 0;
            this.picPhoto.TabStop = false;
            // 
            // btnBrowsePhoto
            // 
            this.btnBrowsePhoto.Location = new System.Drawing.Point(15, 135);
            this.btnBrowsePhoto.Name = "btnBrowsePhoto";
            this.btnBrowsePhoto.Size = new System.Drawing.Size(70, 25);
            this.btnBrowsePhoto.TabIndex = 1;
            this.btnBrowsePhoto.Text = "Browse...";
            this.btnBrowsePhoto.Click += new System.EventHandler(this.btnBrowsePhoto_Click);
            // 
            // btnCameraCapture
            // 
            this.btnCameraCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnCameraCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCameraCapture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCameraCapture.ForeColor = System.Drawing.Color.White;
            this.btnCameraCapture.Location = new System.Drawing.Point(15, 165);
            this.btnCameraCapture.Name = "btnCameraCapture";
            this.btnCameraCapture.Size = new System.Drawing.Size(150, 28);
            this.btnCameraCapture.TabIndex = 3;
            this.btnCameraCapture.Text = "📷 Camera Capture";
            this.btnCameraCapture.UseVisualStyleBackColor = false;
            this.btnCameraCapture.Click += new System.EventHandler(this.btnCameraCapture_Click);
            // 
            // btnClearPhoto
            // 
            this.btnClearPhoto.Location = new System.Drawing.Point(95, 135);
            this.btnClearPhoto.Name = "btnClearPhoto";
            this.btnClearPhoto.Size = new System.Drawing.Size(70, 25);
            this.btnClearPhoto.TabIndex = 2;
            this.btnClearPhoto.Text = "Clear";
            this.btnClearPhoto.Click += new System.EventHandler(this.btnClearPhoto_Click);
            // 
            // grpSignature
            // 
            this.grpSignature.Controls.Add(this.btnClearSignature);
            this.grpSignature.Controls.Add(this.btnBrowseSignature);
            this.grpSignature.Controls.Add(this.picSignature);
            this.grpSignature.Location = new System.Drawing.Point(200, 10);
            this.grpSignature.Name = "grpSignature";
            this.grpSignature.Size = new System.Drawing.Size(180, 100);
            this.grpSignature.TabIndex = 1;
            this.grpSignature.TabStop = false;
            this.grpSignature.Text = "Signature";
            // 
            // picSignature
            // 
            this.picSignature.BackColor = System.Drawing.Color.White;
            this.picSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSignature.Location = new System.Drawing.Point(15, 20);
            this.picSignature.Name = "picSignature";
            this.picSignature.Size = new System.Drawing.Size(150, 40);
            this.picSignature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSignature.TabIndex = 0;
            this.picSignature.TabStop = false;
            // 
            // btnBrowseSignature
            // 
            this.btnBrowseSignature.Location = new System.Drawing.Point(15, 65);
            this.btnBrowseSignature.Name = "btnBrowseSignature";
            this.btnBrowseSignature.Size = new System.Drawing.Size(70, 25);
            this.btnBrowseSignature.TabIndex = 1;
            this.btnBrowseSignature.Text = "Browse...";
            this.btnBrowseSignature.Click += new System.EventHandler(this.btnBrowseSignature_Click);
            // 
            // btnClearSignature
            // 
            this.btnClearSignature.Location = new System.Drawing.Point(95, 65);
            this.btnClearSignature.Name = "btnClearSignature";
            this.btnClearSignature.Size = new System.Drawing.Size(70, 25);
            this.btnClearSignature.TabIndex = 2;
            this.btnClearSignature.Text = "Clear";
            this.btnClearSignature.Click += new System.EventHandler(this.btnClearSignature_Click);
            // 
            // grpCardPreview
            // 
            this.grpCardPreview.Controls.Add(this.lblBack);
            this.grpCardPreview.Controls.Add(this.lblFront);
            this.grpCardPreview.Controls.Add(this.picCardBack);
            this.grpCardPreview.Controls.Add(this.picCardFront);
            this.grpCardPreview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpCardPreview.Location = new System.Drawing.Point(13, 220);
            this.grpCardPreview.Name = "grpCardPreview";
            this.grpCardPreview.Size = new System.Drawing.Size(520, 280);
            this.grpCardPreview.TabIndex = 2;
            this.grpCardPreview.TabStop = false;
            this.grpCardPreview.Text = "Card Preview";
            // 
            // picCardFront
            // 
            this.picCardFront.BackColor = System.Drawing.Color.White;
            this.picCardFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCardFront.Location = new System.Drawing.Point(15, 40);
            this.picCardFront.Name = "picCardFront";
            this.picCardFront.Size = new System.Drawing.Size(216, 220);
            this.picCardFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCardFront.TabIndex = 0;
            this.picCardFront.TabStop = false;
            // 
            // picCardBack
            // 
            this.picCardBack.BackColor = System.Drawing.Color.White;
            this.picCardBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCardBack.Location = new System.Drawing.Point(250, 40);
            this.picCardBack.Name = "picCardBack";
            this.picCardBack.Size = new System.Drawing.Size(216, 220);
            this.picCardBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCardBack.TabIndex = 1;
            this.picCardBack.TabStop = false;
            // 
            // lblFront
            // 
            this.lblFront.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFront.Location = new System.Drawing.Point(15, 20);
            this.lblFront.Name = "lblFront";
            this.lblFront.Size = new System.Drawing.Size(216, 15);
            this.lblFront.Text = "FRONT";
            this.lblFront.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBack
            // 
            this.lblBack.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBack.Location = new System.Drawing.Point(250, 20);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(216, 15);
            this.lblBack.Text = "BACK";
            this.lblBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnPrintPreview);
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Controls.Add(this.btnNew);
            this.pnlButtons.Controls.Add(this.btnSaveAndPrint);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Location = new System.Drawing.Point(13, 560);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(520, 45);
            this.pnlButtons.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(51)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(5, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAndPrint
            // 
            this.btnSaveAndPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnSaveAndPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAndPrint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveAndPrint.ForeColor = System.Drawing.Color.White;
            this.btnSaveAndPrint.Location = new System.Drawing.Point(105, 5);
            this.btnSaveAndPrint.Name = "btnSaveAndPrint";
            this.btnSaveAndPrint.Size = new System.Drawing.Size(110, 35);
            this.btnSaveAndPrint.TabIndex = 1;
            this.btnSaveAndPrint.Text = "Save && Print";
            this.btnSaveAndPrint.UseVisualStyleBackColor = false;
            this.btnSaveAndPrint.Click += new System.EventHandler(this.btnSaveAndPrint_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Gray;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(225, 5);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 35);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "➕ New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(76)))), ((int)(((byte)(0)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(325, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 35);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "🖨️ Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(425, 5);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(85, 35);
            this.btnPrintPreview.TabIndex = 4;
            this.btnPrintPreview.Text = "Preview";
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // grpRemarks
            // 
            this.grpRemarks.Controls.Add(this.txtRemarks);
            this.grpRemarks.Location = new System.Drawing.Point(13, 505);
            this.grpRemarks.Name = "grpRemarks";
            this.grpRemarks.Size = new System.Drawing.Size(520, 50);
            this.grpRemarks.TabIndex = 4;
            this.grpRemarks.TabStop = false;
            this.grpRemarks.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(10, 18);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(500, 23);
            this.txtRemarks.TabIndex = 0;
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EmployeeForm";
            this.Text = "Employee Data";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlFormContainer.ResumeLayout(false);
            this.grpCardInfo.ResumeLayout(false);
            this.grpCardInfo.PerformLayout();
            this.grpEmployment.ResumeLayout(false);
            this.grpEmployment.PerformLayout();
            this.grpPersonal.ResumeLayout(false);
            this.grpPersonal.PerformLayout();
            this.pnlPreview.ResumeLayout(false);
            this.grpRemarks.ResumeLayout(false);
            this.grpRemarks.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.grpCardPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCardBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCardFront)).EndInit();
            this.grpSignature.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).EndInit();
            this.grpPhoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlFormContainer;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.GroupBox grpPersonal;
        private System.Windows.Forms.GroupBox grpEmployment;
        private System.Windows.Forms.GroupBox grpCardInfo;
        private System.Windows.Forms.GroupBox grpPhoto;
        private System.Windows.Forms.GroupBox grpSignature;
        private System.Windows.Forms.GroupBox grpCardPreview;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.GroupBox grpRemarks;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFatherName;
        private System.Windows.Forms.Label lblFatherName;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBloodGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAadhaar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDesignation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPlaceOfPosting;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbZone;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPFNumber;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDateOfJoining;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpDateOfRetirement;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dtpDateOfIssue;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpValidityDate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtIssuingAuthority;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtIssuingAuthorityDesig;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtIDCardNumber;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnGenerateID;
        private System.Windows.Forms.PictureBox picPhoto;
        private System.Windows.Forms.Button btnBrowsePhoto;
        private System.Windows.Forms.Button btnClearPhoto;
        private System.Windows.Forms.Button btnCameraCapture;
        private System.Windows.Forms.PictureBox picSignature;
        private System.Windows.Forms.Button btnBrowseSignature;
        private System.Windows.Forms.Button btnClearSignature;
        private System.Windows.Forms.PictureBox picCardFront;
        private System.Windows.Forms.PictureBox picCardBack;
        private System.Windows.Forms.Label lblFront;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAndPrint;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.TextBox txtRemarks;
    }
}
