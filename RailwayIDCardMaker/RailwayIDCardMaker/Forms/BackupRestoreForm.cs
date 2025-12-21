using System;
using System.IO;
using System.Windows.Forms;

namespace RailwayIDCardMaker.Forms
{
    public partial class BackupRestoreForm : Form
    {
        public BackupRestoreForm()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Save Database Backup";
                    dialog.Filter = "Database Backup (*.db)|*.db|All Files (*.*)|*.*";
                    dialog.FileName = $"RailwayIDCard_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        lblStatus.Text = "Creating backup...";
                        Application.DoEvents();

                        // Get the current database path
                        string sourcePath = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "RailwayIDCardMaker",
                            "railway_idcard.db");

                        if (!File.Exists(sourcePath))
                        {
                            MessageBox.Show("Database file not found!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Copy the database file
                        File.Copy(sourcePath, dialog.FileName, true);

                        lblStatus.Text = "Backup completed successfully!";
                        MessageBox.Show($"Database backed up successfully to:\n{dialog.FileName}",
                            "Backup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating backup: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Backup failed";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirmResult = MessageBox.Show(
                    "WARNING: Restoring from backup will replace all current data!\n\n" +
                    "Are you sure you want to continue?",
                    "Confirm Restore",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes)
                    return;

                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Select Backup File";
                    dialog.Filter = "Database Backup (*.db)|*.db|All Files (*.*)|*.*";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        lblStatus.Text = "Restoring from backup...";
                        Application.DoEvents();

                        // Get the current database path
                        string targetPath = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "RailwayIDCardMaker",
                            "railway_idcard.db");

                        // Create a backup of current database before restoring
                        string autoBackupPath = Path.Combine(
                            Path.GetDirectoryName(targetPath),
                            $"auto_backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");

                        if (File.Exists(targetPath))
                        {
                            File.Copy(targetPath, autoBackupPath, true);
                        }

                        // Copy the backup file to replace current database
                        File.Copy(dialog.FileName, targetPath, true);

                        lblStatus.Text = "Restore completed successfully!";
                        MessageBox.Show(
                            "Database restored successfully!\n\n" +
                            "The application will now restart to load the restored data.",
                            "Restore Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Restart application
                        Application.Restart();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error restoring backup: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Restore failed";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
