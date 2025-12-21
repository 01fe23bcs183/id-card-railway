using System;

namespace RailwayIDCardMaker.Models
{
    /// <summary>
    /// Employee model representing railway employee data
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }

        // ID Card Number (YY + ZZ + UU + SSSSSS)
        public string IDCardNumber { get; set; }

        // Personal Information
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }

        // Contact Information
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string AadhaarNumber { get; set; }

        // Employment Information
        public string Designation { get; set; }
        public string Department { get; set; }
        public string PlaceOfPosting { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string PFNumber { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfRetirement { get; set; }

        // ID Card Information
        public DateTime? DateOfIssue { get; set; }
        public DateTime? ValidityDate { get; set; }
        public string IssuingAuthority { get; set; }
        public string IssuingAuthorityDesignation { get; set; }
        public int SerialNumber { get; set; }

        // Images (stored as file paths)
        public string PhotoPath { get; set; }
        public string SignaturePath { get; set; }
        public string AuthoritySignaturePath { get; set; }

        // Status
        public bool IsActive { get; set; }
        public bool IsCardPrinted { get; set; }
        public DateTime? LastPrintedDate { get; set; }
        public int PrintCount { get; set; }

        // Metadata
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        // Notes
        public string Remarks { get; set; }

        public Employee()
        {
            IsActive = true;
            IsCardPrinted = false;
            PrintCount = 0;
            CreatedDate = DateTime.Now;
            DateOfIssue = DateTime.Now;

            // Default validity: 5 years from issue
            ValidityDate = DateTime.Now.AddYears(5);
        }

        /// <summary>
        /// Get masked Aadhaar number for display on card
        /// </summary>
        public string GetMaskedAadhaar()
        {
            return Utils.Helpers.MaskAadhaar(AadhaarNumber);
        }

        /// <summary>
        /// Generate QR code data string
        /// </summary>
        public string GetQRCodeData()
        {
            return string.Join("|",
                    Name ?? "",
                    Address ?? "",
                    Designation ?? "",
                    PlaceOfPosting ?? "",
                    AadhaarNumber ?? "",
                    DateOfIssue?.ToString("dd-MM-yyyy") ?? "",
                    ValidityDate?.ToString("dd-MM-yyyy") ?? "",
                    IssuingAuthority ?? "",
                    IssuingAuthorityDesignation ?? ""
                );
        }

        /// <summary>
        /// Generate ID card number based on format: YY + ZZ + UU + SSSSSS
        /// </summary>
        public void GenerateIDCardNumber()
        {
            string year = Utils.Helpers.GetYearCode();
            string zone = (ZoneCode ?? "00").PadLeft(2, '0');
            string unit = (UnitCode ?? "00").PadLeft(2, '0');
            string serial = SerialNumber.ToString("D6");

            IDCardNumber = $"{year}{zone}{unit}{serial}";
        }

        public override string ToString()
        {
            return $"{Name} ({IDCardNumber})";
        }
    }
}
