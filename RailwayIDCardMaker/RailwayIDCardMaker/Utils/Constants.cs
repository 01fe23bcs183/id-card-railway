using System;
using System.Drawing;

namespace RailwayIDCardMaker.Utils
{
    /// <summary>
    /// Application constants based on Indian Railways ID Card specifications
    /// </summary>
    public static class Constants
    {
        // ============================================
        // CARD PHYSICAL DIMENSIONS (in mm)
        // ============================================
        public const float CARD_WIDTH_MM = 54f;      // 54mm width
        public const float CARD_HEIGHT_MM = 87f;     // 87mm height (Portrait orientation)

        // Convert mm to pixels at 300 DPI for printing
        public const float MM_TO_PIXELS_300DPI = 11.811f;  // 300 / 25.4

        public static int CardWidthPixels => (int)(CARD_WIDTH_MM * MM_TO_PIXELS_300DPI);   // ~638 pixels
        public static int CardHeightPixels => (int)(CARD_HEIGHT_MM * MM_TO_PIXELS_300DPI); // ~1028 pixels

        // Screen display scale (for preview)
        public const float SCREEN_SCALE = 0.6f;

        // ============================================
        // PHOTO DIMENSIONS (in mm converted to pixels)
        // ============================================
        public const float PHOTO_WIDTH_MM = 42f;     // 4.2cm = 42mm
        public const float PHOTO_HEIGHT_MM = 48.5f;  // 4.85cm = 48.5mm

        public static int PhotoWidthPixels => (int)(PHOTO_WIDTH_MM * MM_TO_PIXELS_300DPI);
        public static int PhotoHeightPixels => (int)(PHOTO_HEIGHT_MM * MM_TO_PIXELS_300DPI);

        // ============================================
        // SIGNATURE BOX DIMENSIONS
        // ============================================
        public const float SIGNATURE_WIDTH_MM = 42f;   // 4.2cm
        public const float SIGNATURE_HEIGHT_MM = 7.5f; // 0.75cm

        // ============================================
        // QR CODE DIMENSIONS (Back side)
        // ============================================
        public const float QRCODE_WIDTH_MM = 25f;    // 2.5cm
        public const float QRCODE_HEIGHT_MM = 23.5f; // 2.35cm

        public static int QRCodeWidthPixels => (int)(QRCODE_WIDTH_MM * MM_TO_PIXELS_300DPI);
        public static int QRCodeHeightPixels => (int)(QRCODE_HEIGHT_MM * MM_TO_PIXELS_300DPI);

        // ============================================
        // COLORS (Based on actual Indian Railway ID Card)
        // ============================================
        public static readonly Color CARD_BACKGROUND_COLOR = Color.FromArgb(255, 255, 255, 0); // Bright Lime Yellow
        public static readonly Color INDIAN_RAILWAYS_TEXT_COLOR = Color.FromArgb(255, 220, 0, 0); // Red (matching logo)
        public static readonly Color EMPLOYEE_CARD_TEXT_COLOR = Color.FromArgb(255, 220, 0, 0); // Red
        public static readonly Color BLOOD_GROUP_COLOR = Color.FromArgb(255, 220, 0, 0); // Red
        public static readonly Color NAME_TEXT_COLOR = Color.Black;
        public static readonly Color WHITE_BOX_COLOR = Color.White;
        public static readonly Color LOGO_RED_COLOR = Color.FromArgb(255, 180, 30, 30); // Indian Railways Logo Red

        // ============================================
        // FONTS (Times New Roman as specified)
        // ============================================
        public const string PRIMARY_FONT_FAMILY = "Times New Roman";

        public const float FONT_SIZE_HEADER_LARGE = 14f;    // "Indian Railways"
        public const float FONT_SIZE_HEADER_MEDIUM = 10f;   // "Employee Identity Card"
        public const float FONT_SIZE_NAME = 12f;            // Employee name
        public const float FONT_SIZE_DESIGNATION = 10f;     // Designation
        public const float FONT_SIZE_DETAILS = 8f;          // Other details
        public const float FONT_SIZE_SMALL = 6f;            // Side text, footer
        public const float FONT_SIZE_BLOOD_GROUP = 16f;     // Blood group (large)
        public const float FONT_SIZE_ID_NUMBER = 9f;        // ID Card number

        // ============================================
        // LANYARD DIMENSIONS
        // ============================================
        public const float LANYARD_WIDTH_CM = 2f;    // 2cm
        public const float LANYARD_LENGTH_CM = 96f;  // 96cm

        // ============================================
        // DATABASE
        // ============================================
        public const string DATABASE_FILENAME = "RailwayIDCards.db";
        public const string PHOTOS_FOLDER = "Photos";
        public const string SIGNATURES_FOLDER = "Signatures";
        public const string TEMPLATES_FOLDER = "Templates";
        public const string EXPORTS_FOLDER = "Exports";
        public const string BACKUPS_FOLDER = "Backups";

        // ============================================
        // DEFAULT USER CREDENTIALS
        // ============================================
        public const string DEFAULT_USERNAME = "admin";
        public const string DEFAULT_PASSWORD = "admin123";

        // ============================================
        // ID NUMBER FORMAT
        // YY + ZZ + UU + SSSSSS
        // ============================================
        public const int ID_YEAR_LENGTH = 2;
        public const int ID_ZONE_LENGTH = 2;
        public const int ID_UNIT_LENGTH = 2;
        public const int ID_SERIAL_LENGTH = 6;

        // ============================================
        // BLOOD GROUPS
        // ============================================
        public static readonly string[] BLOOD_GROUPS = new string[]
        {
            "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"
        };

        // ============================================
        // DEPARTMENTS
        // ============================================
        public static readonly string[] DEPARTMENTS = new string[]
        {
            "ELECTRICAL",
            "MECHANICAL",
            "COMMERCIAL",
            "ENGINEERING",
            "SIGNAL & TELECOM",
            "ACCOUNTS",
            "PERSONNEL",
            "STORES",
            "MEDICAL",
            "SECURITY",
            "TRAFFIC",
            "OPERATING",
            "GENERAL ADMIN",
            "CIVIL",
            "TRACTION DISTRIBUTION",
            "WORKSHOP"
        };

        // ============================================
        // CARD FOOTER TEXT
        // ============================================
        public const string FOOTER_TEXT = "Property of Indian Railways. If found, return to nearest railway office.";

        // ============================================
        // APPLICATION INFO
        // ============================================
        public const string APP_NAME = "Railway Employee ID Card Maker";
        public const string APP_VERSION = "1.0.0";
        public const string APP_AUTHOR = "Indian Railways";
    }
}
