using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RailwayIDCardMaker.Services
{
    /// <summary>
    /// Image processing service for photos and signatures
    /// </summary>
    public static class ImageService
    {
        /// <summary>
        /// Load image from file
        /// </summary>
        public static Image LoadImage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return null;
            }

            try
            {
                // Load image without locking the file
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    return Image.FromStream(stream);
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Save image to file
        /// </summary>
        public static bool SaveImage(Image image, string filePath, ImageFormat format = null)
        {
            if (image == null || string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            try
            {
                if (format == null)
                {
                    format = ImageFormat.Png;
                }

                // Ensure directory exists
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                image.Save(filePath, format);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Save employee photo with proper sizing
        /// </summary>
        public static string SaveEmployeePhoto(Image photo, string employeeId)
        {
            if (photo == null)
            {
                return null;
            }

            string directory = Utils.Helpers.GetPhotosDirectory();
            string filename = Utils.Helpers.GeneratePhotoFilename(employeeId);
            string filePath = Path.Combine(directory, filename);

            // Resize and crop to proper dimensions
            using (var resized = ResizeAndCropForCard(photo,
                Utils.Constants.PhotoWidthPixels,
                Utils.Constants.PhotoHeightPixels))
            {
                if (SaveImage(resized, filePath, ImageFormat.Png))
                {
                    return filePath;
                }
            }

            return null;
        }

        /// <summary>
        /// Save employee signature
        /// </summary>
        public static string SaveEmployeeSignature(Image signature, string employeeId)
        {
            if (signature == null)
            {
                return null;
            }

            string directory = Utils.Helpers.GetSignaturesDirectory();
            string filename = Utils.Helpers.GenerateSignatureFilename(employeeId);
            string filePath = Path.Combine(directory, filename);

            if (SaveImage(signature, filePath, ImageFormat.Png))
            {
                return filePath;
            }

            return null;
        }

        /// <summary>
        /// Resize and crop image to fit card dimensions
        /// Maintains aspect ratio and centers the crop
        /// </summary>
        public static Image ResizeAndCropForCard(Image source, int targetWidth, int targetHeight)
        {
            if (source == null)
            {
                return null;
            }

            // Calculate aspect ratios
            double sourceAspect = (double)source.Width / source.Height;
            double targetAspect = (double)targetWidth / targetHeight;

            int newWidth, newHeight;

            if (sourceAspect > targetAspect)
            {
                // Source is wider, fit by height
                newHeight = targetHeight;
                newWidth = (int)(targetHeight * sourceAspect);
            }
            else
            {
                // Source is taller, fit by width
                newWidth = targetWidth;
                newHeight = (int)(targetWidth / sourceAspect);
            }

            // Create resized image
            Bitmap resized = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                g.DrawImage(source, 0, 0, newWidth, newHeight);
            }

            // Crop to target size (center crop)
            Bitmap cropped = new Bitmap(targetWidth, targetHeight);
            int cropX = (newWidth - targetWidth) / 2;
            int cropY = (newHeight - targetHeight) / 2;

            using (Graphics g = Graphics.FromImage(cropped))
            {
                g.DrawImage(resized,
                    new Rectangle(0, 0, targetWidth, targetHeight),
                    new Rectangle(cropX, cropY, targetWidth, targetHeight),
                    GraphicsUnit.Pixel);
            }

            resized.Dispose();
            return cropped;
        }

        /// <summary>
        /// Resize image maintaining aspect ratio
        /// </summary>
        public static Image ResizeImage(Image source, int maxWidth, int maxHeight)
        {
            if (source == null)
            {
                return null;
            }

            double ratioX = (double)maxWidth / source.Width;
            double ratioY = (double)maxHeight / source.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(source.Width * ratio);
            int newHeight = (int)(source.Height * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(source, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        /// <summary>
        /// Convert image to grayscale
        /// </summary>
        public static Image ConvertToGrayscale(Image source)
        {
            if (source == null)
            {
                return null;
            }

            Bitmap grayscale = new Bitmap(source.Width, source.Height);

            using (Graphics g = Graphics.FromImage(grayscale))
            {
                // Create grayscale color matrix
                ColorMatrix colorMatrix = new ColorMatrix(
                    new float[][]
                    {
                        new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                        new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                        new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                    });

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(source,
                    new Rectangle(0, 0, source.Width, source.Height),
                    0, 0, source.Width, source.Height,
                    GraphicsUnit.Pixel, attributes);
            }

            return grayscale;
        }

        /// <summary>
        /// Adjust image brightness and contrast
        /// </summary>
        public static Image AdjustBrightnessContrast(Image source, float brightness, float contrast)
        {
            if (source == null)
            {
                return null;
            }

            Bitmap adjusted = new Bitmap(source.Width, source.Height);

            float adjustedBrightness = brightness - 1.0f;

            // Create brightness/contrast matrix
            float[][] colorMatrixElements = {
                new float[] {contrast, 0, 0, 0, 0},
                new float[] {0, contrast, 0, 0, 0},
                new float[] {0, 0, contrast, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            using (Graphics g = Graphics.FromImage(adjusted))
            {
                g.DrawImage(source,
                    new Rectangle(0, 0, source.Width, source.Height),
                    0, 0, source.Width, source.Height,
                    GraphicsUnit.Pixel, attributes);
            }

            return adjusted;
        }

        /// <summary>
        /// Create placeholder image with text
        /// </summary>
        public static Image CreatePlaceholder(int width, int height, string text, Color backgroundColor, Color textColor)
        {
            Bitmap placeholder = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.Clear(backgroundColor);

                using (Font font = new Font("Arial", 10, FontStyle.Regular))
                using (SolidBrush brush = new SolidBrush(textColor))
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    g.DrawString(text, font, brush,
                        new RectangleF(0, 0, width, height), format);
                }

                // Draw border
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    g.DrawRectangle(pen, 0, 0, width - 1, height - 1);
                }
            }

            return placeholder;
        }

        /// <summary>
        /// Create photo placeholder
        /// </summary>
        public static Image CreatePhotoPlaceholder()
        {
            return CreatePlaceholder(
                Utils.Constants.PhotoWidthPixels,
                Utils.Constants.PhotoHeightPixels,
                "Photo\nNot Available",
                Color.LightGray,
                Color.DarkGray);
        }

        /// <summary>
        /// Check if file is a valid image
        /// </summary>
        public static bool IsValidImageFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return false;
            }

            string extension = Path.GetExtension(filePath).ToLower();
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff" };

            foreach (string ext in validExtensions)
            {
                if (extension == ext)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get image file filter for file dialogs
        /// </summary>
        public static string GetImageFileFilter()
        {
            return "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" +
                   "JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                   "PNG Files (*.png)|*.png|" +
                   "Bitmap Files (*.bmp)|*.bmp|" +
                   "All Files (*.*)|*.*";
        }
    }
}
