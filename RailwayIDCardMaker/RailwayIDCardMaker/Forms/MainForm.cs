using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Services;
using RailwayIDCardMaker.Utils;

namespace RailwayIDCardMaker.Forms
{
    public partial class MainForm : Form
    {
        private EmployeeForm _employeeForm;
        private DataListForm _dataListForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (LoginForm.CurrentUser != null)
            {
                this.Text = $"{Constants.APP_NAME} - {LoginForm.CurrentUser.FullName} ({LoginForm.CurrentUser.Role})";
                lblUser.Text = $"User: {LoginForm.CurrentUser.Username}";
            }

            UpdateStatus("Ready");
            lblDate.Text = DateTime.Now.ToString("dd-MMM-yyyy | hh:mm tt");

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, ev) => { lblDate.Text = DateTime.Now.ToString("dd-MMM-yyyy | hh:mm tt"); };
            timer.Start();

            ShowWelcome();
        }

        private void ShowWelcome()
        {
            Panel welcomePanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 245, 245) };

            Label lblWelcome = new Label
            {
                Text = "Welcome to Railway Employee ID Card Maker",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };

            Label lblInstructions = new Label
            {
                Text = "Select an option from the menu or toolbar to begin:\n\n" +
                       "• New Card - Add a new employee and create their ID card\n" +
                       "• Data List - View and manage existing employee records\n" +
                       "• Import - Import employees from Excel\n" +
                       "• Settings - Configure application settings\n\n" +
                       "ID Card Format: YY-ZZ-UU-SSSSSS\n" +
                       "(Year-Zone-Unit-Serial Number)",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.DimGray,
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill,
                Padding = new Padding(50)
            };

            welcomePanel.Controls.Add(lblInstructions);
            welcomePanel.Controls.Add(lblWelcome);
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(welcomePanel);
        }

        private void UpdateStatus(string message) { lblStatus.Text = message; }

        #region Menu Events

        private void newCardToolStripMenuItem_Click(object sender, EventArgs e) { OpenEmployeeForm(null); }
        private void dataListToolStripMenuItem_Click(object sender, EventArgs e) { OpenDataList(); }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) { OpenSettings(); }
        private void importDataToolStripMenuItem_Click(object sender, EventArgs e) { ImportData(); }
        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e) { ExportData(); }
        private void backupToolStripMenuItem_Click(object sender, EventArgs e) { BackupDatabase(); }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e) { ChangePassword(); }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm about = new AboutForm()) { about.ShowDialog(); }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        #endregion

        #region Toolbar Events

        private void tsbNewCard_Click(object sender, EventArgs e) { OpenEmployeeForm(null); }
        private void tsbDataList_Click(object sender, EventArgs e) { OpenDataList(); }
        private void tsbSettings_Click(object sender, EventArgs e) { OpenSettings(); }
        private void tsbExit_Click(object sender, EventArgs e) { exitToolStripMenuItem_Click(sender, e); }

        #endregion

        #region Form Operations

        private void OpenEmployeeForm(Employee employee)
        {
            // Always create a fresh form when editing to ensure proper data loading
            if (employee != null)
            {
                // Dispose existing form if any
                try
                {
                    if (_employeeForm != null && !_employeeForm.IsDisposed)
                    {
                        _employeeForm.Close();
                    }
                }
                catch { }
                _employeeForm = null;
            }

            if (_employeeForm == null || _employeeForm.IsDisposed)
            {
                _employeeForm = new EmployeeForm();
                _employeeForm.FormClosed += (s, e) => { _employeeForm = null; };
                _employeeForm.EmployeeSaved += EmployeeForm_EmployeeSaved;
            }

            // IMPORTANT: Load employee data BEFORE showing the form
            // This sets _pendingEmployee which will be used in EmployeeForm_Load
            if (employee != null)
            {
                _employeeForm.LoadEmployee(employee);
            }

            // Now show the form in the panel
            ShowFormInPanel(_employeeForm);

            // Update status
            if (employee != null)
            {
                UpdateStatus($"Editing: {employee.Name}");
            }
            else
            {
                // Only call NewEmployee if no employee was passed
                _employeeForm.NewEmployee();
                UpdateStatus("Creating new ID Card...");
            }
        }

        private void EmployeeForm_EmployeeSaved(object sender, Employee employee)
        {
            UpdateStatus($"Employee '{employee.Name}' saved successfully");
            if (_dataListForm != null && !_dataListForm.IsDisposed)
                _dataListForm.RefreshData();
        }

        private void OpenDataList()
        {
            if (_dataListForm == null || _dataListForm.IsDisposed)
            {
                _dataListForm = new DataListForm();
                _dataListForm.FormClosed += (s, e) => { _dataListForm = null; };
                _dataListForm.EmployeeSelected += DataListForm_EmployeeSelected;
            }
            ShowFormInPanel(_dataListForm);
            UpdateStatus("Viewing employee data list");
        }

        private void DataListForm_EmployeeSelected(object sender, Employee employee) { OpenEmployeeForm(employee); }

        private void OpenSettings()
        {
            using (SettingsForm settings = new SettingsForm())
            {
                if (settings.ShowDialog() == DialogResult.OK)
                    UpdateStatus("Settings saved successfully");
            }
        }

        private void ShowFormInPanel(Form form)
        {
            pnlMain.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(form);
            form.Show();
        }

        #endregion

        #region Data Operations

        private void ImportData()
        {
            using (ExcelImportForm importForm = new ExcelImportForm())
            {
                if (importForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateStatus("Data imported successfully");
                    if (_dataListForm != null && !_dataListForm.IsDisposed)
                        _dataListForm.RefreshData();
                }
            }
        }

        private void ExportData()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Employee Data";
                dialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                dialog.FileName = $"EmployeeData_{DateTime.Now:yyyyMMdd}.csv";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var employees = DatabaseService.GetAllEmployees();
                        ExportToCsv(employees, dialog.FileName);
                        MessageBox.Show($"Data exported successfully!\n\nFile: {dialog.FileName}",
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateStatus("Data exported successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Export failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportToCsv(List<Employee> employees, string filePath)
        {
            using (var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("ID Card Number,Name,Designation,Department,Zone,Unit,Mobile,Aadhaar,Blood Group,Date of Issue,Validity");
                foreach (var emp in employees)
                {
                    writer.WriteLine($"\"{emp.IDCardNumber}\",\"{emp.Name}\",\"{emp.Designation}\",\"{emp.Department}\",\"{emp.ZoneName}\",\"{emp.UnitName}\",\"{emp.MobileNumber}\",\"{emp.GetMaskedAadhaar()}\",\"{emp.BloodGroup}\",\"{emp.DateOfIssue:dd-MM-yyyy}\",\"{emp.ValidityDate:dd-MM-yyyy}\"");
                }
            }
        }

        private void BackupDatabase()
        {
            using (BackupRestoreForm backupForm = new BackupRestoreForm())
            {
                backupForm.ShowDialog();
            }
        }

        private void ChangePassword()
        {
            using (ChangePasswordForm passwordForm = new ChangePasswordForm())
            {
                if (passwordForm.ShowDialog() == DialogResult.OK)
                    UpdateStatus("Password changed successfully");
            }
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
