using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Utils;

namespace RailwayIDCardMaker.Services
{
    /// <summary>
    /// Card Renderer service - renders the ID card front and back
    /// Based on Indian Railways ID Card specifications:
    /// - Dimensions: 87mm (H) x 54mm (W) - Portrait orientation
    /// - Background: Bright Yellow
    /// - Photo: 4.85cm x 4.2cm (centered, 70% face coverage)
    /// - Signature box: 0.75cm x 4.2cm
    /// - QR Code (back): 2.35cm x 2.5cm
    /// </summary>
    public static class CardRenderer
    {
        // Scale factor for mm to pixels at 300 DPI
        private const float MM_TO_PX = 11.811f;

        // Card dimensions in pixels (at 300 DPI)
        private static readonly int CardWidth = (int)(54 * MM_TO_PX);   // ~638px
        private static readonly int CardHeight = (int)(87 * MM_TO_PX); // ~1028px

        // Fonts
        private static Font _headerFont;
        private static Font _subHeaderFont;
        private static Font _nameFont;
        private static Font _designationFont;
        private static Font _detailsFont;
        private static Font _smallFont;
        private static Font _bloodGroupFont;
        private static Font _idNumberFont;

        static CardRenderer()
        {
            InitializeFonts();
        }

        private static void InitializeFonts()
        {
            string fontFamily = Constants.PRIMARY_FONT_FAMILY;

            // Font sizes in PIXELS for proper sizing on 300 DPI card
            _headerFont = new Font(fontFamily, 36, FontStyle.Bold, GraphicsUnit.Pixel);
            _subHeaderFont = new Font(fontFamily, 28, FontStyle.Bold, GraphicsUnit.Pixel);
            _nameFont = new Font(fontFamily, 28, FontStyle.Bold, GraphicsUnit.Pixel);
            _designationFont = new Font(fontFamily, 20, FontStyle.Regular, GraphicsUnit.Pixel);
            _detailsFont = new Font(fontFamily, 18, FontStyle.Regular, GraphicsUnit.Pixel);
            _smallFont = new Font(fontFamily, 14, FontStyle.Regular, GraphicsUnit.Pixel);
            _bloodGroupFont = new Font(fontFamily, 48, FontStyle.Bold, GraphicsUnit.Pixel);
            _idNumberFont = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Render the front side of the ID card
        /// </summary>
        public static Bitmap RenderCardFront(Employee employee, Image logoImage = null)
        {
            Bitmap card = new Bitmap(CardWidth, CardHeight);
            card.SetResolution(300, 300);

            using (Graphics g = Graphics.FromImage(card))
            {
                // Set high quality rendering
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;

                // Fill background with yellow
                g.Clear(Constants.CARD_BACKGROUND_COLOR);

                // Draw border
                using (Pen borderPen = new Pen(Color.Black, 2))
                {
                    g.DrawRectangle(borderPen, 1, 1, CardWidth - 3, CardHeight - 3);
                }

                int margin = 25;
                int centerX = CardWidth / 2;

                // === HEADER SECTION ===
                int logoSize = 70;
                int headerY = 15;

                // Draw Indian Railways Logo (red circle placeholder)
                using (SolidBrush redBrush = new SolidBrush(Color.FromArgb(180, 30, 30)))
                using (Pen redPen = new Pen(Color.FromArgb(180, 30, 30), 2))
                {
                    g.DrawEllipse(redPen, margin, headerY, logoSize, logoSize);
                    using (Font logoFont = new Font("Arial", 24, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        StringFormat cf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        g.DrawString("IR", logoFont, redBrush, new RectangleF(margin, headerY, logoSize, logoSize), cf);
                    }
                }

                // Draw "Indian Railways" in RED - using PIXEL unit
                using (Font headerFont = new Font("Times New Roman", 36, FontStyle.Bold, GraphicsUnit.Pixel))
                using (SolidBrush redBrush = new SolidBrush(Constants.INDIAN_RAILWAYS_TEXT_COLOR))
                {
                    g.DrawString("Indian Railways", headerFont, redBrush, margin + logoSize + 10, headerY + 5);
                }

                // Draw "ID Card NO:" 
                using (Font idFont = new Font("Times New Roman", 18, FontStyle.Regular, GraphicsUnit.Pixel))
                using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                {
                    string idText = $"ID Card NO : {employee.IDCardNumber ?? "XXXXXX"}";
                    g.DrawString(idText, idFont, blackBrush, margin + logoSize + 10, headerY + 42);
                }

                // Draw "Employee Identity Card" in RED - centered
                using (Font subFont = new Font("Times New Roman", 28, FontStyle.Bold, GraphicsUnit.Pixel))
                using (SolidBrush redBrush = new SolidBrush(Constants.EMPLOYEE_CARD_TEXT_COLOR))
                {
                    StringFormat cf = new StringFormat { Alignment = StringAlignment.Center };
                    g.DrawString("Employee Identity Card", subFont, redBrush, centerX, headerY + 70, cf);
                }

                int currentY = 110;

                // === PHOTO SECTION ===

                // Calculate photo dimensions - sized for card
                int photoWidth = 380;
                int photoHeight = 450;
                int photoX = (CardWidth - photoWidth) / 2;
                int photoY = currentY;

                // Draw photo frame (white background)
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(whiteBrush, photoX - 3, photoY - 3,
                        photoWidth + 6, photoHeight + 6);
                }

                // Draw photo border
                using (Pen photoBorder = new Pen(Color.Black, 1))
                {
                    g.DrawRectangle(photoBorder, photoX - 3, photoY - 3,
                        photoWidth + 6, photoHeight + 6);
                }

                // Draw employee photo
                Image photo = null;
                if (!string.IsNullOrEmpty(employee.PhotoPath))
                {
                    photo = ImageService.LoadImage(employee.PhotoPath);
                }

                if (photo != null)
                {
                    g.DrawImage(photo, photoX, photoY, photoWidth, photoHeight);
                    photo.Dispose();
                }
                else
                {
                    // Draw placeholder
                    using (SolidBrush grayBrush = new SolidBrush(Color.LightGray))
                    {
                        g.FillRectangle(grayBrush, photoX, photoY, photoWidth, photoHeight);
                    }
                    using (SolidBrush textBrush = new SolidBrush(Color.Gray))
                    {
                        StringFormat centerFormat = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString("Photo", _detailsFont, textBrush,
                            new RectangleF(photoX, photoY, photoWidth, photoHeight), centerFormat);
                    }
                }

                currentY = photoY + photoHeight + 10;

                // === SIGNATURE BOX ===

                int sigWidth = 380;
                int sigHeight = 60;
                int sigX = (CardWidth - sigWidth) / 2;
                int sigY = currentY;

                // Draw signature box
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(whiteBrush, sigX, sigY, sigWidth, sigHeight);
                }
                using (Pen sigBorder = new Pen(Color.Black, 1))
                {
                    g.DrawRectangle(sigBorder, sigX, sigY, sigWidth, sigHeight);
                }

                // Draw signature if available
                if (!string.IsNullOrEmpty(employee.SignaturePath))
                {
                    Image signature = ImageService.LoadImage(employee.SignaturePath);
                    if (signature != null)
                    {
                        g.DrawImage(signature, sigX + 2, sigY + 2, sigWidth - 4, sigHeight - 4);
                        signature.Dispose();
                    }
                }

                // Signature label
                using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                {
                    g.DrawString("(Signature of Card Holder)", _smallFont, blackBrush,
                        centerX, sigY + sigHeight + 2, new StringFormat { Alignment = StringAlignment.Center });
                }

                currentY = sigY + sigHeight + 20;

                // === EMPLOYEE NAME ===

                using (SolidBrush nameBrush = new SolidBrush(Constants.NAME_TEXT_COLOR))
                {
                    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                    g.DrawString(employee.Name?.ToUpper() ?? "EMPLOYEE NAME", _nameFont, nameBrush,
                        centerX, currentY, centerFormat);
                }

                currentY += 35;

                // === ISSUING AUTHORITY SECTION ===

                // Authority signature line
                int authSigWidth = 350;
                int authSigX = (CardWidth - authSigWidth) / 2;

                using (Pen linePen = new Pen(Color.Black, 1))
                {
                    g.DrawLine(linePen, authSigX, currentY, authSigX + authSigWidth, currentY);
                }

                currentY += 5;

                // Issuing authority details
                using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                {
                    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };

                    string authorityText = employee.IssuingAuthority ?? "Issuing Authority";
                    string designationText = employee.IssuingAuthorityDesignation ?? "";

                    g.DrawString(authorityText, _detailsFont, blackBrush, centerX, currentY, centerFormat);
                    currentY += 18;
                    g.DrawString(designationText, _smallFont, blackBrush, centerX, currentY, centerFormat);
                }
            }

