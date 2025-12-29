using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace RailwayIDCardMaker.Utils
{
    /// <summary>
    /// Manages official Indian Railways logo loading
    /// </summary>
    public static class LogoManager
    {
        private static Image _cachedLogo = null;
        private static readonly object _lock = new object();

        // Default logo file locations
        private static readonly string[] LogoSearchPaths = new string[]
        {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "railway_logo.png"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "indian_railways_logo.png"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ashoka_emblem.png"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "railway_logo.png"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                        "RailwayIDCard", "railway_logo.png")
        };

        /// <summary>
        /// Get the official Railway logo
        /// Returns cached logo or loads from file/resources
        /// </summary>
        public static Image GetLogo()
        {
            lock (_lock)
            {
                if (_cachedLogo != null)
                    return _cachedLogo;

                // Try to load from file paths
                foreach (string path in LogoSearchPaths)
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            _cachedLogo = Image.FromFile(path);
                            return _cachedLogo;
                        }
                        catch { }
                    }
                }

                // Try to load from embedded resource
                try
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var resourceName = "RailwayIDCardMaker.Resources.railway_logo.png";
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        if (stream != null)
                        {
                            _cachedLogo = Image.FromStream(stream);
                            return _cachedLogo;
                        }
                    }
                }
                catch { }

                // Generate default logo if not found
                _cachedLogo = GenerateDefaultLogo(100);
                return _cachedLogo;
            }
        }

        /// <summary>
        /// Load logo from a specific file path
        /// </summary>
        public static bool LoadLogoFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                lock (_lock)
                {
                    _cachedLogo?.Dispose();
                    _cachedLogo = Image.FromFile(filePath);
                }

                // Copy to resources folder for future use
                string resourceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                if (!Directory.Exists(resourceDir))
                    Directory.CreateDirectory(resourceDir);

                string destPath = Path.Combine(resourceDir, "railway_logo.png");
                File.Copy(filePath, destPath, true);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Clear cached logo
        /// </summary>
        public static void ClearCache()
        {
            lock (_lock)
            {
                _cachedLogo?.Dispose();
                _cachedLogo = null;
            }
        }

        /// <summary>
        /// Check if official logo is loaded
        /// </summary>
        public static bool HasOfficialLogo()
        {
            foreach (string path in LogoSearchPaths)
            {
                if (File.Exists(path))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Generate default Ashoka Chakra style logo
        /// </summary>
        public static Image GenerateDefaultLogo(int size)
        {
            var bmp = new Bitmap(size, size);
            bmp.SetResolution(300, 300);

            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(Color.FromArgb(255, 255, 0)); // Yellow background

                var red = Color.FromArgb(200, 0, 0);
                int cx = size / 2, cy = size / 2;

                // Outer circle
                using (var pen = new Pen(red, size * 0.04f))
                {
                    g.DrawEllipse(pen, 2, 2, size - 4, size - 4);
                }

                // Inner circle
                using (var pen = new Pen(red, size * 0.03f))
                {
                    int offset = size / 10;
                    g.DrawEllipse(pen, offset, offset, size - offset * 2, size - offset * 2);
                }

                // 24 spokes (Ashoka Chakra)
                using (var pen = new Pen(red, size * 0.015f))
                {
                    int innerR = size / 5;
                    int outerR = size / 2 - size / 10;

                    for (int i = 0; i < 24; i++)
                    {
                        double angle = i * Math.PI / 12;
                        int x1 = cx + (int)(innerR * Math.Cos(angle));
                        int y1 = cy + (int)(innerR * Math.Sin(angle));
                        int x2 = cx + (int)(outerR * Math.Cos(angle));
                        int y2 = cy + (int)(outerR * Math.Sin(angle));
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }

                // Center hub
                using (var brush = new SolidBrush(red))
                {
                    int hubSize = size / 4;
                    g.FillEllipse(brush, cx - hubSize / 2, cy - hubSize / 2, hubSize, hubSize);
                }

                // Inner hub circle
                using (var brush = new SolidBrush(Color.FromArgb(255, 255, 0)))
                {
                    int innerHub = size / 8;
                    g.FillEllipse(brush, cx - innerHub / 2, cy - innerHub / 2, innerHub, innerHub);
                }
            }

            return bmp;
        }

        /// <summary>
        /// Get logo scaled to specific size
        /// </summary>
        public static Image GetScaledLogo(int width, int height)
        {
            var original = GetLogo();
            if (original.Width == width && original.Height == height)
                return original;

            var scaled = new Bitmap(width, height);
            using (var g = Graphics.FromImage(scaled))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(original, 0, 0, width, height);
            }

            return scaled;
        }
    }
}
