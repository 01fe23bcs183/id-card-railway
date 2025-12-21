using System;

namespace RailwayIDCardMaker.Models
{
    /// <summary>
    /// Card settings and configuration
    /// </summary>
    public class CardSettings
    {
        public int Id { get; set; }

        // Issuing Authority defaults
        public string DefaultIssuingAuthority { get; set; }
        public string DefaultIssuingAuthorityDesignation { get; set; }
        public string DefaultAuthoritySignaturePath { get; set; }

        // Default zone and unit
        public string DefaultZoneCode { get; set; }
        public string DefaultZoneName { get; set; }
        public string DefaultUnitCode { get; set; }
        public string DefaultUnitName { get; set; }

        // Card validity period (in years)
        public int DefaultValidityYears { get; set; }

        // Serial number tracking
        public int LastSerialNumber { get; set; }

        // Print settings
        public string DefaultPrinterName { get; set; }
        public bool PrintFrontAndBack { get; set; }
        public bool UseDuplexPrinting { get; set; }

        // Logo path
        public string LogoPath { get; set; }

        // Organization name
        public string OrganizationName { get; set; }

        // Last updated
        public DateTime? LastUpdated { get; set; }

        public CardSettings()
        {
            DefaultValidityYears = 5;
            LastSerialNumber = 0;
            PrintFrontAndBack = true;
            UseDuplexPrinting = false;
            OrganizationName = "Indian Railways";
        }

        /// <summary>
        /// Get next serial number
        /// </summary>
        public int GetNextSerialNumber()
        {
            LastSerialNumber++;
            return LastSerialNumber;
        }
    }
}
