using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RailwayIDCardMaker.Utils
{
    /// <summary>
    /// Photo validation utility for ID card photos
    /// Validates photo composition requirements per Indian Railways specifications
    /// </summary>
    public static class PhotoValidator
    {
        // Photo requirements
        private const int MIN_WIDTH = 300;
        private const int MIN_HEIGHT = 350;
        private const float MIN_ASPECT_RATIO = 0.75f;  // Portrait orientation
        private const float MAX_ASPECT_RATIO = 0.95f;
        private const int MIN_FILE_SIZE_KB = 10;
        private const int MAX_FILE_SIZE_KB = 500;

        /// <summary>
        /// Validation result with details
        /// </summary>
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string Message { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public float AspectRatio { get; set; }
            public long FileSizeKB { get; set; }
            public bool IsFaceDetected { get; set; }
            public float FaceCoveragePercent { get; set; }
        }

        /// <summary>
        /// Validate photo for ID card usage
        /// Checks: dimensions, aspect ratio, file size, and basic face detection
        /// </summary>
        public static ValidationResult ValidatePhoto(string imagePath)
        {
            var result = new ValidationResult { IsValid = false };

            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                result.Message = "Photo file not found.";
                return result;
            }

            try
            {
                // Check file size
                var fileInfo = new FileInfo(imagePath);
                result.FileSizeKB = fileInfo.Length / 1024;

                if (result.FileSizeKB < MIN_FILE_SIZE_KB)
                {
                    result.Message = $"Photo file too small. Minimum {MIN_FILE_SIZE_KB}KB required.";
                    return result;
                }

                if (result.FileSizeKB > MAX_FILE_SIZE_KB)
                {
                    result.Message = $"Photo file too large. Maximum {MAX_FILE_SIZE_KB}KB allowed.";
                    return result;
                }

                // Load and check dimensions
                using (var img = Image.FromFile(imagePath))
                {
                    result.Width = img.Width;
                    result.Height = img.Height;
                    result.AspectRatio = (float)img.Width / img.Height;

                    // Check minimum dimensions
                    if (img.Width < MIN_WIDTH || img.Height < MIN_HEIGHT)
                    {
                        result.Message = $"Photo too small. Minimum {MIN_WIDTH}x{MIN_HEIGHT} pixels required. Current: {img.Width}x{img.Height}";
                        return result;
                    }

                    // Check aspect ratio (should be portrait)
                    if (result.AspectRatio < MIN_ASPECT_RATIO || result.AspectRatio > MAX_ASPECT_RATIO)
                    {
                        result.Message = $"Photo aspect ratio should be portrait (4:5 to 1:1). Current ratio: {result.AspectRatio:F2}";
                        return result;
                    }

                    // Basic face detection using skin tone analysis
                    result.FaceCoveragePercent = EstimateFaceCoverage((Bitmap)img);
                    result.IsFaceDetected = result.FaceCoveragePercent >= 20;

                    if (!result.IsFaceDetected)
                    {
                        result.Message = "Could not detect face in photo. Please ensure face is clearly visible.";
                        return result;
                    }

                    // Check 70% face coverage (center region should contain face)
                    if (result.FaceCoveragePercent < 50)
                    {
                        result.Message = $"Face coverage is {result.FaceCoveragePercent:F0}%. Recommended: 70% or more. Please crop photo to show face prominently.";
                        // Still allow but warn
                        result.IsValid = true;
                        return result;
                    }
                }

                result.IsValid = true;
                result.Message = $"Photo validated. Face coverage: {result.FaceCoveragePercent:F0}%";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = $"Error validating photo: {ex.Message}";
                return result;
            }
        }

        /// <summary>
        /// Estimate face coverage using skin tone detection
        /// Returns percentage of center region containing skin tones
        /// </summary>
        private static float EstimateFaceCoverage(Bitmap img)
        {
            try
            {
                // Analyze center region (where face should be)
                int startX = img.Width / 4;
                int startY = img.Height / 8;
                int regionWidth = img.Width / 2;
                int regionHeight = img.Height * 3 / 4;

                int skinPixels = 0;
                int totalPixels = 0;

                for (int y = startY; y < startY + regionHeight && y < img.Height; y += 3)
                {
                    for (int x = startX; x < startX + regionWidth && x < img.Width; x += 3)
                    {
                        Color pixel = img.GetPixel(x, y);
                        if (IsSkinTone(pixel))
                        {
                            skinPixels++;
                        }
                        totalPixels++;
                    }
                }

                if (totalPixels == 0) return 0;
                return (float)skinPixels / totalPixels * 100;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Check if color is in skin tone range
        /// Using HSV color space for better detection
        /// </summary>
        private static bool IsSkinTone(Color c)
        {
            // Convert to HSV
            float r = c.R / 255f;
            float g = c.G / 255f;
            float b = c.B / 255f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));
            float delta = max - min;

            float h = 0;
            if (delta != 0)
            {
                if (max == r) h = 60 * (((g - b) / delta) % 6);
                else if (max == g) h = 60 * (((b - r) / delta) + 2);
                else h = 60 * (((r - g) / delta) + 4);
            }
            if (h < 0) h += 360;

            float s = max == 0 ? 0 : delta / max;
            float v = max;

            // Skin tone ranges in HSV
            // Hue: 0-50 degrees (red to orange)
            // Saturation: 0.2-0.6
            // Value: 0.35-0.95
            bool isHueSkin = (h >= 0 && h <= 50) || (h >= 340 && h <= 360);
            bool isSatSkin = s >= 0.1f && s <= 0.7f;
            bool isValSkin = v >= 0.3f && v <= 0.95f;

            // Additional RGB check for various skin tones
            bool isRgbSkin = c.R > 80 && c.G > 50 && c.B > 30 &&
                            c.R > c.G && c.G > c.B &&
                            Math.Abs(c.R - c.G) > 10;

            return (isHueSkin && isSatSkin && isValSkin) || isRgbSkin;
        }

        /// <summary>
        /// Auto-crop image to focus on face region
        /// </summary>
        public static Bitmap AutoCropToFace(Bitmap original, int targetWidth, int targetHeight)
        {
            try
            {
                // Find face region
                Rectangle faceRegion = FindFaceRegion(original);

                if (faceRegion.IsEmpty)
                {
                    // No face detected, center crop
                    return CenterCrop(original, targetWidth, targetHeight);
                }

                // Expand region to include full head and shoulders
                int expandX = faceRegion.Width / 3;
                int expandY = faceRegion.Height / 2;

                int cropX = Math.Max(0, faceRegion.X - expandX);
                int cropY = Math.Max(0, faceRegion.Y - expandY / 2);
                int cropWidth = Math.Min(original.Width - cropX, faceRegion.Width + expandX * 2);
                int cropHeight = Math.Min(original.Height - cropY, faceRegion.Height + expandY * 2);

                // Maintain aspect ratio
                float targetRatio = (float)targetWidth / targetHeight;
                float currentRatio = (float)cropWidth / cropHeight;

                if (currentRatio > targetRatio)
                {
                    // Too wide, increase height
                    int newHeight = (int)(cropWidth / targetRatio);
                    int diff = newHeight - cropHeight;
                    cropY = Math.Max(0, cropY - diff / 2);
                    cropHeight = Math.Min(original.Height - cropY, newHeight);
                }
                else
                {
                    // Too tall, increase width
                    int newWidth = (int)(cropHeight * targetRatio);
                    int diff = newWidth - cropWidth;
                    cropX = Math.Max(0, cropX - diff / 2);
                    cropWidth = Math.Min(original.Width - cropX, newWidth);
                }

                // Create cropped and resized image
                var cropped = new Bitmap(targetWidth, targetHeight);
                using (var g = Graphics.FromImage(cropped))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(original, 
                        new Rectangle(0, 0, targetWidth, targetHeight),
                        new Rectangle(cropX, cropY, cropWidth, cropHeight),
                        GraphicsUnit.Pixel);
                }

                return cropped;
            }
            catch
            {
                return CenterCrop(original, targetWidth, targetHeight);
            }
        }

        private static Rectangle FindFaceRegion(Bitmap img)
        {
            // Simple face detection based on skin tone clustering
            int sampleStep = 5;
            int minX = img.Width, minY = img.Height;
            int maxX = 0, maxY = 0;
            int skinCount = 0;

            for (int y = 0; y < img.Height; y += sampleStep)
            {
                for (int x = 0; x < img.Width; x += sampleStep)
                {
                    if (IsSkinTone(img.GetPixel(x, y)))
                    {
                        skinCount++;
                        minX = Math.Min(minX, x);
                        minY = Math.Min(minY, y);
                        maxX = Math.Max(maxX, x);
                        maxY = Math.Max(maxY, y);
                    }
                }
            }

            if (skinCount < 100) return Rectangle.Empty;

            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        private static Bitmap CenterCrop(Bitmap original, int targetWidth, int targetHeight)
        {
            float targetRatio = (float)targetWidth / targetHeight;
            float sourceRatio = (float)original.Width / original.Height;

            int cropWidth, cropHeight, cropX, cropY;

            if (sourceRatio > targetRatio)
            {
                cropHeight = original.Height;
                cropWidth = (int)(cropHeight * targetRatio);
                cropX = (original.Width - cropWidth) / 2;
                cropY = 0;
            }
            else
            {
                cropWidth = original.Width;
                cropHeight = (int)(cropWidth / targetRatio);
                cropX = 0;
                cropY = (original.Height - cropHeight) / 2;
            }

            var result = new Bitmap(targetWidth, targetHeight);
            using (var g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(original,
                    new Rectangle(0, 0, targetWidth, targetHeight),
                    new Rectangle(cropX, cropY, cropWidth, cropHeight),
                    GraphicsUnit.Pixel);
            }

            return result;
        }
    }
}
