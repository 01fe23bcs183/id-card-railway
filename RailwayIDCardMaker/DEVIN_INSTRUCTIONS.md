# Railway ID Card Maker - Devin Instructions

## Project Overview

This is a Windows Forms (.NET Framework 4.7.2) application for generating Indian Railways Employee ID Cards. It uses Microsoft Access (.mdb) as the database backend.

## Current Issues to Fix

### 1. ID Card Rendering - Text Overlap Problem

**File:** `RailwayIDCardMaker\Services\CardRenderer.cs`

**Problem:** Text elements are overlapping in the print preview. The card dimensions are:

- Width: 54mm (638 pixels @ 300 DPI)
- Height: 87mm (1028 pixels @ 300 DPI)

**Reference Design Requirements (from official specification):**

- Font: Times New Roman
- Font sizes: 12 & 8 for header, 12 & 9 for name/designation sections
- Card is divided into:
  - Top: Logo (left) + "Ministry of Railways" + "Government of India" + ID Number
  - Middle: Employee Photo (3.85cm x 4.35cm with rounded corners)
  - Below photo: Signature box (4.2cm wide)
  - Bottom: Name, Designation, Issuing Authority section

**Solution Approach:**
The issue is likely that the Graphics object is using screen DPI instead of the 300 DPI set on the bitmap. You need to ensure all drawing operations account for the correct scaling. Try:

1. Use `g.PageUnit = GraphicsUnit.Pixel;` at the start
2. All coordinates should be in pixels (at 300 DPI)
3. Test by exporting the card to PNG and checking actual pixel positions

### 2. Employee Data Not Loading in Edit Form

**Files:**

- `RailwayIDCardMaker\Forms\EmployeeForm.cs` - DisplayEmployee method
- `RailwayIDCardMaker\Forms\MainForm.cs` - OpenEmployeeForm method
- `RailwayIDCardMaker\Services\DatabaseService.cs` - GetEmployee, MapEmployee methods

**Problem:** When clicking Edit on an employee in the Data List, not all fields load into the form. The data IS in the database (shows in grid) but text fields like Designation, Department, Mobile, etc. don't populate in the form.

**Possible Causes:**

1. The MapEmployee method might not be reading all columns correctly
2. The DisplayEmployee method might have issues with control binding
3. The form might not be fully initialized when data is loaded

**Solution Approach:**

1. Add debug logging to MapEmployee to verify all fields are being read
2. Check if column names in SQL match the Employee model properties
3. Ensure DisplayEmployee is called AFTER the form is fully visible

### 3. Print Paper Size

**File:** `RailwayIDCardMaker\Services\PrintService.cs`

**Current Issue:** Paper size is set to ID card dimensions (87mm x 54mm) but print preview shows it on A4-sized canvas.

**Solution:** Verify that the PaperSize is being applied correctly. The print preview control might be scaling differently.

## Database Schema

The database is Microsoft Access (.mdb) with the following main table:

```sql
Employees (
    Id AUTOINCREMENT PRIMARY KEY,
    IDCardNumber TEXT(50),
    Name TEXT(100),
    FatherName TEXT(100),
    DateOfBirth DATETIME,
    BloodGroup TEXT(10),
    Gender TEXT(10),
    Address MEMO,
    MobileNumber TEXT(20),
    AadhaarNumber TEXT(20),
    Designation TEXT(100),
    Department TEXT(100),
    PlaceOfPosting TEXT(100),
    ZoneCode TEXT(10),
    ZoneName TEXT(100),
    UnitCode TEXT(20),
    UnitName TEXT(100),
    PFNumber TEXT(50),
    DateOfJoining DATETIME,
    DateOfRetirement DATETIME,
    DateOfIssue DATETIME,
    ValidityDate DATETIME,
    IssuingAuthority TEXT(100),
    IssuingAuthorityDesignation TEXT(100),
    SerialNumber LONG,
    PhotoPath TEXT(255),
    SignaturePath TEXT(255),
    AuthoritySignaturePath TEXT(255),
    -- ... additional fields
)
```

## Official ID Card Specification

### Card Front

1. **Header:**
   - Ashoka Chakra logo (left, ~10mm diameter)
   - "Ministry of Railways" (Font 12, Bold)
   - "Government of India" (Font 8)
   - "No XXXXXX" (ID Card Number, Font 8)

2. **Title:** "Employee Identity Card" (Font 12, Bold, Red, Centered)

3. **Photo Area:**
   - Size: 3.85cm x 4.35cm
   - Rounded corners
   - Centered horizontally
   - Left side: Vertical text "Valid Upto: DD/MM/YYYY" (Font 6)

4. **Signature Box:**
   - Width: 4.2cm
   - Centered below photo
   - Label: "Signature of Card Holder"

5. **Name:** "Name : XXXXX" (Font 12, Bold)

6. **Designation:** "Designation: XXXXX" (Font 9)

7. **Issuing Authority:** (Right side)
   - Small signature box
   - "(Signature)"
   - "Designation of Issuing Authority"

### Card Back

1. **QR Code:** (Left, contains employee details)
2. **Blood Group:** (Right, large font, red)
3. **Department Box:** (Full width, bordered)
4. **Designation**
5. **Mobile Number**
6. **Aadhaar (masked)**
7. **Date of Issue**
8. **Instruction text**

### QR Code Content (plain text mode)

The QR encodes plain text (no link). Example fields:

1. Name
2. Address
3. Designation
4. Department
5. Place of Posting
6. Zone
7. Mobile
8. Aadhaar
9. DOB
10. Date of Issue
11. Validity
12. Issuing Authority
13. Authority Designation
14. ID

## Build Instructions

1. Open `RailwayIDCardMaker.sln` in Visual Studio 2019+
2. Restore NuGet packages (AForge for camera capture)
3. Build in Debug mode
4. Run - the database will be auto-created on first run
5. Default login: admin / admin

## Key Files

| File | Purpose |
|------|---------|
| `Services/CardRenderer.cs` | Renders ID card front/back as bitmap |
| `Services/PrintService.cs` | Handles printing with custom paper size |
| `Services/DatabaseService.cs` | All database operations |
| `Forms/EmployeeForm.cs` | Main form for adding/editing employees |
| `Forms/MainForm.cs` | Main MDI container |
| `Models/Employee.cs` | Employee data model |

## Testing Steps

1. Run the application
2. Login with admin/admin
3. Click "+ New Card"
4. Fill ALL fields (Name, Father Name, Address, Mobile, Aadhaar, Blood Group, Gender, Designation, Department, Place of Posting, Zone, Unit Code, PF Number, Issuing Authority, etc.)
5. Add photo and signature
6. Click Save
7. Go to Data List
8. Click Edit on the saved employee
9. Verify ALL fields are populated
10. Click Print Preview
11. Verify card layout has no overlapping text

## Expected Final Result

The ID card should look exactly like the reference images provided:

- Clean, non-overlapping text
- Proper spacing between elements
- Photo with rounded corners
- All data fields visible and readable
- Print at actual ID card size (87mm x 54mm)
