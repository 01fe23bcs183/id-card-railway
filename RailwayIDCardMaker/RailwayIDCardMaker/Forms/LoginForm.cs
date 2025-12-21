using System;
using System.Drawing;
using System.Windows.Forms;
using RailwayIDCardMaker.Services;
using RailwayIDCardMaker.Utils;

namespace RailwayIDCardMaker.Forms
{
    /// <summary>
    /// Login form for user authentication
    /// </summary>
    public partial class LoginForm : Form
    {
        public static Models.User CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Set focus to username
            txtUsername.Focus();

            // Set default credentials hint (remove in production)
            lblHint.Text = $"Default: {Constants.DEFAULT_USERNAME} / {Constants.DEFAULT_PASSWORD}";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogin();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void PerformLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate input
            if (string.IsNullOrEmpty(username))
            {
                ShowError("Please enter username");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowError("Please enter password");
                txtPassword.Focus();
                return;
            }

            // Disable controls during login
            btnLogin.Enabled = false;
            btnLogin.Text = "Logging in...";
            Application.DoEvents();

            try
            {
                // Attempt login
                var user = DatabaseService.ValidateUser(username, password);

                if (user != null)
                {
                    CurrentUser = user;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowError("Invalid username or password");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Login error: {ex.Message}");
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "&Login";
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
    }
}
