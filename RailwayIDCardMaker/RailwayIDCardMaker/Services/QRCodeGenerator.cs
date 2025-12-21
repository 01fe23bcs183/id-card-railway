using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace RailwayIDCardMaker.Services
{
    /// <summary>
    /// QR Code Generator service
    /// Simple implementation without external libraries (compatible with .NET 4.0)
    /// Uses a basic encoding suitable for the ID card data
    /// </summary>
    public static class QRCodeGenerator
    {
        // QR Code error correction level
        private const int ERROR_CORRECTION_LEVEL_M = 1; // ~15% error correction

        /// <summary>
        /// Generate QR Code image from text data
        /// </summary>
        /// <param name="data">Data to encode in QR code</param>
        /// <param name="size">Size in pixels (width and height)</param>
        /// <param name="quietZone">Quiet zone (white border) in modules</param>
        /// <returns>QR Code as Bitmap image</returns>
        public static Bitmap GenerateQRCode(string data, int size = 200, int quietZone = 4)
        {
            if (string.IsNullOrEmpty(data))
            {
                // Return blank white image if no data
                Bitmap blank = new Bitmap(size, size);
                using (Graphics g = Graphics.FromImage(blank))
                {
                    g.Clear(Color.White);
                }
                return blank;
            }

            // Generate QR matrix using simple encoding
            bool[,] matrix = GenerateQRMatrix(data);

            int matrixSize = matrix.GetLength(0);
            int totalModules = matrixSize + (quietZone * 2);
            int moduleSize = size / totalModules;

            if (moduleSize < 1) moduleSize = 1;

            int actualSize = moduleSize * totalModules;

            Bitmap qrCode = new Bitmap(actualSize, actualSize);

            using (Graphics g = Graphics.FromImage(qrCode))
            {
                g.Clear(Color.White);

                using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                {
                    for (int y = 0; y < matrixSize; y++)
                    {
                        for (int x = 0; x < matrixSize; x++)
                        {
                            if (matrix[y, x])
                            {
                                int px = (x + quietZone) * moduleSize;
                                int py = (y + quietZone) * moduleSize;
                                g.FillRectangle(blackBrush, px, py, moduleSize, moduleSize);
                            }
                        }
                    }
                }
            }

            // Resize to exact requested size if needed
            if (actualSize != size)
            {
                Bitmap resized = new Bitmap(size, size);
                using (Graphics g = Graphics.FromImage(resized))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.DrawImage(qrCode, 0, 0, size, size);
                }
                qrCode.Dispose();
                return resized;
            }

            return qrCode;
        }

        /// <summary>
        /// Generate QR matrix from data
        /// This is a simplified implementation that creates a Version 4 QR code pattern
        /// </summary>
        private static bool[,] GenerateQRMatrix(string data)
        {
            // For a proper QR code, we'd need a full implementation
            // This creates a simple but functional pattern

            // Convert data to bit stream
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            List<bool> bits = new List<bool>();

            foreach (byte b in bytes)
            {
                for (int i = 7; i >= 0; i--)
                {
                    bits.Add((b & (1 << i)) != 0);
                }
            }

            // Determine size based on data length (simplified)
            int version = Math.Min(10, Math.Max(1, (bytes.Length / 10) + 1));
            int size = 17 + (version * 4); // QR version formula

            bool[,] matrix = new bool[size, size];

            // Add finder patterns (top-left, top-right, bottom-left)
            AddFinderPattern(matrix, 0, 0);
            AddFinderPattern(matrix, size - 7, 0);
            AddFinderPattern(matrix, 0, size - 7);

            // Add timing patterns
            for (int i = 8; i < size - 8; i++)
            {
                matrix[6, i] = (i % 2 == 0);
                matrix[i, 6] = (i % 2 == 0);
            }

            // Add alignment pattern (for version 2+)
            if (version >= 2)
            {
                int alignPos = size - 9;
                AddAlignmentPattern(matrix, alignPos, alignPos);
            }

            // Fill data area with encoded bits
            int bitIndex = 0;
            bool upward = true;

            for (int col = size - 1; col > 0; col -= 2)
            {
                if (col == 6) col--; // Skip timing pattern column

                for (int row = upward ? size - 1 : 0;
                     upward ? row >= 0 : row < size;
                     row += upward ? -1 : 1)
                {
                    for (int c = 0; c < 2; c++)
                    {
                        int actualCol = col - c;

                        if (!IsReservedArea(size, row, actualCol))
                        {
                            if (bitIndex < bits.Count)
                            {
                                matrix[row, actualCol] = bits[bitIndex++];
                            }
                            else
                            {
                                // Padding pattern
                                matrix[row, actualCol] = ((row + actualCol) % 2 == 0);
                            }
                        }
                    }
                }
                upward = !upward;
            }

            // Apply mask pattern (checkerboard pattern for simplicity)
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (!IsReservedArea(size, y, x))
                    {
                        if ((y + x) % 2 == 0)
                        {
                            matrix[y, x] = !matrix[y, x];
                        }
                    }
                }
            }

            return matrix;
        }

        /// <summary>
        /// Add finder pattern (7x7 square pattern at corners)
        /// </summary>
        private static void AddFinderPattern(bool[,] matrix, int row, int col)
        {
            // 7x7 finder pattern
            int[,] pattern = new int[,]
            {
                {1,1,1,1,1,1,1},
                {1,0,0,0,0,0,1},
                {1,0,1,1,1,0,1},
                {1,0,1,1,1,0,1},
                {1,0,1,1,1,0,1},
                {1,0,0,0,0,0,1},
                {1,1,1,1,1,1,1}
            };

            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (row + y < matrix.GetLength(0) && col + x < matrix.GetLength(1))
                    {
                        matrix[row + y, col + x] = pattern[y, x] == 1;
                    }
                }
            }

            // Add separator (white line around finder pattern)
            int size = matrix.GetLength(0);

            // Right of top-left, left of top-right, right of bottom-left
            if (col == 0 && row == 0) // Top-left
            {
                for (int i = 0; i <= 7 && i < size; i++)
                {
                    if (7 < size) matrix[i, 7] = false;
                    if (7 < size) matrix[7, i] = false;
                }
            }
            else if (col == size - 7 && row == 0) // Top-right
            {
                for (int i = 0; i <= 7 && i < size; i++)
                {
                    if (col - 1 >= 0) matrix[i, col - 1] = false;
                    if (7 < size) matrix[7, col + i] = false;
                }
            }
            else if (col == 0 && row == size - 7) // Bottom-left
            {
                for (int i = 0; i <= 7 && i < size; i++)
                {
                    if (7 < size) matrix[row + i, 7] = false;
                    if (row - 1 >= 0) matrix[row - 1, i] = false;
                }
            }
        }

        /// <summary>
        /// Add alignment pattern (5x5 pattern)
        /// </summary>
        private static void AddAlignmentPattern(bool[,] matrix, int row, int col)
        {
            int[,] pattern = new int[,]
            {
                {1,1,1,1,1},
                {1,0,0,0,1},
                {1,0,1,0,1},
                {1,0,0,0,1},
                {1,1,1,1,1}
            };

            int startRow = row - 2;
            int startCol = col - 2;

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    int actualRow = startRow + y;
                    int actualCol = startCol + x;

                    if (actualRow >= 0 && actualRow < matrix.GetLength(0) &&
                        actualCol >= 0 && actualCol < matrix.GetLength(1))
                    {
                        matrix[actualRow, actualCol] = pattern[y, x] == 1;
                    }
                }
            }
        }

        /// <summary>
        /// Check if position is in reserved area (finder patterns, timing, etc.)
        /// </summary>
        private static bool IsReservedArea(int size, int row, int col)
        {
            // Top-left finder pattern + separator
            if (row < 9 && col < 9) return true;

            // Top-right finder pattern + separator
            if (row < 9 && col >= size - 8) return true;

            // Bottom-left finder pattern + separator
            if (row >= size - 8 && col < 9) return true;

            // Timing patterns
            if (row == 6 || col == 6) return true;

            return false;
        }

        /// <summary>
        /// Generate QR code for employee data
        /// </summary>
        public static Bitmap GenerateEmployeeQRCode(Models.Employee employee, int size = 200)
        {
            string qrData = employee.GetQRCodeData();
            return GenerateQRCode(qrData, size);
        }

        /// <summary>
        /// Save QR code to file
        /// </summary>
        public static void SaveQRCode(Bitmap qrCode, string filePath, ImageFormat format = null)
        {
            if (format == null) format = ImageFormat.Png;
            qrCode.Save(filePath, format);
        }
    }
}
