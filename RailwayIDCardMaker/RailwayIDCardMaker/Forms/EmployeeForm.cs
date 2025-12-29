using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Services;
using RailwayIDCardMaker.Utils;

namespace RailwayIDCardMaker.Forms
{
    /// <summary>
    /// Employee data entry form with card preview
    /// </summary>
    public partial class EmployeeForm : Form
    {
        private Employee _currentEmployee;
        private Image _logoImage = null;
        private bool _isNewEmployee = true;
        private PrintService _printService;
        private Employee _pendingEmployee = null; // Employee to load after form is ready
        private bool _isFormLoaded = false;
        private bool _isPopulatingForm = false;

        public event EventHandler<Employee> EmployeeSaved;

        public EmployeeForm()
        {
            InitializeComponent();
            _printService = new PrintService();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            _isFormLoaded = true;

            // If there's a pending employee to load, load it now
            if (_pendingEmployee != null)
            {
                _currentEmployee = _pendingEmployee;
                _pendingEmployee = null;
                _isNewEmployee = false;
                PopulateForm();
                lblFormTitle.Text = $"Edit: {_currentEmployee.Name}";
                UpdateCardPreview();
            }
            else
            {
                NewEmployee();
            }
        }

        #region Data Loading

        private void LoadComboBoxes()
        {
            // Blood Groups
            cmbBloodGroup.Items.Clear();
            cmbBloodGroup.Items.AddRange(Constants.BLOOD_GROUPS);

            // Gender
            cmbGender.Items.Clear();
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });

            // Departments
            cmbDepartment.Items.Clear();
            cmbDepartment.Items.AddRange(Constants.DEPARTMENTS);

