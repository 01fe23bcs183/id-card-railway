# Railway ID Card Maker - Build Instructions

## 📋 Prerequisites

### Required Software

1. **Visual Studio 2019 or later** (Community Edition is sufficient)
   - Workload: ".NET desktop development"
   - Component: ".NET Framework 4.0 targeting pack"

2. **NuGet Package Manager** (included with Visual Studio)

### Optional Tools

- **Windows 7 x86** or later for testing (target platform)
- **Card printer** for physical ID card printing

---

## 🚀 Quick Start - Building in Visual Studio

### Step 1: Open the Solution

1. Launch **Visual Studio**
2. Open `RailwayIDCardMaker.sln`
3. Wait for Visual Studio to load all projects

### Step 2: Restore NuGet Packages

**Option A: Automatic (Recommended)**

- Visual Studio should automatically restore packages on solution load
- If not, right-click on **Solution** → **Restore NuGet Packages**

**Option B: Manual**

- Open **Tools** → **NuGet Package Manager** → **Package Manager Console**
- Run: `Update-Package -reinstall`

### Step 3: Verify References

Ensure these packages are installed:

- ✅ `System.Data.SQLite.Core` v1.0.118.0
- ✅ `AForge` v2.2.5
- ✅ `AForge.Video` v2.2.5
- ✅ `AForge.Video.DirectShow` v2.2.5

Check in: **Solution Explorer** → **RailwayIDCardMaker** → **References**

### Step 4: Select Build Configuration

- **Platform**: `x86` (required for Windows 7 compatibility)
- **Configuration**: `Debug` or `Release`

Set via: **Build** → **Configuration Manager**

### Step 5: Build the Solution

- Press **Ctrl+Shift+B** or
- **Build** → **Build Solution**

✅ **Expected Output**: `Build succeeded` (0 errors)

### Step 6: Run the Application

- Press **F5** (Debug) or **Ctrl+F5** (Run without debugging)
- Login with default credentials:
  - **Username**: `admin`
  - **Password**: `admin123`

---

## 🔧 Troubleshooting

### Issue: "Could not resolve this reference"

**Solution**:

1. Right-click Solution → **Restore NuGet Packages**
2. Clean Solution: **Build** → **Clean Solution**
3. Rebuild: **Build** → **Rebuild Solution**

### Issue: "Platform mismatch - x86 vs AnyCPU"

**Solution**:

1. **Build** → **Configuration Manager**
2. Set Active solution platform to **x86**
3. Ensure all projects use **x86**

### Issue: "SQLite.Interop.dll not found"

**Solution**:

- This is auto-copied by NuGet during build
- If missing, reinstall: `Update-Package System.Data.SQLite.Core -reinstall`

### Issue: "AForge assemblies missing"

**Solution**:

1. Package Manager Console: `Update-Package AForge.Video.DirectShow -reinstall`
2. Rebuild the solution

---

## 📦 Building via Command Line

### Prerequisites

- Install **Build Tools for Visual Studio 2019** or use Developer Command Prompt

### Using Developer Command Prompt

```batch
# 1. Open "Developer Command Prompt for VS 2019"
# 2. Navigate to project directory
cd "c:\Users\iamje\OneDrive\Desktop\anitgravity\railway\vs\RailwayIDCardMaker"

# 3. Restore NuGet packages (if nuget.exe is in PATH)
nuget restore RailwayIDCardMaker.sln

# 4. Build the solution
msbuild RailwayIDCardMaker.sln /p:Configuration=Release /p:Platform=x86

# 5. Output location
# bin\x86\Release\RailwayIDCardMaker.exe
```

### Alternative: Using MSBuild directly

```batch
# Find MSBuild path (usually at):
# C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe

"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" ^
  RailwayIDCardMaker.sln ^
  /t:Restore;Build ^
  /p:Configuration=Release ^
  /p:Platform=x86
```

---

## 📁 Output Files

After successful build, files are located at:

```
RailwayIDCardMaker/
└── RailwayIDCardMaker/
    └── bin/
        └── x86/
            ├── Debug/    (or Release/)
                ├── RailwayIDCardMaker.exe       ← Main executable
                ├── RailwayIDCardMaker.exe.config
                ├── System.Data.SQLite.dll
                ├── AForge.dll
                ├── AForge.Video.dll
                ├── AForge.Video.DirectShow.dll
                └── x86/
                    └── SQLite.Interop.dll       ← Native SQLite library
```

---

## 🎯 Deployment

### Creating a Portable Release

**Method 1: Copy bin folder**

1. Build in **Release** mode (x86)
2. Copy entire `bin\x86\Release\` folder
3. Rename to `RailwayIDCardMaker`
4. Distribute this folder

**Method 2: Create installer** (Future enhancement)

- Use tools like: Inno Setup, WiX, or Advanced Installer
- Include prerequisites: .NET Framework 4.0

### Installation on Target Machine

**Requirements:**

- Windows 7 or later (x86 or x64)
- .NET Framework 4.0 or later

**Steps:**

1. Copy the application folder to `C:\Program Files (x86)\RailwayIDCardMaker\`
2. Create desktop shortcut to `RailwayIDCardMaker.exe`
3. Run the application (database auto-creates on first run)

### Data Storage Locations

```
%AppData%\RailwayIDCardMaker\
├── railway_idcard.db      # SQLite database
├── Photos\                # Employee photos
└── Signatures\            # Employee signatures
```

---

## ✅ Verification Checklist

After building, verify:

- [ ] Application starts without errors
- [ ] Login form appears with Railway branding
- [ ] Can login with admin/admin123
- [ ] Main form loads with menu and toolbar
- [ ] Can create a new employee card
- [ ] Can view data list
- [ ] Settings form opens
- [ ] About form shows version info

---

## 🐛 Known Limitations

1. **Windows 7 Support**: Requires .NET Framework 4.0 (pre-installed on Win7 SP1+)
2. **Platform**: Must build as **x86** (not AnyCPU) for compatibility
3. **Webcam**: Requires DirectShow-compatible cameras (most USB webcams)
4. **Printer**: Standard Windows printer drivers required

---

## 📞 Development Notes

### Modifying the Application

**Adding new forms:**

1. Right-click **Forms** folder → **Add** → **Form (Windows Forms)**
2. Design the form
3. Add event handlers in code-behind
4. Register in `MainForm.cs` if needed

**Modifying database schema:**

- Update `DatabaseService.cs` → `InitializeDatabase()` method
- Increment version number in constants
- Test with a fresh database

**Changing ID card design:**

- Edit `CardRenderer.cs`
- Modify `Constants.cs` for colors/fonts/dimensions
- Test print output on actual card stock

---

## 🔐 Security Notes

- Default admin password **must** be changed after deployment
- Aadhaar numbers are stored (consider encryption for production)
- Database file is unencrypted SQLite (protect file system access)
- No network transmission of data (standalone application)

---

## 📚 Additional Resources

- **Project Guide**: `PROJECT_GUIDE.md`
- **Project Status**: `PROJECT_STATUS.md`
- **Code Documentation**: Inline comments in source files
- **SQLite Documentation**: <https://www.sqlite.org/docs.html>
- **AForge Documentation**: <http://www.aforgenet.com/framework/>

---

**Last Updated**: December 20, 2024  
**Build System**: Visual Studio 2019+, MSBuild 16.0+  
**Target Framework**: .NET Framework 4.0  
**Platform**: Windows x86/x64
