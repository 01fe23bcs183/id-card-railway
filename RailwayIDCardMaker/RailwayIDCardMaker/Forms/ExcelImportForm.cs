using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Services;

namespace RailwayIDCardMaker.Forms
{
    public partial class ExcelImportForm : Form
    {
        private List<Employee> importedEmployees;

        public ExcelImportForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Excel File";
                dialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = dialog.FileName;
                    btnPreview.Enabled = true;
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Loading data from Excel...";

                importedEmployees = ExcelImportService.ImportFromExcel(txtFilePath.Text);

                dgvPreview.DataSource = null;
                dgvPreview.DataSource = importedEmployees;

                // Configure grid columns
                if (dgvPreview.Columns.Count > 0)
                {
                    dgvPreview.Columns["Id"].Visible = false;
                    dgvPreview.Columns["Photo"].Visible = false;
                    dgvPreview.Columns["Signature"].Visible = false;
                    dgvPreview.Columns["IsPrinted"].Visible = false;
                }

                lblStatus.Text = $"Loaded {importedEmployees.Count} records from Excel";
                btnImport.Enabled = importedEmployees.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Excel file: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error loading file";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (importedEmployees == null || importedEmployees.Count == 0)
            {
                MessageBox.Show("No data to import", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to import {importedEmployees.Count} employee records?\n\n" +
                "This will add all records to the database.",
                "Confirm Import",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                int successCount = 0;
                int errorCount = 0;

                foreach (var employee in importedEmployees)
                {
                    try
                    {
                        // Generate ID if not present
                        if (string.IsNullOrEmpty(employee.IDCardNumber))
                        {
                            employee.IDCardNumber = IDNumberGenerator.GenerateIDNumber(
                                employee.ZoneName, employee.UnitCode);
                        }

                        DatabaseService.SaveEmployee(employee);
                        successCount++;
                        lblStatus.Text = $"Importing... {successCount}/{importedEmployees.Count}";
                        Application.DoEvents();
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        System.Diagnostics.Debug.WriteLine($"Error importing employee: {ex.Message}");
                    }
                }

                MessageBox.Show(
                    $"Import completed!\n\n" +
                    $"Successfully imported: {successCount}\n" +
                    $"Errors: {errorCount}",
                    "Import Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during import: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Save Template";
                    dialog.Filter = "CSV Files (*.csv)|*.csv";
                    dialog.FileName = "EmployeeImportTemplate.csv";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ExcelImportService.ExportTemplate(dialog.FileName);

                        MessageBox.Show("Template saved successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ask if they want to open the file
                        if (MessageBox.Show("Do you want to open the template file?", "Open Template",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(dialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving template: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
