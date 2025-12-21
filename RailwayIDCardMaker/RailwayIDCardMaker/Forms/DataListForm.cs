using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Services;

namespace RailwayIDCardMaker.Forms
{
    public partial class DataListForm : Form
    {
        private List<Employee> _employees;
        public event EventHandler<Employee> EmployeeSelected;

        public DataListForm()
        {
            InitializeComponent();
        }

        private void DataListForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            _employees = DatabaseService.GetAllEmployees();
            PopulateGrid();
            UpdateStatus();
        }

        private void PopulateGrid()
        {
            dgvEmployees.Rows.Clear();

            foreach (var emp in _employees)
            {
                dgvEmployees.Rows.Add(
                    emp.Id,
                    emp.IDCardNumber,
                    emp.Name,
                    emp.Designation,
                    emp.Department,
                    emp.ZoneName,
                    emp.MobileNumber,
                    emp.IsCardPrinted ? "Yes" : "No",
                    emp.DateOfIssue?.ToString("dd-MM-yyyy")
                );
            }
        }

        private void UpdateStatus()
        {
            lblCount.Text = $"Total Records: {_employees.Count}";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchEmployees();
        }

        private void SearchEmployees()
        {
            string search = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                _employees = DatabaseService.GetAllEmployees();
            }
            else
            {
                _employees = DatabaseService.SearchEmployees(search);
            }

            PopulateGrid();
            UpdateStatus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            RefreshData();
        }

        private void dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedEmployee();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedEmployee();
        }

        private void EditSelectedEmployee()
        {
            if (dgvEmployees.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value);
            var employee = DatabaseService.GetEmployee(id);

            if (employee != null)
            {
                EmployeeSelected?.Invoke(this, employee);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to delete.", "Select Employee",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value);
                DatabaseService.DeleteEmployee(id);
                RefreshData();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to print card.", "Select Employee",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value);
            var employee = DatabaseService.GetEmployee(id);

            if (employee != null)
            {
                var printService = new PrintService();
                printService.PrintCard(employee, null);
                RefreshData();
            }
        }
    }
}
