using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using RailwayIDCardMaker.Models;

namespace RailwayIDCardMaker.Services
{
    /// <summary>
    /// Print service for printing ID cards
    /// </summary>
    public class PrintService
    {
        private Employee _currentEmployee;
        private Image _logoImage;
        private bool _printingBack = false;
        private PrintDocument _printDocument;

        public PrintService()
        {
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        /// <summary>
        /// Print ID card with print dialog
        /// </summary>
        public DialogResult PrintCard(Employee employee, Image logoImage = null)
        {
            _currentEmployee = employee;
            _logoImage = logoImage;
            _printingBack = false;

            using (PrintDialog dialog = new PrintDialog())
            {
                dialog.Document = _printDocument;
                dialog.AllowSomePages = false;
                dialog.ShowHelp = false;
                dialog.ShowNetwork = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Print front side
                        _printDocument.Print();

                        // Ask to print back side
                        if (MessageBox.Show("Print the back side of the card?",
                            "Print Back", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _printingBack = true;
                            _printDocument.Print();
                        }

                        // Update print status
                        UpdatePrintStatus(employee);

                        return DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Print error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return DialogResult.Cancel;
                    }
                }
            }

            return DialogResult.Cancel;
        }

        /// <summary>
        /// Print preview dialog
        /// </summary>
        public DialogResult ShowPrintPreview(Employee employee, Image logoImage = null)
        {
            _currentEmployee = employee;
            _logoImage = logoImage;
            _printingBack = false;

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = _printDocument;
                preview.Width = 800;
                preview.Height = 600;
                preview.ShowIcon = false;

                return preview.ShowDialog();
            }
        }

        /// <summary>
        /// Direct print without dialog
        /// </summary>
        public bool PrintDirect(Employee employee, string printerName = null, Image logoImage = null)
        {
            _currentEmployee = employee;
            _logoImage = logoImage;
            _printingBack = false;

            try
            {
                if (!string.IsNullOrEmpty(printerName))
                {
                    _printDocument.PrinterSettings.PrinterName = printerName;
                }

                // Print front
                _printDocument.Print();

                // Print back
                _printingBack = true;
                _printDocument.Print();

                UpdatePrintStatus(employee);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Print error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_currentEmployee == null)
            {
                return;
            }

            // Generate card image
            Bitmap cardImage;
            if (_printingBack)
            {
                cardImage = CardRenderer.RenderCardBack(_currentEmployee);
            }
            else
            {
                cardImage = CardRenderer.RenderCardFront(_currentEmployee, _logoImage);
            }

            try
            {
                // Calculate print area
                // Card size: 54mm x 87mm
                // Convert mm to hundredths of an inch (1mm = 3.937)
                float cardWidthInch = 54f / 25.4f;  // ~2.126 inches
                float cardHeightInch = 87f / 25.4f; // ~3.425 inches

                // Get printable area
                float printableWidth = e.MarginBounds.Width;
                float printableHeight = e.MarginBounds.Height;

                // Calculate scale to fit printable area
                float scaleX = printableWidth / (cardWidthInch * 100);
                float scaleY = printableHeight / (cardHeightInch * 100);
                float scale = Math.Min(scaleX, scaleY);

                // Calculate final dimensions
                float printWidth = cardWidthInch * 100; // in hundredths of inch
                float printHeight = cardHeightInch * 100;

                // Center on page
                float printX = e.MarginBounds.Left + (e.MarginBounds.Width - printWidth) / 2;
                float printY = e.MarginBounds.Top + (e.MarginBounds.Height - printHeight) / 2;

                // Draw the card
                e.Graphics.DrawImage(cardImage, printX, printY, printWidth, printHeight);

                e.HasMorePages = false;
            }
            finally
            {
                cardImage.Dispose();
            }
        }

        private void UpdatePrintStatus(Employee employee)
        {
            employee.IsCardPrinted = true;
            employee.PrintCount++;
            employee.LastPrintedDate = DateTime.Now;

            DatabaseService.SaveEmployee(employee);

            // Log print action
            LogPrint(employee);
        }

        private void LogPrint(Employee employee)
        {
            // Log to database (using existing connection)
            try
            {
                using (var connection = new System.Data.SQLite.SQLiteConnection(DatabaseService.ConnectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO PrintLog (EmployeeId, IDCardNumber, PrintedDate, PrintedBy, PrintType)
                                  VALUES (@empId, @idcard, @date, @user, @type)";

                    using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@empId", employee.Id);
                        command.Parameters.AddWithValue("@idcard", employee.IDCardNumber ?? "");
                        command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@user", Environment.UserName);
                        command.Parameters.AddWithValue("@type", _printingBack ? "Back" : "Front");
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Silently ignore log errors
            }
        }

        /// <summary>
        /// Get list of installed printers
        /// </summary>
        public static string[] GetPrinters()
        {
            string[] printers = new string[PrinterSettings.InstalledPrinters.Count];
            PrinterSettings.InstalledPrinters.CopyTo(printers, 0);
            return printers;
        }

        /// <summary>
        /// Get default printer name
        /// </summary>
        public static string GetDefaultPrinter()
        {
            using (PrintDocument doc = new PrintDocument())
            {
                return doc.PrinterSettings.PrinterName;
            }
        }

        /// <summary>
        /// Check if printer exists
        /// </summary>
        public static bool PrinterExists(string printerName)
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.Equals(printerName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
