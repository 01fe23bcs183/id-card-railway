using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Utils;

namespace RailwayIDCardMaker.Services
{
    public static class CardRenderer
    {
        // 300 DPI constants
        private const float DPI = 300f;
        private const float CM_TO_PX = DPI / 2.54f; // ~118.11 px per cm

        // Card Dimensions: 5.4cm x 8.75cm
        private static readonly int W = (int)(5.4f * CM_TO_PX);  // 638
        private static readonly int H = (int)(8.75f * CM_TO_PX); // 1033

        private static readonly Color YELLOW = Color.FromArgb(255, 255, 0);
        private static readonly Color RED = Color.FromArgb(180, 0, 0);

        public static Bitmap RenderCardFront(Employee emp, Image logo = null)
        {
            var bmp = new Bitmap(W, H);
            bmp.SetResolution(300, 300);

            using (var g = Graphics.FromImage(bmp))
            {
                SetQuality(g);
                g.Clear(YELLOW);

                // Draw Border
                g.DrawRectangle(new Pen(Color.Black, 2), 2, 2, W - 4, H - 4);

                // Helper for Centering
                var sfCenter = new StringFormat { Alignment = StringAlignment.Center };

                // ============================================
                // HEADER SECTION
                // ============================================
                // Logo Size approx 1.2cm ? Let's guess based on spacing
                int logoSize = (int)(1.2f * CM_TO_PX);
                DrawAshokChakra(g, 20, 20, logoSize);

                // Ministry of Railways - Font Size 12 (approx 50px at 300dpi)
                // Positioned to right of logo
                int headerX = 20 + logoSize + 10;
                using (var f = new Font("Times New Roman", 48, FontStyle.Bold, GraphicsUnit.Pixel))
                    g.DrawString("Ministry of Railways", f, Brushes.Black, headerX, 20);

                // Government of India - Font Size 8 (approx 33px)
                using (var f = new Font("Times New Roman", 32, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    g.DrawString("Government of India", f, Brushes.Black, headerX, 75);
                    g.DrawString("No " + (emp.IDCardNumber ?? "251001XXXXXX"), f, Brushes.Black, headerX, 110);
                }

                // ============================================
                // PHOTO SECTION (Height 4.85 cm)
                // ============================================
                int photoH = (int)(4.85f * CM_TO_PX); // ~573 px
                int photoW = (int)(3.8f * CM_TO_PX);  // Width slightly less than sig box
                int photoY = 160;
                int photoX = (W - photoW) / 2;

                // Photo Rounded Rectangle
                using (var path = CreateRoundedRect(photoX, photoY, photoW, photoH, 20))
                {
                    g.FillPath(Brushes.White, path);
                    g.DrawPath(new Pen(Color.Gray, 2), path);
                }

                if (!string.IsNullOrEmpty(emp.PhotoPath))
                {
                    var img = ImageService.LoadImage(emp.PhotoPath);
                    if (img != null)
                    {
                        using (var clip = CreateRoundedRect(photoX + 2, photoY + 2, photoW - 4, photoH - 4, 18))
                        {
                            g.SetClip(clip);
                            g.DrawImage(img, photoX + 2, photoY + 2, photoW - 4, photoH - 4);
                            g.ResetClip();
                        }
                        img.Dispose();
                    }
                }
                else
                {
                    // Placeholder Text
                    using (var f = new Font("Times New Roman", 30, FontStyle.Bold, GraphicsUnit.Pixel))
                        g.DrawString("Employee Photo", f, Brushes.Black, photoX + photoW / 2, photoY + photoH / 2 - 15, sfCenter);
                }

                // Vertical Validity Text: "Valid Upto DD/MM/YYYY" - Font Size 6 (~25px)
                if (emp.ValidityDate.HasValue)
                {
                    string validText = "Valid Upto: " + emp.ValidityDate.Value.ToString("dd/MM/yyyy");
                    DrawVerticalText(g, validText, photoX - 25, photoY + photoH - 10, 24);
                }

                // ============================================
                // SIGNATURE BOX (Width 4.2cm, Height 0.75cm)
                // ============================================
                int sigW = (int)(4.2f * CM_TO_PX); // ~496 px
                int sigH = (int)(0.75f * CM_TO_PX); // ~88 px
                int sigY = photoY + photoH + 10;
                int sigX = (W - sigW) / 2;

                g.FillRectangle(Brushes.White, sigX, sigY, sigW, sigH);
                g.DrawRectangle(new Pen(Color.Black, 2), sigX, sigY, sigW, sigH);

                if (!string.IsNullOrEmpty(emp.SignaturePath))
                {
                    var sig = ImageService.LoadImage(emp.SignaturePath);
                    if (sig != null) { g.DrawImage(sig, sigX + 5, sigY + 5, sigW - 10, sigH - 10); sig.Dispose(); }
                }

                // "Signature of card Holder" label - Small font
                using (var f = new Font("Times New Roman", 20, FontStyle.Regular, GraphicsUnit.Pixel))
                    g.DrawString("Signature of card Holder", f, Brushes.Black, W / 2, sigY + sigH + 5, sfCenter);


                // ============================================
                // DETAILS SECTION
                // ============================================
                // Name: Size 12 (~50px)
                int detailsY = sigY + sigH + 40;
                using (var f = new Font("Times New Roman", 48, FontStyle.Bold, GraphicsUnit.Pixel))
                    g.DrawString("Name : " + (emp.Name ?? "XXXXX"), f, Brushes.Black, 30, detailsY);

                // Designation: Size 9 (~38px)
                detailsY += 55;
                using (var f = new Font("Times New Roman", 36, FontStyle.Regular, GraphicsUnit.Pixel))
                    g.DrawString("Designation: " + (emp.Designation ?? "XXXXX/DDDD"), f, Brushes.Black, 30, detailsY);

                // Issuing Authority
                int authX = W - 220;
                int authY = detailsY + 20;
                if (!string.IsNullOrEmpty(emp.AuthoritySignaturePath))
                {
                    var authSig = ImageService.LoadImage(emp.AuthoritySignaturePath);
                    if (authSig != null)
                    {
                        int authSigW = 160;
                        int authSigH = 50;
                        int authSigX = authX + 20;
                        int authSigY = authY - 80;
                        g.DrawImage(authSig, authSigX, authSigY, authSigW, authSigH);
                        authSig.Dispose();
                    }
                }
                using (var f = new Font("Times New Roman", 20, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    var sfRight = new StringFormat { Alignment = StringAlignment.Center };
                    g.DrawString("(Signature)", f, Brushes.Black, authX + 100, authY - 25, sfRight); // Place above designation
                    g.DrawString("Designation of Issuing Authority", f, Brushes.Black, authX + 100, authY, sfRight);

                    // Arrow pointing to signature (optional decorative)
                    // g.DrawLine(new Pen(Color.Black, 1), authX + 100, authY - 10, authX + 100, authY - 35);
                }
            }
            return bmp;
        }

        public static Bitmap RenderCardBack(Employee emp)
        {
            var bmp = new Bitmap(W, H);
            bmp.SetResolution(300, 300);

            using (var g = Graphics.FromImage(bmp))
            {
                SetQuality(g);
                g.Clear(YELLOW);
                g.DrawRectangle(new Pen(Color.Black, 2), 2, 2, W - 4, H - 4);

                var sfCenter = new StringFormat { Alignment = StringAlignment.Center };
                var sfCenterBoth = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                int topMargin = 30;

                // ============================================
                // TOP SECTION: QR & BLOOD GROUP
                // Size: 2.35cm x 2.5cm
                // ============================================
                int boxW = (int)(2.35f * CM_TO_PX); // ~277 px
                int boxH = (int)(2.5f * CM_TO_PX);  // ~295 px

                int gap = W - (2 * boxW) - 60; // Calculate gap to center them reasonably or fit to measurement arrows
                                               // Arrows show width 4.95 cm for department box below, which matches 2.35 + 2.35 + gap?
                                               // 2.35 + 2.35 = 4.7. So gap is approx 0.25cm.

                int leftX = (W - (int)(4.95f * CM_TO_PX)) / 2; // X starting point aligned with Dept box
                int rightX = leftX + (int)(4.95f * CM_TO_PX) - boxW;

                // QR Code (Left)
                g.FillRectangle(Brushes.White, leftX, topMargin, boxW, boxH);
                g.DrawRectangle(new Pen(Color.Black, 2), leftX, topMargin, boxW, boxH);

                // Label "QR Code" inside if needed, or actual QR
                try
                {
                    int qrPadding = 10;
                    int qrSize = Math.Min(boxW, boxH) - (qrPadding * 2);
                    if (qrSize < 1) qrSize = 1;

                    string qrPayload = string.IsNullOrWhiteSpace(emp?.QRCodeUrl)
                        ? null
                        : emp.QRCodeUrl.Trim();
                    var qr = string.IsNullOrWhiteSpace(qrPayload)
                        ? QRCodeGenerator.GenerateEmployeeDataQRCode(emp, qrSize)
                        : QRCodeGenerator.GenerateQRCode(qrPayload, qrSize);
                    if (qr != null)
                    {
                        int qrX = leftX + (boxW - qrSize) / 2;
                        int qrY = topMargin + (boxH - qrSize) / 2;
                        g.DrawImage(qr, qrX, qrY, qrSize, qrSize);
                        qr.Dispose();
                    }
                }
                catch { }

                // Blood Group (Right)
                g.FillRectangle(Brushes.White, rightX, topMargin, boxW, boxH);
                g.DrawRectangle(new Pen(Color.Black, 2), rightX, topMargin, boxW, boxH);

                using (var f = new Font("Times New Roman", 100, FontStyle.Bold, GraphicsUnit.Pixel))
                    g.DrawString(emp.BloodGroup ?? "B+", f, Brushes.Black, new RectangleF(rightX, topMargin, boxW, boxH), sfCenterBoth);

                // ============================================
                // DEPARTMENT BOX
                // Size: 4.95cm width, 1.5cm height
                // ============================================
                int deptW = (int)(4.95f * CM_TO_PX); // ~585 px
                int deptH = (int)(1.5f * CM_TO_PX);  // ~177 px
                int deptY = topMargin + boxH + 15;
                int deptX = (W - deptW) / 2;

                g.FillRectangle(Brushes.White, deptX, deptY, deptW, deptH);
                g.DrawRectangle(new Pen(Color.Black, 2), deptX, deptY, deptW, deptH);

                // Text: DEPARTMENT
                // Reference font size says "size 18 - 48". Department looks big.
                using (var f = new Font("Times New Roman", 64, FontStyle.Bold, GraphicsUnit.Pixel))
                    g.DrawString((emp.Department ?? "DEPARTMENT").ToUpper(), f, Brushes.Black, new RectangleF(deptX, deptY, deptW, deptH), sfCenterBoth);


                // ============================================
                // DETAILS BELOW
                // ============================================
                int currentY = deptY + deptH + 20;

                // Mobile Number (Large Font ~48px?)
                using (var f = new Font("Times New Roman", 60, FontStyle.Bold, GraphicsUnit.Pixel))
                    g.DrawString(emp.MobileNumber ?? "9989999999", f, Brushes.Black, W / 2, currentY, sfCenter);

                currentY += 70;

                // Aadhaar (Medium Font ~24px?)
                using (var f = new Font("Times New Roman", 40, FontStyle.Bold, GraphicsUnit.Pixel))
                    g.DrawString(emp.GetMaskedAadhaar() ?? "XXXX-XXXX-4545", f, Brushes.Black, W / 2, currentY, sfCenter);

                currentY += 60;

                // Date of Issue
                using (var f = new Font("Times New Roman", 30, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    string doi = emp.DateOfIssue.HasValue ? "Date of Issue: " + emp.DateOfIssue.Value.ToString("dd-MM-yyyy") : "Date of Issue: ";
                    g.DrawString(doi, f, Brushes.Black, W / 2, currentY, sfCenter);
                }

                currentY += 40;

                // Line
                g.DrawLine(new Pen(Color.Black, 2), 60, currentY, W - 60, currentY);
                currentY += 10;

                // Instructions (Font Size 6 ~25px)
                using (var f = new Font("Times New Roman", 24, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    g.DrawString("Instruction", f, Brushes.Black, W / 2, currentY, sfCenter);
                    currentY += 25;
                    g.DrawString("Please surrender to issuing Authority on transfer", f, Brushes.Black, W / 2, currentY, sfCenter);
                    g.DrawString("promotion/completion/termination of Railway service", f, Brushes.Black, W / 2, currentY + 25, sfCenter);
                }
            }
            return bmp;
        }

        #region Helpers

        private static void SetQuality(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

        private static void DrawAshokChakra(Graphics g, int x, int y, int sz)
        {
            using (var p = new Pen(Color.Black, 2)) g.DrawEllipse(p, x, y, sz, sz); // Simplistic circle for now or load real logo

            // If we have a fancy drawer, use it, but spec often just shows the emblem image
            // Inner Layout
            int cx = x + sz / 2, cy = y + sz / 2;
            using (var b = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                g.FillEllipse(b, x, y, sz, sz);

            // Just a placeholder drawing for the Chakra if image missing
            // In real app, LogoManager is better, but here we draw basic
            using (var p = new Pen(Color.DarkBlue, 2))
            {
                g.DrawEllipse(p, x, y, sz, sz);
                g.DrawEllipse(p, x + sz / 2 - 2, y + sz / 2 - 2, 4, 4);
                for (int i = 0; i < 24; i++)
                {
                    double a = i * Math.PI * 2 / 24;
                    g.DrawLine(p, cx, cy, cx + (int)(sz / 2 * Math.Cos(a)), cy + (int)(sz / 2 * Math.Sin(a)));
                }
            }
        }

        private static void DrawVerticalText(Graphics g, string text, int x, int y, int fontSize)
        {
            var st = g.Save();
            g.TranslateTransform(x, y);
            g.RotateTransform(-90);
            using (var f = new Font("Times New Roman", fontSize, FontStyle.Regular, GraphicsUnit.Pixel))
                g.DrawString(text, f, Brushes.Black, 0, 0);
            g.Restore(st);
        }

        private static GraphicsPath CreateRoundedRect(int x, int y, int w, int h, int r)
        {
            var p = new GraphicsPath();
            int d = r * 2;
            p.AddArc(x, y, d, d, 180, 90);
            p.AddArc(x + w - d, y, d, d, 270, 90);
            p.AddArc(x + w - d, y + h - d, d, d, 0, 90);
            p.AddArc(x, y + h - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        #endregion

        #region Public API

        public static Bitmap RenderCardPreview(Employee emp, Image logo = null, bool both = true)
        {
            using (var front = RenderCardFront(emp, logo))
            {
                if (!both) return new Bitmap(front);
                using (var back = RenderCardBack(emp))
                {
                    var combined = new Bitmap(W * 2 + 50, H + 50);
                    combined.SetResolution(300, 300);

                    using (var g = Graphics.FromImage(combined))
                    {
                        g.Clear(Color.White);

                        // Draw Front
                        g.DrawImage(front, 0, 0);
                        g.DrawRectangle(Pens.Black, 0, 0, W, H);

                        // Draw Back
                        g.DrawImage(back, W + 40, 0);
                        g.DrawRectangle(Pens.Black, W + 40, 0, W, H);

                        // Labels
                        using (var f = new Font("Arial", 24, FontStyle.Bold, GraphicsUnit.Pixel))
                        {
                            var sf = new StringFormat { Alignment = StringAlignment.Center };
                            g.DrawString("Front view", f, Brushes.Black, W / 2, H + 10, sf);
                            g.DrawString("Back view", f, Brushes.Black, W + 40 + W / 2, H + 10, sf);
                        }
                    }
                    return combined;
                }
            }
        }

        public static Bitmap GetScaledPreview(Bitmap orig, float scale)
        {
            int nw = (int)(orig.Width * scale), nh = (int)(orig.Height * scale);
            var scaled = new Bitmap(nw, nh);
            using (var g = Graphics.FromImage(scaled))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(orig, 0, 0, nw, nh);
            }
            return scaled;
        }

        public static void ExportToImage(Employee emp, string frontPath, string backPath, Image logo = null)
        {
            using (var f = RenderCardFront(emp, logo)) f.Save(frontPath, System.Drawing.Imaging.ImageFormat.Png);
            using (var b = RenderCardBack(emp)) b.Save(backPath, System.Drawing.Imaging.ImageFormat.Png);
        }

        #endregion
    }
}