            return card;
        }

        /// <summary>
        /// Render the back side of the ID card
        /// </summary>
        public static Bitmap RenderCardBack(Employee employee)
        {
            Bitmap card = new Bitmap(CardWidth, CardHeight);
            card.SetResolution(300, 300);

            using (Graphics g = Graphics.FromImage(card))
            {
                // Set high quality rendering
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;

                // Fill background with yellow
                g.Clear(Constants.CARD_BACKGROUND_COLOR);

                // Draw border
                using (Pen borderPen = new Pen(Color.Black, 2))
                {
                    g.DrawRectangle(borderPen, 1, 1, CardWidth - 3, CardHeight - 3);
                }

                float leftMargin = 20;
                float rightMargin = CardWidth - 20;
                float currentY = 20;
                float centerX = CardWidth / 2f;

                // === QR CODE SECTION (Top-Left) ===

                int qrSize = 120; // Reasonable size for QR code
                int qrX = (int)leftMargin;
                int qrY = (int)currentY;

                // Generate and draw QR code
                using (Bitmap qrCode = QRCodeGenerator.GenerateEmployeeQRCode(employee, qrSize))
                {
                    // Draw white background for QR code
                    using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(whiteBrush, qrX - 3, qrY - 3, qrSize + 6, qrSize + 6);
                    }

                    g.DrawImage(qrCode, qrX, qrY, qrSize, qrSize);
                }

                // Draw QR border
                using (Pen qrBorder = new Pen(Color.Black, 1))
                {
                    g.DrawRectangle(qrBorder, qrX - 3, qrY - 3, qrSize + 6, qrSize + 6);
                }

                // === BLOOD GROUP (Top-Right) - LARGE ===

                int bloodBoxWidth = 120;
                int bloodBoxHeight = 100;
                int bloodX = CardWidth - (int)leftMargin - bloodBoxWidth;
                int bloodY = (int)currentY;

                // Draw blood group box
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(whiteBrush, bloodX, bloodY, bloodBoxWidth, bloodBoxHeight);
                }
                using (Pen boxBorder = new Pen(Color.Black, 1))
                {
                    g.DrawRectangle(boxBorder, bloodX, bloodY, bloodBoxWidth, bloodBoxHeight);
                }

