# Railway Employee ID Card Maker

A Windows desktop application for creating and managing Indian Railway employee ID cards.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-blue)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)
![License](https://img.shields.io/badge/License-MIT-green)

## Features

- **Employee Management** - Add, edit, delete employee records
- **ID Card Generation** - Front and back card with Indian Railway design
- **Photo & Signature** - Upload or capture via webcam
- **QR Code** - Auto-generated QR code on back of card
- **Print Support** - Direct printing and print preview
- **Excel Import/Export** - Bulk import employees from Excel/CSV
- **Database Backup** - Backup and restore SQLite database
- **User Authentication** - Login system with admin/user roles

## Requirements

- Windows 7 SP1 / Windows 10/11 (32-bit or 64-bit)
- .NET Framework 4.5
- Visual Studio 2017/2019/2022 (for development)

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/01fe23bcs183/id-card-railway.git
   ```

2. Open `RailwayIDCardMaker/RailwayIDCardMaker.sln` in Visual Studio

3. Restore NuGet packages (Right-click solution → Restore NuGet Packages)

4. Build and run (F5)

## Default Login

- **Username:** `admin`
- **Password:** `admin123`

## Project Structure

```
RailwayIDCardMaker/
├── Forms/              # Windows Forms UI
│   ├── MainForm.cs     # Main application window
│   ├── EmployeeForm.cs # Employee data entry
│   ├── LoginForm.cs    # Authentication
│   └── ...
├── Models/             # Data models
│   ├── Employee.cs
│   ├── User.cs
│   └── Zone.cs
├── Services/           # Business logic
│   ├── CardRenderer.cs # ID card rendering
│   ├── DatabaseService.cs
│   ├── PrintService.cs
│   └── QRCodeGenerator.cs
└── Utils/              # Utilities
    ├── Constants.cs
    └── Helpers.cs
```

## NuGet Packages

- `ZXing.Net` - QR code generation
- `EPPlus` - Excel import/export
- `AForge.Video.DirectShow` - Webcam capture

## Screenshots

The application generates ID cards matching the official Indian Railway employee ID card design with:

- Yellow background
- Indian Railways logo
- Employee photo and signature
- QR code with employee details
- Blood group display

## License

MIT License - Feel free to use and modify.

## Author

Developed for Indian Railway employee ID card management.
