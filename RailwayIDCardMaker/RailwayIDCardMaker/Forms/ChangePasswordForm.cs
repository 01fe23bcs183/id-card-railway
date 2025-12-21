using System;
using System.Windows.Forms;
using RailwayIDCardMaker.Services;

namespace RailwayIDCardMaker.Forms
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                MessageBox.Show("Please enter your current password", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Please enter a new password", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (txtNewPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("New password and confirm password do not match", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                var currentUser = LoginForm.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("No user is currently logged in", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verify current password
                string currentPasswordHash = Utils.Helpers.HashPassword(txtCurrentPassword.Text);
                var user = DatabaseService.AuthenticateUser(currentUser.Username, txtCurrentPassword.Text);

                if (user == null)
                {
                    MessageBox.Show("Current password is incorrect", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCurrentPassword.Clear();
                    txtCurrentPassword.Focus();
                    return;
                }

                // Update password
                DatabaseService.UpdateUserPassword(currentUser.Id, txtNewPassword.Text);

                MessageBox.Show("Password changed successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}", "Error",
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