                // Draw blood group text - LARGE RED
                using (Font largeBloodFont = new Font("Times New Roman", 48, FontStyle.Bold, GraphicsUnit.Pixel))
                using (SolidBrush redBrush = new SolidBrush(Constants.BLOOD_GROUP_COLOR))
                {
                    StringFormat centerFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(employee.BloodGroup ?? "-", largeBloodFont, redBrush,
                        new RectangleF(bloodX, bloodY, bloodBoxWidth, bloodBoxHeight), centerFormat);
                }

                currentY = Math.Max(qrY + qrSize, bloodY + bloodBoxHeight) + 20;

                // === DEPARTMENT BOX ===

                int deptBoxWidth = 590;
                int deptBoxHeight = 50;
                int deptX = 25;
                int deptY = (int)currentY;

                // Draw department box
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(whiteBrush, deptX, deptY, deptBoxWidth, deptBoxHeight);
                }
                using (Pen boxBorder = new Pen(Color.Black, 2))
                {
                    g.DrawRectangle(boxBorder, deptX, deptY, deptBoxWidth, deptBoxHeight);
                }

                // Draw department text - BOLD CENTERED
                using (Font deptFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel))
                using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                {
                    StringFormat centerFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(employee.Department?.ToUpper() ?? "DEPARTMENT", deptFont, blackBrush,
                        new RectangleF(deptX, deptY, deptBoxWidth, deptBoxHeight), centerFormat);
                }

                currentY = deptY + deptBoxHeight + 20;

                // === CENTERED EMPLOYEE DETAILS ===

                using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                {
                    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };

                    // Designation - Bold Centered
                    using (Font desigFont = new Font("Times New Roman", 20, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        g.DrawString(employee.Designation?.ToUpper() ?? "", desigFont, blackBrush, centerX, currentY, centerFormat);
                    }
                    currentY += 35;

                    // Mobile Number - Large Bold Centered
                    using (Font phoneFont = new Font("Times New Roman", 26, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        g.DrawString(employee.MobileNumber ?? "", phoneFont, blackBrush, centerX, currentY, centerFormat);
                    }
                    currentY += 40;

                    // Aadhaar (masked) - Centered with underline effect
                    using (Font aadhaarFont = new Font("Times New Roman", 18, FontStyle.Regular, GraphicsUnit.Pixel))
                    {
                        string maskedAadhaar = employee.GetMaskedAadhaar() ?? "XXXX-XXXX-0000";
                        g.DrawString(maskedAadhaar, aadhaarFont, blackBrush, centerX, currentY, centerFormat);
                    }
                    currentY += 30;

                    // Date of Issue
                    using (Font dateFont = new Font("Times New Roman", 14, FontStyle.Regular, GraphicsUnit.Pixel))
                    {
                        string issueText = $"Date of Issue: {employee.DateOfIssue?.ToString("dd-MM-yyyy") ?? ""}";
                        g.DrawString(issueText, dateFont, blackBrush, centerX, currentY, centerFormat);
                    }
                    currentY += 30;
                }

                // === INSTRUCTIONS SECTION ===

                using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                {
                    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };

                    // Instructions header
                    using (Font headerFont = new Font("Times New Roman", 14, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        g.DrawString("Instructions", headerFont, blackBrush, centerX, currentY, centerFormat);
                    }
                    currentY += 18;

                    // Instructions text
                    using (Font textFont = new Font("Times New Roman", 12, FontStyle.Regular, GraphicsUnit.Pixel))
                    {
                        g.DrawString(Constants.FOOTER_TEXT, textFont, blackBrush,
                            new RectangleF(leftMargin, currentY, CardWidth - 40, 50),
                            new StringFormat { Alignment = StringAlignment.Center });
                    }
                }
            }

            return card;
        }

        /// <summary>
        /// Render both front and back as a combined image (for preview)
        /// </summary>
        public static Bitmap RenderCardPreview(Employee employee, Image logoImage = null, bool showBothSides = true)
        {
            using (Bitmap front = RenderCardFront(employee, logoImage))
            {
                if (!showBothSides)
                {
                    return new Bitmap(front);
                }

                using (Bitmap back = RenderCardBack(employee))
                {
                    // Create combined image with both cards side by side
                    int gap = 20;
                    Bitmap combined = new Bitmap(CardWidth * 2 + gap, CardHeight);
                    combined.SetResolution(300, 300);

                    using (Graphics g = Graphics.FromImage(combined))
                    {
                        g.Clear(Color.White);
                        g.DrawImage(front, 0, 0, CardWidth, CardHeight);
                        g.DrawImage(back, CardWidth + gap, 0, CardWidth, CardHeight);

                        // Labels
                        using (Font labelFont = new Font("Arial", 10, FontStyle.Bold))
                        using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                        {
                            g.DrawString("FRONT", labelFont, blackBrush, CardWidth / 2 - 25, CardHeight + 5);
                            g.DrawString("BACK", labelFont, blackBrush, CardWidth + gap + CardWidth / 2 - 20, CardHeight + 5);
                        }
                    }

                    return combined;
                }
            }
        }

        /// <summary>
        /// Get scaled preview for display
        /// </summary>
        public static Bitmap GetScaledPreview(Bitmap original, float scale)
        {
            int newWidth = (int)(original.Width * scale);
            int newHeight = (int)(original.Height * scale);

            Bitmap scaled = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(scaled))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(original, 0, 0, newWidth, newHeight);
            }

            return scaled;
        }

        /// <summary>
        /// Export card to image file
        /// </summary>
        public static void ExportToImage(Employee employee, string frontPath, string backPath, Image logoImage = null)
        {
            using (Bitmap front = RenderCardFront(employee, logoImage))
            {
                front.Save(frontPath, System.Drawing.Imaging.ImageFormat.Png);
            }

            using (Bitmap back = RenderCardBack(employee))
            {
                back.Save(backPath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