            // Zones
            cmbZone.Items.Clear();
            var zones = DatabaseService.GetAllZones();
            foreach (var zone in zones)
            {
                cmbZone.Items.Add(zone);
            }
            cmbZone.DisplayMember = "ToString";
        }

        public void NewEmployee()
        {
            _currentEmployee = new Employee();
            _isNewEmployee = true;

            // Set defaults from settings
            var settings = DatabaseService.GetSettings();
            _currentEmployee.ZoneCode = settings.DefaultZoneCode;
            _currentEmployee.ZoneName = settings.DefaultZoneName;
            _currentEmployee.UnitCode = settings.DefaultUnitCode;
            _currentEmployee.UnitName = settings.DefaultUnitName;
            _currentEmployee.IssuingAuthority = settings.DefaultIssuingAuthority;
            _currentEmployee.IssuingAuthorityDesignation = settings.DefaultIssuingAuthorityDesignation;
            _currentEmployee.DateOfIssue = DateTime.Now;
            _currentEmployee.ValidityDate = DateTime.Now.AddYears(settings.DefaultValidityYears);

            ClearForm();
            PopulateForm();
            UpdateCardPreview();

            lblFormTitle.Text = "New Employee ID Card";
        }

        public void LoadEmployee(Employee employee)
        {
            if (employee == null) return;

            if (!_isFormLoaded)
            {
                // Form not yet loaded, store the employee to load after form is ready
                _pendingEmployee = employee;
                return;
            }

            // Form is loaded, directly populate
            _currentEmployee = employee;
            _isNewEmployee = false;
            PopulateForm();
            lblFormTitle.Text = $"Edit: {_currentEmployee.Name}";
            UpdateCardPreview();
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtFatherName.Text = "";
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-25);
            cmbBloodGroup.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
            txtAddress.Text = "";
            txtMobile.Text = "";
            txtAadhaar.Text = "";
            txtDesignation.Text = "";
            cmbDepartment.SelectedIndex = -1;
            txtPlaceOfPosting.Text = "";
            cmbZone.SelectedIndex = -1;
            txtUnit.Text = "";
            txtPFNumber.Text = "";
            dtpDateOfJoining.Value = DateTime.Now;
            dtpDateOfRetirement.Value = DateTime.Now.AddYears(35);
            dtpDateOfIssue.Value = DateTime.Now;
            dtpValidityDate.Value = DateTime.Now.AddYears(5);
            txtIssuingAuthority.Text = "";
            txtIssuingAuthorityDesig.Text = "";
            txtRemarks.Text = "";
            txtIDCardNumber.Text = "";
            txtQRCodeUrl.Text = "";
            picPhoto.Image = null;
            picSignature.Image = null;
            picAuthoritySignature.Image = null;
        }

        /// <summary>
        /// Populates all form fields from _currentEmployee
        /// </summary>
        private void PopulateForm()
        {
            if (_currentEmployee == null) return;

            _isPopulatingForm = true;
            try
            {
                // Personal Information
                txtName.Text = _currentEmployee.Name ?? "";
                txtFatherName.Text = _currentEmployee.FatherName ?? "";

                if (_currentEmployee.DateOfBirth.HasValue && _currentEmployee.DateOfBirth.Value > DateTime.MinValue)
                    dtpDateOfBirth.Value = _currentEmployee.DateOfBirth.Value;

                // ComboBoxes - find and select by value
                SelectComboItem(cmbBloodGroup, _currentEmployee.BloodGroup);
                SelectComboItem(cmbGender, _currentEmployee.Gender);
                SelectComboItem(cmbDepartment, _currentEmployee.Department);

                // Contact Information
                txtAddress.Text = _currentEmployee.Address ?? "";
                txtMobile.Text = _currentEmployee.MobileNumber ?? "";
                txtAadhaar.Text = _currentEmployee.AadhaarNumber ?? "";

                // Employment Information
                txtDesignation.Text = _currentEmployee.Designation ?? "";
                txtPlaceOfPosting.Text = _currentEmployee.PlaceOfPosting ?? "";

                // Select zone by code
                SelectZone(_currentEmployee.ZoneCode);

                txtUnit.Text = _currentEmployee.UnitCode ?? "";
                txtPFNumber.Text = _currentEmployee.PFNumber ?? "";

                // Dates
                if (_currentEmployee.DateOfJoining.HasValue && _currentEmployee.DateOfJoining.Value > DateTime.MinValue)
                    dtpDateOfJoining.Value = _currentEmployee.DateOfJoining.Value;
                if (_currentEmployee.DateOfRetirement.HasValue && _currentEmployee.DateOfRetirement.Value > DateTime.MinValue)
                    dtpDateOfRetirement.Value = _currentEmployee.DateOfRetirement.Value;
                if (_currentEmployee.DateOfIssue.HasValue && _currentEmployee.DateOfIssue.Value > DateTime.MinValue)
                    dtpDateOfIssue.Value = _currentEmployee.DateOfIssue.Value;
                if (_currentEmployee.ValidityDate.HasValue && _currentEmployee.ValidityDate.Value > DateTime.MinValue)
                    dtpValidityDate.Value = _currentEmployee.ValidityDate.Value;

                // ID Card Information
                txtIssuingAuthority.Text = _currentEmployee.IssuingAuthority ?? "";
                txtIssuingAuthorityDesig.Text = _currentEmployee.IssuingAuthorityDesignation ?? "";
                txtRemarks.Text = _currentEmployee.Remarks ?? "";
                txtIDCardNumber.Text = _currentEmployee.IDCardNumber ?? "";
                txtQRCodeUrl.Text = _currentEmployee.QRCodeUrl ?? "";

                // Load images
                if (!string.IsNullOrEmpty(_currentEmployee.PhotoPath) && File.Exists(_currentEmployee.PhotoPath))
                    picPhoto.Image = ImageService.LoadImage(_currentEmployee.PhotoPath);
                if (!string.IsNullOrEmpty(_currentEmployee.SignaturePath) && File.Exists(_currentEmployee.SignaturePath))
                    picSignature.Image = ImageService.LoadImage(_currentEmployee.SignaturePath);
                if (!string.IsNullOrEmpty(_currentEmployee.AuthoritySignaturePath) && File.Exists(_currentEmployee.AuthoritySignaturePath))
                    picAuthoritySignature.Image = ImageService.LoadImage(_currentEmployee.AuthoritySignaturePath);
            }
            finally
            {
                _isPopulatingForm = false;
            }
        }

        private void SelectComboItem(ComboBox combo, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i].ToString() == value)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }

        private void SelectZone(string zoneCode)
        {
            if (string.IsNullOrEmpty(zoneCode)) return;

            for (int i = 0; i < cmbZone.Items.Count; i++)
            {
                var zone = cmbZone.Items[i] as Zone;
                if (zone != null && zone.Code == zoneCode)
                {
                    cmbZone.SelectedIndex = i;
                    return;
                }
            }
        }

        private void CollectFormData()
        {
            _currentEmployee.Name = txtName.Text.Trim();
            _currentEmployee.FatherName = txtFatherName.Text.Trim();
            _currentEmployee.DateOfBirth = dtpDateOfBirth.Value;
            _currentEmployee.BloodGroup = cmbBloodGroup.SelectedIndex >= 0 ? cmbBloodGroup.SelectedItem.ToString() : "";
            _currentEmployee.Gender = cmbGender.SelectedIndex >= 0 ? cmbGender.SelectedItem.ToString() : "";
            _currentEmployee.Address = txtAddress.Text.Trim();
            _currentEmployee.MobileNumber = txtMobile.Text.Trim();
            _currentEmployee.AadhaarNumber = txtAadhaar.Text.Trim().Replace("-", "").Replace(" ", "");
            _currentEmployee.Designation = txtDesignation.Text.Trim();
            _currentEmployee.Department = cmbDepartment.SelectedIndex >= 0 ? cmbDepartment.SelectedItem.ToString() : "";
            _currentEmployee.PlaceOfPosting = txtPlaceOfPosting.Text.Trim();

            var selectedZone = cmbZone.SelectedItem as Zone;
            if (selectedZone != null)
            {
                _currentEmployee.ZoneCode = selectedZone.Code;
                _currentEmployee.ZoneName = selectedZone.Abbreviation;
            }

            _currentEmployee.UnitCode = txtUnit.Text.Trim();
            _currentEmployee.UnitName = txtUnit.Text.Trim();
            _currentEmployee.PFNumber = txtPFNumber.Text.Trim();
            _currentEmployee.DateOfJoining = dtpDateOfJoining.Value;
            _currentEmployee.DateOfRetirement = dtpDateOfRetirement.Value;
            _currentEmployee.DateOfIssue = dtpDateOfIssue.Value;
            _currentEmployee.ValidityDate = dtpValidityDate.Value;
            _currentEmployee.IssuingAuthority = txtIssuingAuthority.Text.Trim();
            _currentEmployee.IssuingAuthorityDesignation = txtIssuingAuthorityDesig.Text.Trim();
            _currentEmployee.Remarks = txtRemarks.Text.Trim();
            _currentEmployee.QRCodeUrl = txtQRCodeUrl.Text.Trim();

            // Generate ID if new employee
            if (_isNewEmployee && string.IsNullOrEmpty(_currentEmployee.IDCardNumber))
            {
                _currentEmployee.SerialNumber = DatabaseService.GetNextSerialNumber();
                _currentEmployee.GenerateIDCardNumber();
            }

            txtIDCardNumber.Text = _currentEmployee.IDCardNumber;
        }

        #endregion

        #region Validation

        private bool ValidateForm()
        {
            // Name is required
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Employee name is required");
                txtName.Focus();
                return false;
            }

            // Designation is required
            if (string.IsNullOrWhiteSpace(txtDesignation.Text))
            {
                ShowError("Designation is required");
                txtDesignation.Focus();
                return false;
            }

            // Department is required
            if (string.IsNullOrWhiteSpace(cmbDepartment.Text))
            {
                ShowError("Department is required");
                cmbDepartment.Focus();
                return false;
            }

            // Zone is required
            if (cmbZone.SelectedIndex < 0)
            {
                ShowError("Please select a Zone");
                cmbZone.Focus();
                return false;
            }

            // Validate Aadhaar if provided
            if (!string.IsNullOrWhiteSpace(txtAadhaar.Text))
            {
                if (!Helpers.IsValidAadhaar(txtAadhaar.Text))
                {
                    ShowError("Invalid Aadhaar number. Must be 12 digits.");
                    txtAadhaar.Focus();
                    return false;
                }
            }

            // Validate mobile if provided
            if (!string.IsNullOrWhiteSpace(txtMobile.Text))
            {
                if (!Helpers.IsValidMobile(txtMobile.Text))
                {
                    ShowError("Invalid mobile number. Must be 10 digits starting with 6-9.");
                    txtMobile.Focus();
                    return false;
                }
            }

            return true;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion

        #region Photo & Signature

        private void btnBrowsePhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Employee Photo";
                dialog.Filter = ImageService.GetImageFileFilter();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Validate photo first (70% face coverage check)
                        var validation = PhotoValidator.ValidatePhoto(dialog.FileName);

                        if (!validation.IsValid && validation.FaceCoveragePercent < 20)
                        {
                            // Photo is completely invalid
                            MessageBox.Show($"Photo validation failed:\n{validation.Message}",
                                "Invalid Photo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (validation.FaceCoveragePercent < 50)
                        {
                            // Photo is acceptable but face coverage is low
                            var result = MessageBox.Show(
                                $"Photo validation warning:\n{validation.Message}\n\nFace coverage: {validation.FaceCoveragePercent:F0}%\nRecommended: 70% or more\n\nDo you want to use this photo anyway?",
                                "Low Face Coverage", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result != DialogResult.Yes)
                                return;
                        }

                        Image photo = Image.FromFile(dialog.FileName);

                        // Resize and display
                        picPhoto.Image = ImageService.ResizeAndCropForCard(photo,
                            picPhoto.Width, picPhoto.Height);

                        // Save to photos folder
                        string empId = _isNewEmployee ? Guid.NewGuid().ToString().Substring(0, 8) :
                            _currentEmployee.Id.ToString();
                        _currentEmployee.PhotoPath = ImageService.SaveEmployeePhoto(photo, empId);

                        photo.Dispose();

                        UpdateCardPreview();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading photo: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClearPhoto_Click(object sender, EventArgs e)
        {
            picPhoto.Image = null;
            _currentEmployee.PhotoPath = null;
            UpdateCardPreview();
        }

        private void btnCameraCapture_Click(object sender, EventArgs e)
        {
            try
            {
                using (WebcamCaptureForm webcamForm = new WebcamCaptureForm())
                {
                    if (webcamForm.ShowDialog() == DialogResult.OK && webcamForm.CapturedPhoto != null)
                    {
                        // Display captured image
                        picPhoto.Image = ImageService.ResizeAndCropForCard(webcamForm.CapturedPhoto,
                            picPhoto.Width, picPhoto.Height);

                        // Save to photos folder
                        string empId = _isNewEmployee ? Guid.NewGuid().ToString().Substring(0, 8) :
                            _currentEmployee.Id.ToString();
                        _currentEmployee.PhotoPath = ImageService.SaveEmployeePhoto(webcamForm.CapturedPhoto, empId);

                        UpdateCardPreview();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error with camera capture: {ex.Message}\n\nMake sure a webcam is connected.",
                    "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseSignature_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Signature Image";
                dialog.Filter = ImageService.GetImageFileFilter();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image signature = Image.FromFile(dialog.FileName);
                        picSignature.Image = ImageService.ResizeImage(signature,
                            picSignature.Width, picSignature.Height);

                        string empId = _isNewEmployee ? Guid.NewGuid().ToString().Substring(0, 8) :
                            _currentEmployee.Id.ToString();
                        _currentEmployee.SignaturePath = ImageService.SaveEmployeeSignature(signature, empId);

                        signature.Dispose();

                        UpdateCardPreview();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading signature: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClearSignature_Click(object sender, EventArgs e)
        {
            picSignature.Image = null;
            _currentEmployee.SignaturePath = null;
            UpdateCardPreview();
        }

        private void btnCameraSignature_Click(object sender, EventArgs e)
        {
            try
            {
                using (WebcamCaptureForm webcamForm = new WebcamCaptureForm())
                {
                    if (webcamForm.ShowDialog() == DialogResult.OK && webcamForm.CapturedPhoto != null)
                    {
                        Image captured = webcamForm.CapturedPhoto;
                        picSignature.Image = ImageService.ResizeImage(captured,
                            picSignature.Width, picSignature.Height);

                        string empId = _isNewEmployee ? Guid.NewGuid().ToString().Substring(0, 8) :
                            _currentEmployee.Id.ToString();
                        _currentEmployee.SignaturePath = ImageService.SaveEmployeeSignature(captured, empId);

                        captured.Dispose();

                        UpdateCardPreview();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error with camera capture: {ex.Message}\n\nMake sure a webcam is connected.",
                    "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseAuthoritySignature_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Authority Signature Image";
                dialog.Filter = ImageService.GetImageFileFilter();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image signature = Image.FromFile(dialog.FileName);
                        picAuthoritySignature.Image = ImageService.ResizeImage(signature,
                            picAuthoritySignature.Width, picAuthoritySignature.Height);

                        string empId = _isNewEmployee ? Guid.NewGuid().ToString().Substring(0, 8) :
                            _currentEmployee.Id.ToString();
                        _currentEmployee.AuthoritySignaturePath = ImageService.SaveAuthoritySignature(signature, empId);

                        signature.Dispose();

                        UpdateCardPreview();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading signature: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClearAuthoritySignature_Click(object sender, EventArgs e)
        {
            picAuthoritySignature.Image = null;
            _currentEmployee.AuthoritySignaturePath = null;
            UpdateCardPreview();
        }

        #endregion

        #region Card Preview

        private void UpdateCardPreview()
        {
            if (_isPopulatingForm) return;
            CollectFormData();

            // Render front card
            try
            {
                using (Bitmap front = CardRenderer.RenderCardFront(_currentEmployee, _logoImage))
                {
                    if (picCardFront.Image != null)
                    {
                        picCardFront.Image.Dispose();
                    }
                    picCardFront.Image = CardRenderer.GetScaledPreview(front, 0.5f);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Front card error: {ex.Message}");
            }

            // Render back card separately
            try
            {
                using (Bitmap back = CardRenderer.RenderCardBack(_currentEmployee))
                {
                    if (picCardBack.Image != null)
                    {
                        picCardBack.Image.Dispose();
                    }
                    picCardBack.Image = CardRenderer.GetScaledPreview(back, 0.5f);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Back card error: {ex.Message}");
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // Update preview on key fields change
            if (_currentEmployee != null && !_isPopulatingForm)
            {
                _currentEmployee.Name = txtName.Text;
                UpdateCardPreview();
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentEmployee != null && !_isPopulatingForm)
            {
                _currentEmployee.Department = cmbDepartment.Text;
                UpdateCardPreview();
            }
        }

        private void cmbBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentEmployee != null && !_isPopulatingForm)
            {
                _currentEmployee.BloodGroup = cmbBloodGroup.Text;
                UpdateCardPreview();
            }
        }

        private void txtQRCodeUrl_TextChanged(object sender, EventArgs e)
        {
            if (_currentEmployee != null && !_isPopulatingForm)
            {
                UpdateCardPreview();
            }
        }

        #endregion

        #region Save & Print

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            CollectFormData();

            // DEBUG: Show what data is being saved
            System.Diagnostics.Debug.WriteLine($"=== SAVING EMPLOYEE ===");
            System.Diagnostics.Debug.WriteLine($"Name: '{_currentEmployee.Name}'");
            System.Diagnostics.Debug.WriteLine($"FatherName: '{_currentEmployee.FatherName}'");
            System.Diagnostics.Debug.WriteLine($"BloodGroup: '{_currentEmployee.BloodGroup}'");
            System.Diagnostics.Debug.WriteLine($"Gender: '{_currentEmployee.Gender}'");
            System.Diagnostics.Debug.WriteLine($"Address: '{_currentEmployee.Address}'");
            System.Diagnostics.Debug.WriteLine($"Mobile: '{_currentEmployee.MobileNumber}'");
            System.Diagnostics.Debug.WriteLine($"Aadhaar: '{_currentEmployee.AadhaarNumber}'");
            System.Diagnostics.Debug.WriteLine($"Designation: '{_currentEmployee.Designation}'");
            System.Diagnostics.Debug.WriteLine($"Department: '{_currentEmployee.Department}'");
            System.Diagnostics.Debug.WriteLine($"PlaceOfPosting: '{_currentEmployee.PlaceOfPosting}'");
            System.Diagnostics.Debug.WriteLine($"========================");

            try
            {
                _currentEmployee.CreatedBy = LoginForm.CurrentUser?.Username;
                _currentEmployee.ModifiedBy = LoginForm.CurrentUser?.Username;

                int id = DatabaseService.SaveEmployee(_currentEmployee);
                _currentEmployee.Id = id;
                _isNewEmployee = false;

                MessageBox.Show("Employee saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Raise event
                EmployeeSaved?.Invoke(this, _currentEmployee);

                lblFormTitle.Text = $"Edit: {_currentEmployee.Name}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving employee: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            CollectFormData();

            try
            {
                _currentEmployee.CreatedBy = LoginForm.CurrentUser?.Username;
                _currentEmployee.ModifiedBy = LoginForm.CurrentUser?.Username;

                int id = DatabaseService.SaveEmployee(_currentEmployee);
                _currentEmployee.Id = id;
                _isNewEmployee = false;

                // Print
                _printService.PrintCard(_currentEmployee, _logoImage);

                // Raise event
                EmployeeSaved?.Invoke(this, _currentEmployee);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_isNewEmployee)
            {
                MessageBox.Show("Please save the employee first before printing.",
                    "Save Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _printService.PrintCard(_currentEmployee, _logoImage);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            CollectFormData();
            _printService.ShowPrintPreview(_currentEmployee, _logoImage);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Create a new employee? Unsaved changes will be lost.",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                NewEmployee();
            }
        }

        #endregion

        private void btnGenerateID_Click(object sender, EventArgs e)
        {
            CollectFormData();

            if (cmbZone.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Zone first", "Zone Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedZone = cmbZone.SelectedItem as Zone;
            string zoneCode = selectedZone?.Code ?? "00";
            string unitCode = txtUnit.Text.Trim().PadLeft(2, '0');

            _currentEmployee.SerialNumber = DatabaseService.GetNextSerialNumber();
            _currentEmployee.IDCardNumber = IDNumberGenerator.GenerateIDNumber(zoneCode, unitCode,
                _currentEmployee.SerialNumber);

            txtIDCardNumber.Text = _currentEmployee.IDCardNumber;
            UpdateCardPreview();
        }
    }
}
