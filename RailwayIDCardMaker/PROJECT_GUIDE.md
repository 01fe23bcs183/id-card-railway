# Railway Employee ID Card Maker - Project Guide

## 📋 Overview

A Windows Forms application for generating and managing Indian Railways employee ID cards. Built for **Windows 7 x86** compatibility using **.NET Framework 4.0**.

---

## 🎯 Core Features

### 1. Employee Management
- Add/Edit/Delete employee records
- Store personal details, employment info, photos, signatures
- Search and filter employees

### 2. ID Card Generation
- **Front Side:** Photo, Name, Designation, ID Number, Blood Group, QR Code
- **Back Side:** Address, Emergency contact, Validity, Authority signature
- Live preview while editing

### 3. ID Card Printing
- Print preview
- Direct printing to card printer
- Support for standard ID card size (85.6mm × 53.98mm)

### 4. Data Management
- Import from Excel (.xls/.xlsx)
- Export to CSV
- Database backup/restore

### 5. Security
- User authentication
- Password management
- Role-based access (Admin/Operator)

---

## 🆔 ID Card Number Format

```
YY - ZZ - UU - SSSSSS
│    │    │    └── Serial Number (6 digits)
│    │    └── Unit/Division Code (2 digits)
│    └── Zone Code (2 digits)
└── Year (2 digits)

Example: 24-01-05-000123
         └── Year 2024, Zone CR (01), Unit 05, Serial 123
```

---

## 🏗️ Architecture

### Layers

```
┌─────────────────────────────────────┐
│           Forms (UI Layer)          │
│  LoginForm, MainForm, EmployeeForm  │
├─────────────────────────────────────┤
│         Services (Business)         │
│  DatabaseService, CardRenderer,     │
│  PrintService, ImageService, etc.   │
├─────────────────────────────────────┤
│         Models (Data Layer)         │
│  Employee, Zone, User, CardSettings │
├─────────────────────────────────────┤
│         Utils (Utilities)           │
│  Constants, Helpers                 │
├─────────────────────────────────────┤
│       SQLite (Persistence)          │
└─────────────────────────────────────┘
```

### Key Files

| File | Purpose |
|------|---------|
| `Program.cs` | Application entry point |
| `DatabaseService.cs` | All database operations |
| `CardRenderer.cs` | Generates ID card images |
| `Constants.cs` | Card dimensions, colors, fonts |
| `Employee.cs` | Main data model |

---

## 🎨 ID Card Specifications

### Physical Dimensions
- **Width:** 85.6mm (1011 pixels @ 300 DPI)
- **Height:** 53.98mm (638 pixels @ 300 DPI)
- **Corner Radius:** 3mm
- **DPI:** 300 (print quality)

### Colors
- **Railway Blue:** RGB(0, 51, 102) - Header background
- **Railway Green:** RGB(0, 102, 51) - Accents
- **Railway Gold:** RGB(255, 204, 0) - Highlights
- **Background:** White

### Typography
- **Primary Font:** Times New Roman
- **Name:** 14pt Bold
- **Labels:** 9pt Regular
- **ID Number:** 12pt Bold

---

## 🗄️ Database Schema

### Tables

**Employees**
- Personal info (Name, DOB, Gender, Address, Mobile, Aadhaar)
- Employment info (Designation, Department, Zone, Unit, PF Number)
- Card info (ID Number, Photo, Signature, Issue/Validity dates)
- Metadata (Created/Modified dates, Print count)

**Users**
- Username, PasswordHash, FullName, Role, IsActive

**Zones**
- Code, Name, Abbreviation, Headquarters

**Settings**
- Default authority, zone, validity years, printer settings

---

## 🔐 Default Credentials

```
Username: admin
Password: admin123
```

---

## 📚 Indian Railway Zones

| Code | Abbreviation | Zone Name |
|------|--------------|-----------|
| 01 | CR | Central Railway |
| 02 | ER | Eastern Railway |
| 03 | ECR | East Central Railway |
| 04 | ECoR | East Coast Railway |
| 05 | NR | Northern Railway |
| 06 | NCR | North Central Railway |
| 07 | NER | North Eastern Railway |
| 08 | NFR | Northeast Frontier Railway |
| 09 | NWR | North Western Railway |
| 10 | SR | Southern Railway |
| 11 | SCR | South Central Railway |
| 12 | SER | South Eastern Railway |
| 13 | SECR | South East Central Railway |
| 14 | SWR | South Western Railway |
| 15 | WR | Western Railway |
| 16 | WCR | West Central Railway |
| 17 | KR | Konkan Railway |
| 18 | MR | Metro Railway Kolkata |

---

## 🔧 Development Notes

### Dependencies
- .NET Framework 4.0 (for Win7 x86 support)
- System.Data.SQLite.Core 1.0.118.0
- AForge.Video.DirectShow 2.2.5 (webcam)

### Key Design Decisions
1. **No external QR library** - Custom implementation for compatibility
2. **SQLite** - Portable, no server setup required
3. **GDI+** - Native Windows drawing for card rendering
4. **x86 target** - Maximum Windows 7 compatibility

### File Storage
- Database: `%AppData%\RailwayIDCardMaker\railway_idcard.db`
- Photos: `%AppData%\RailwayIDCardMaker\Photos\`
- Signatures: `%AppData%\RailwayIDCardMaker\Signatures\`

---

## 🚀 Quick Start for Developers

1. Clone/Open the project in Visual Studio
2. Restore NuGet packages
3. Build solution
4. Run - Database auto-creates on first launch
5. Login with `admin` / `admin123`
6. Add employees and generate ID cards!

---

## 📞 Support

This application is designed for Indian Railways employee ID card generation. For customizations or issues, refer to the source code documentation.
