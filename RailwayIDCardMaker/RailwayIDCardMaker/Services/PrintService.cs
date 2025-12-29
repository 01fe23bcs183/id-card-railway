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
        private int _currentPage = 0;
        private PrintDocument _printDocument;

        public PrintService()
        {
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += PrintDocument_PrintPage;
            _printDocument.BeginPrint += (s, e) => { _currentPage = 0; };

            // Set ID Card paper size: 87mm x 54mm
            SetIDCardPaperSize();
        }

        /// <summary>
        /// Set paper size to ID Card (87mm x 54mm)
        /// </summary>
        private void SetIDCardPaperSize()
        {
            // ID Card dimensions in hundredths of an inch
            // 87mm = 3.425 inches = 342.5 (hundredths)
            // 54mm = 2.126 inches = 212.6 (hundredths)
            int widthHundredths = 213;  // 54mm
            int heightHundredths = 343; // 87mm

            PaperSize cardSize = new PaperSize("ID Card (87x54mm)", widthHundredths, heightHundredths);
            _printDocument.DefaultPageSettings.PaperSize = cardSize;

            // Set margins to minimal (for edge-to-edge printing)
            _printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
        }

        /// <summary>
        /// Print ID card with print dialog
        /// </summary>
        public DialogResult PrintCard(Employee employee, Image logoImage = null)
        {
            _currentEmployee = employee;
            _logoImage = logoImage;
            _currentPage = 0;

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
                        _printDocument.Print();
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
            _currentPage = 0;

            // Ensure paper size is set before preview
            SetIDCardPaperSize();

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = _printDocument;
                preview.Width = 600;
                preview.Height = 800;
                preview.ShowIcon = false;
                
                // Set zoom to show actual size (100%)
                // The PrintPreviewControl is accessed through the Controls collection
                foreach (Control ctrl in preview.Controls)
                {
                    if (ctrl is PrintPreviewControl previewCtrl)
                    {
                        previewCtrl.Zoom = 1.0; // 100% zoom for actual size
                        previewCtrl.AutoZoom = false;
                        break;
                    }
                }

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
            _currentPage = 0;

            try
            {
                if (!string.IsNullOrEmpty(printerName))
                {
                    _printDocument.PrinterSettings.PrinterName = printerName;
                }

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
                e.HasMorePages = false;
                return;
            }

            // Generate card image - Front on page 1, Back on page 2
            Bitmap cardImage = null;
            try
            {
                if (_currentPage == 0)
                {
                    // Print FRONT
                    cardImage = CardRenderer.RenderCardFront(_currentEmployee, _logoImage);
                }
                else
                {
                    // Print BACK
                    cardImage = CardRenderer.RenderCardBack(_currentEmployee);
                }

                // ID Card size: 54mm x 87mm
                // Print at exact size from top-left corner (edge-to-edge)
                float cardWidthInch = 54f / 25.4f;   // 54mm = 2.126 inches
                float cardHeightInch = 87f / 25.4f;  // 87mm = 3.425 inches

                // Convert to points (100 points per inch)
                float printWidth = cardWidthInch * 100;
                float printHeight = cardHeightInch * 100;

                // Print from origin (0,0) for edge-to-edge printing
                e.Graphics.DrawImage(cardImage, 0, 0, printWidth, printHeight);

                // Check if we need to print more pages
                _currentPage++;
                e.HasMorePages = (_currentPage < 2); // 2 pages: front and back
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Print error: {ex.Message}");
                e.HasMorePages = false;
            }
            finally
            {
                if (cardImage != null)
                {
                    cardImage.Dispose();
                }
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
            // Use DatabaseService to log print action
            try
            {
                string username = Forms.LoginForm.CurrentUser?.Username ?? Environment.UserName;
                DatabaseService.LogPrint(employee.Id, employee.IDCardNumber, username, "FrontAndBack");
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
