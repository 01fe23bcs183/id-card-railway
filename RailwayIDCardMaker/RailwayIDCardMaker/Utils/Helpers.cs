using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RailwayIDCardMaker.Utils
{
    /// <summary>
    /// Utility helper methods
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Hash password using SHA256
        /// </summary>
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Mask Aadhaar number (show only last 4 digits)
        /// Format: XXXX-XXXX-1234
        /// </summary>
        public static string MaskAadhaar(string aadhaar)
        {
            if (string.IsNullOrEmpty(aadhaar) || aadhaar.Length < 4)
                return aadhaar;

            // Remove any existing formatting
            string cleanAadhaar = aadhaar.Replace("-", "").Replace(" ", "");

            if (cleanAadhaar.Length != 12)
                return aadhaar;

            string lastFour = cleanAadhaar.Substring(8, 4);
            return $"XXXX-XXXX-{lastFour}";
        }

        /// <summary>
        /// Format Aadhaar number with dashes
        /// Format: 1234-5678-9012
        /// </summary>
        public static string FormatAadhaar(string aadhaar)
        {
            if (string.IsNullOrEmpty(aadhaar))
                return aadhaar;

            string cleanAadhaar = aadhaar.Replace("-", "").Replace(" ", "");

            if (cleanAadhaar.Length != 12)
                return aadhaar;

            return $"{cleanAadhaar.Substring(0, 4)}-{cleanAadhaar.Substring(4, 4)}-{cleanAadhaar.Substring(8, 4)}";
        }

        /// <summary>
        /// Validate Aadhaar number (basic validation - 12 digits)
        /// </summary>
        public static bool IsValidAadhaar(string aadhaar)
        {
            if (string.IsNullOrEmpty(aadhaar))
                return false;

            string cleanAadhaar = aadhaar.Replace("-", "").Replace(" ", "");

            if (cleanAadhaar.Length != 12)
                return false;

            foreach (char c in cleanAadhaar)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Validate mobile number (10 digits starting with 6-9)
        /// </summary>
        public static bool IsValidMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
                return false;

            string cleanMobile = mobile.Replace(" ", "").Replace("-", "");

            if (cleanMobile.Length != 10)
                return false;

            if (cleanMobile[0] < '6' || cleanMobile[0] > '9')
                return false;

            foreach (char c in cleanMobile)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Resize image to specified dimensions maintaining aspect ratio
        /// </summary>
        public static Image ResizeImage(Image image, int maxWidth, int maxHeight, bool maintainAspectRatio = true)
        {
            if (image == null)
                return null;

            int newWidth, newHeight;

            if (maintainAspectRatio)
            {
                double ratioX = (double)maxWidth / image.Width;
                double ratioY = (double)maxHeight / image.Height;
                double ratio = Math.Min(ratioX, ratioY);

                newWidth = (int)(image.Width * ratio);
                newHeight = (int)(image.Height * ratio);
            }
            else
            {
                newWidth = maxWidth;
                newHeight = maxHeight;
            }

            Bitmap destImage = new Bitmap(newWidth, newHeight);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight),
                        0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Crop image to center with specified dimensions
        /// </summary>
        public static Image CropToCenter(Image image, int width, int height)
        {
            if (image == null)
                return null;

            int sourceX = (image.Width - width) / 2;
            int sourceY = (image.Height - height) / 2;

            if (sourceX < 0) sourceX = 0;
            if (sourceY < 0) sourceY = 0;

            int cropWidth = Math.Min(width, image.Width);
            int cropHeight = Math.Min(height, image.Height);

            Bitmap destImage = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                graphics.Clear(Color.White);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                int destX = (width - cropWidth) / 2;
                int destY = (height - cropHeight) / 2;

                graphics.DrawImage(image,
                    new Rectangle(destX, destY, cropWidth, cropHeight),
                    new Rectangle(sourceX, sourceY, cropWidth, cropHeight),
                    GraphicsUnit.Pixel);
            }

            return destImage;
        }

        /// <summary>
        /// Convert image to byte array
        /// </summary>
        public static byte[] ImageToBytes(Image image, ImageFormat format = null)
        {
            if (image == null)
                return null;

            if (format == null)
                format = ImageFormat.Png;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Convert byte array to image
        /// </summary>
        public static Image BytesToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        /// <summary>
        /// Ensure directory exists
        /// </summary>
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Get application data directory (in Documents folder)
        /// </summary>
        public static string GetAppDataDirectory()
        {
            // Use Documents folder for easier access
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string appPath = Path.Combine(documentsPath, "RailwayIDCardMaker");
            EnsureDirectoryExists(appPath);
            return appPath;
        }

        /// <summary>
        /// Get photos directory
        /// </summary>
        public static string GetPhotosDirectory()
        {
            string path = Path.Combine(GetAppDataDirectory(), Constants.PHOTOS_FOLDER);
            EnsureDirectoryExists(path);
            return path;
        }

        /// <summary>
        /// Get signatures directory
        /// </summary>
        public static string GetSignaturesDirectory()
        {
            string path = Path.Combine(GetAppDataDirectory(), Constants.SIGNATURES_FOLDER);
            EnsureDirectoryExists(path);
            return path;
        }

        /// <summary>
        /// Get exports directory
        /// </summary>
        public static string GetExportsDirectory()
        {
            string path = Path.Combine(GetAppDataDirectory(), Constants.EXPORTS_FOLDER);
            EnsureDirectoryExists(path);
            return path;
        }

        /// <summary>
        /// Get backups directory
        /// </summary>
        public static string GetBackupsDirectory()
        {
            string path = Path.Combine(GetAppDataDirectory(), Constants.BACKUPS_FOLDER);
            EnsureDirectoryExists(path);
            return path;
        }

        /// <summary>
        /// Generate unique filename for photo
        /// </summary>
        public static string GeneratePhotoFilename(string employeeId)
        {
            return $"photo_{employeeId}_{DateTime.Now:yyyyMMddHHmmss}.png";
        }

        /// <summary>
        /// Generate unique filename for signature
        /// </summary>
        public static string GenerateSignatureFilename(string employeeId)
        {
            return $"sig_{employeeId}_{DateTime.Now:yyyyMMddHHmmss}.png";
        }

        /// <summary>
        /// Generate unique filename for authority signature
        /// </summary>
        public static string GenerateAuthoritySignatureFilename(string employeeId)
        {
            return $"authsig_{employeeId}_{DateTime.Now:yyyyMMddHHmmss}.png";
        }

        /// <summary>
        /// Format date for display
        /// </summary>
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

        /// <summary>
        /// Parse date from string
        /// </summary>
        public static DateTime? ParseDate(string dateString)
        {
            DateTime result;
            if (DateTime.TryParseExact(dateString, "dd-MM-yyyy", null,
                System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Get current financial year (April to March)
        /// </summary>
        public static int GetCurrentFinancialYear()
        {
            DateTime now = DateTime.Now;
            if (now.Month >= 4)
                return now.Year;
            else
                return now.Year - 1;
        }

        /// <summary>
        /// Get 2-digit year code for ID number
        /// </summary>
        public static string GetYearCode()
        {
            return (GetCurrentFinancialYear() % 100).ToString("D2");
        }
    }
}
