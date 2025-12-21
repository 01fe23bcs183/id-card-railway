using System;
using System.Windows.Forms;

namespace RailwayIDCardMaker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize database
            Services.DatabaseService.InitializeDatabase();

            // Show login form first
            using (var loginForm = new Forms.LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // If login successful, show main form
                    Application.Run(new Forms.MainForm());
                }
            }
        }
    }
}
