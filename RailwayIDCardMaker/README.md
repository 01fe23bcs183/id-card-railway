# 🚂 Railway Employee ID Card Maker

<div align="center">

![Railway ID Card Maker](Resources/app.ico)

**Professional ID Card Management System for Indian Railways**

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.0-blue.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows%20x86-lightgrey.svg)](https://www.microsoft.com/windows)
[![License](https://img.shields.io/badge/License-Proprietary-red.svg)]()

</div>

---

## 📋 Overview

A Windows desktop application designed for generating and managing employee ID cards for Indian Railways. Built with **WinForms** targeting **.NET Framework 4.0** for maximum **Windows 7 compatibility**.

### ✨ Key Features

- 🆔 **ID Card Generation** - Professional front/back ID cards with QR codes
- 👥 **Employee Management** - Complete CRUD operations for employee records
- 📸 **Photo Capture** - Webcam integration or file upload
- 🖨️ **Printing** - Direct card printing with preview
- 📊 **Data Import/Export** - Excel import, CSV export
- 💾 **Backup/Restore** - Database backup and recovery
- 🔐 **User Authentication** - Role-based access control
- 🏢 **Multi-Zone Support** - All 18 Indian Railway zones

---

## 🎯 Quick Start

### Installation

1. **Prerequisites**: Windows 7+ with .NET Framework 4.0
2. **Download**: Get the latest release
3. **Extract**: Unzip to `C:\Program Files (x86)\RailwayIDCardMaker\`
4. **Run**: Double-click `RailwayIDCardMaker.exe`

### First Login

```
Username: admin
Password: admin123
```

⚠️ **Change the default password after first login!**

---

## 🆔 ID Card Format

```
YY - ZZ - UU - SSSSSS
│    │    │    └── Serial Number (6 digits)
│    │    └── Unit/Division Code (2 digits)
│    └── Zone Code (2 digits, e.g., 01=CR, 05=NR)
└── Year (2 digits)

Example: 24-01-05-000123
         Year 2024, Central Railway (CR), Unit 05, Serial 123
```

---

## 🎨 Features in Detail

### 1. Employee Management

- **Add/Edit/Delete** employee records
- **Search & Filter** with advanced criteria
- **Bulk Import** from Excel spreadsheets
- **Data Export** to CSV format

### 2. ID Card Design

**Front Side:**

- Employee photo (passport size)
- Full name and designation
- Department and ID number
- Blood group
- QR code (encoded employee data)
- Railway logo and branding

**Back Side:**

- Complete address
- Emergency contact details
- Date of issue and validity
- Authorized signatory
- Terms and conditions

### 3. Printing

- **Print Preview** before printing
- **Direct Print** to card printer
- **Standard Size**: 85.6mm × 53.98mm (CR80)
- **High Quality**: 300 DPI output

### 4. Data Management

- **SQLite Database** - Lightweight, portable
- **Auto-backup** capability
- **Import from Excel** (.xls, .xlsx)
- **Export to CSV** for reporting
- **Photo Management** - Organized storage

### 5. Security

- **Password Protection** with hashing
- **Role-based Access** (Admin/Operator)
- **Change Password** functionality
- **Session Management**

---

## 🏗️ System Requirements

### Minimum Requirements

- **OS**: Windows 7 (x86 or x64)
- **Framework**: .NET Framework 4.0
- **RAM**: 512 MB
- **Disk Space**: 50 MB
- **Display**: 1024×768

### Recommended Requirements

- **OS**: Windows 10/11
- **Framework**: .NET Framework 4.8
- **RAM**: 2 GB
- **Disk Space**: 100 MB
- **Display**: 1920×1080
- **Webcam**: For photo capture
- **Printer**: Card/PVC printer for ID cards

---

## 📦 Technology Stack

| Component | Technology |
|-----------|-----------|
| **Framework** | .NET Framework 4.0 |
| **UI** | Windows Forms (WinForms) |
| **Database** | SQLite 3 (System.Data.SQLite) |
| **Imaging** | GDI+ (System.Drawing) |
| **Webcam** | AForge.NET Video |
| **QR Code** | Custom implementation |
| **Target Platform** | Windows x86 |

---

## 🗄️ Database Schema

### Main Tables

**Employees**

- Personal information (Name, DOB, Gender, Address, Mobile, Aadhaar)
- Employment details (Designation, Department, Zone, Unit, PF Number)
- Card details (ID Number, Photo, Signature, Issue/Validity dates)
- Metadata (Created/Modified times, Print count)

**Users**

- Username, PasswordHash, FullName, Role, IsActive

**Zones**

- Code, Name, Abbreviation, Headquarters (18 Railway zones)

**Settings**

- Application configuration
- Default authority, zone, validity period
- Printer preferences

---

## 🚄 Indian Railway Zones Supported

| Code | Zone | Abbreviation | Headquarters |
|------|------|--------------|--------------|
| 01 | Central Railway | CR | Mumbai |
| 02 | Eastern Railway | ER | Kolkata |
| 03 | East Central Railway | ECR | Hajipur |
| 04 | East Coast Railway | ECoR | Bhubaneswar |
| 05 | Northern Railway | NR | Delhi |
| 06 | North Central Railway | NCR | Prayagraj |
| 07 | North Eastern Railway | NER | Gorakhpur |
| 08 | Northeast Frontier Railway | NFR | Guwahati |
| 09 | North Western Railway | NWR | Jaipur |
| 10 | Southern Railway | SR | Chennai |
| 11 | South Central Railway | SCR | Secunderabad |
| 12 | South Eastern Railway | SER | Kolkata |
| 13 | South East Central Railway | SECR | Bilaspur |
| 14 | South Western Railway | SWR | Hubballi |
| 15 | Western Railway | WR | Mumbai |
| 16 | West Central Railway | WCR | Jabalpur |
| 17 | Konkan Railway | KR | Navi Mumbai |
| 18 | Metro Railway Kolkata | MR | Kolkata |

---

## 📁 Project Structure

```
RailwayIDCardMaker/
├── RailwayIDCardMaker.sln           # Visual Studio solution
├── README.md                        # This file
├── PROJECT_GUIDE.md                 # Development guide
├── PROJECT_STATUS.md                # Completion status
├── BUILD_INSTRUCTIONS.md            # Build instructions
└── RailwayIDCardMaker/              # Main project
    ├── Forms/                       # UI Forms (12 forms)
    │   ├── LoginForm.cs
    │   ├── MainForm.cs
    │   ├── EmployeeForm.cs
    │   ├── DataListForm.cs
    │   ├── CardPreviewForm.cs
    │   ├── SettingsForm.cs
    │   ├── AboutForm.cs
    │   ├── PhotoUploadForm.cs
    │   ├── WebcamCaptureForm.cs
    │   ├── ChangePasswordForm.cs
    │   ├── ExcelImportForm.cs
    │   └── BackupRestoreForm.cs
    ├── Models/                      # Data models
    │   ├── Employee.cs
    │   ├── Zone.cs
    │   ├── Unit.cs
    │   ├── User.cs
    │   └── CardSettings.cs
    ├── Services/                    # Business logic
    │   ├── DatabaseService.cs
    │   ├── CardRenderer.cs
    │   ├── QRCodeGenerator.cs
    │   ├── IDNumberGenerator.cs
    │   ├── ImageService.cs
    │   ├── PrintService.cs
    │   └── ExcelImportService.cs
    ├── Utils/                       # Utilities
    │   ├── Constants.cs
    │   └── Helpers.cs
    ├── Resources/                   # Images, icons
    │   └── app.ico
    ├── Properties/
    │   └── AssemblyInfo.cs
    ├── Program.cs                   # Entry point
    ├── packages.config              # NuGet packages
    └── RailwayIDCardMaker.csproj   # Project file
```

---

## 🛠️ Development

### Building from Source

See **[BUILD_INSTRUCTIONS.md](BUILD_INSTRUCTIONS.md)** for detailed build steps.

**Quick Build:**

1. Open `RailwayIDCardMaker.sln` in Visual Studio 2019+
2. Restore NuGet packages
3. Set platform to **x86**
4. Build Solution (Ctrl+Shift+B)
5. Run (F5)

### NuGet Dependencies

```xml
<packages>
  <package id="System.Data.SQLite.Core" version="1.0.118.0" targetFramework="net40" />
  <package id="AForge" version="2.2.5" targetFramework="net40" />
  <package id="AForge.Video" version="2.2.5" targetFramework="net40" />
  <package id="AForge.Video.DirectShow" version="2.2.5" targetFramework="net40" />
</packages>
```

---

## 📖 Usage Guide

### Creating an ID Card

1. **Login** to the application
2. Click **New Card** (Ctrl+N) or toolbar button
3. Fill employee details:
   - Personal info (Name, DOB, Gender, etc.)
   - Employment info (Designation, Department, Zone, etc.)
   - Upload photo/signature or capture via webcam
4. **Preview** the card (front and back)
5. **Save** the employee record
6. **Print** the ID card

### Importing Bulk Data

1. **Prepare Excel file** with columns:
   - Name, DOB, Gender, Mobile, Aadhaar
   - Designation, Department, Zone, Unit
   - Blood Group, Address, Emergency Contact
2. Go to **Data** → **Import Data**
3. **Browse** and select Excel file
4. **Preview** imported data
5. **Import** to database

### Managing Employees

1. Click **Data List** (Ctrl+L)
2. **Search** by name, ID, zone, etc.
3. **Edit** - Double-click on a record
4. **Delete** - Select and click Delete
5. **Print** - Select and click Print Card
6. **Export** - Export filtered data to CSV

### Application Settings

1. Go to **File** → **Settings**
2. Configure:
   - Default zone and authority
   - ID card validity period (years)
   - Printer preferences
   - Authority signature
3. **Save** settings

### Backup & Restore

1. Go to **Data** → **Backup**
2. **Create Backup**:
   - Choose location
   - Backup is created (includes photos/signatures)
3. **Restore Backup**:
   - Select backup file (.db)
   - Confirm restore (overwrites current data)

---

## 🎨 Customization

### Changing ID Card Design

Edit `Services/CardRenderer.cs` and `Utils/Constants.cs`:

```csharp
// Colors
public static readonly Color RAILWAY_BLUE = Color.FromArgb(0, 51, 102);
public static readonly Color RAILWAY_GREEN = Color.FromArgb(0, 102, 51);

// Dimensions (mm to pixels at 300 DPI)
public const int CARD_WIDTH = 1011;   // 85.6mm
public const int CARD_HEIGHT = 638;   // 53.98mm

// Fonts
public static readonly Font NAME_FONT = new Font("Times New Roman", 14, FontStyle.Bold);
```

### Adding New Zones/Units

Edit `DatabaseService.cs` → `InitializeDatabase()` method:

```csharp
zones.Add(new Zone { Code = "19", Name = "New Zone", Abbreviation = "NZ", Headquarters = "City" });
```

---

## 🐛 Troubleshooting

### Application won't start

- **Ensure .NET Framework 4.0 is installed**
- Check Windows Event Viewer for errors
- Run as Administrator

### Database errors

- Delete `%AppData%\RailwayIDCardMaker\railway_idcard.db`
- Restart application (new DB will be created)
- Restore from backup if needed

### Webcam not working

- Check camera permissions in Windows
- Ensure camera is DirectShow-compatible
- Try uploading photo instead

### Print quality issues

- Check printer DPI settings (should be 300+ DPI)
- Use proper card stock (CR80 size)
- Verify card design in preview first

---

## 📝 License

**Proprietary Software**  
© 2024 Indian Railways. All rights reserved.

This software is developed for internal use by Indian Railways employee ID card generation. Unauthorized distribution, modification, or commercial use is prohibited.

---

## 👥 Credits

**Development Team**

- Core Application Development
- UI/UX Design
- Database Architecture
- QR Code Implementation

**Technologies**

- Microsoft .NET Framework
- SQLite Database Engine
- AForge.NET Framework
- Windows Forms

---

## 📞 Support

For technical support or feature requests:

- **Email**: <support@example.com>
- **Documentation**: See `PROJECT_GUIDE.md`
- **Issues**: Check `PROJECT_STATUS.md` for known issues

---

## 🔄 Version History

### Version 1.0.0 (December 2024)

- ✅ Initial release
- ✅ Full employee management
- ✅ ID card generation (front/back)
- ✅ QR code integration
- ✅ Print functionality
- ✅ Excel import/export
- ✅ Backup/restore capability
- ✅ User authentication
- ✅ 18 Railway zones support
- ✅ Webcam integration

---

<div align="center">

**Made with ❤️ for Indian Railways**

🚂 Connecting India 🇮🇳

</div>
