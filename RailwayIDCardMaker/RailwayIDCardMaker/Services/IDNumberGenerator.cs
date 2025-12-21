using System;

namespace RailwayIDCardMaker.Services
{
    /// <summary>
    /// ID Number Generator service
    /// Generates ID card numbers in format: YY + ZZ + UU + SSSSSS
    /// </summary>
    public static class IDNumberGenerator
    {
        /// <summary>
        /// Generate new ID card number
        /// Format: YY (Year) + ZZ (Zone Code) + UU (Unit Code) + SSSSSS (Serial)
        /// Example: 25 + 01 + 02 + 000001 = 250102000001
        /// </summary>
        public static string GenerateIDNumber(string zoneCode, string unitCode, int serialNumber)
        {
            // Get 2-digit year (financial year based - April to March)
            string yearCode = GetYearCode();

            // Ensure zone code is 2 digits
            string zone = (zoneCode ?? "00").PadLeft(2, '0');
            if (zone.Length > 2) zone = zone.Substring(0, 2);

            // Ensure unit code is 2 digits
            string unit = (unitCode ?? "00").PadLeft(2, '0');
            if (unit.Length > 2) unit = unit.Substring(0, 2);

            // Ensure serial number is 6 digits
            string serial = serialNumber.ToString("D6");
            if (serial.Length > 6) serial = serial.Substring(serial.Length - 6);

            return $"{yearCode}{zone}{unit}{serial}";
        }

        /// <summary>
        /// Generate ID number with auto-incremented serial
        /// </summary>
        public static string GenerateIDNumber(string zoneCode, string unitCode)
        {
            int nextSerial = DatabaseService.GetNextSerialNumber();
            return GenerateIDNumber(zoneCode, unitCode, nextSerial);
        }

        /// <summary>
        /// Get 2-digit year code based on financial year
        /// Financial year runs from April to March
        /// </summary>
        public static string GetYearCode()
        {
            DateTime now = DateTime.Now;
            int year;

            // If current month is January-March, use previous year
            if (now.Month >= 4)
            {
                year = now.Year;
            }
            else
            {
                year = now.Year - 1;
            }

            // Return last 2 digits
            return (year % 100).ToString("D2");
        }

        /// <summary>
        /// Parse ID number into components
        /// </summary>
        public static bool TryParseIDNumber(string idNumber, out string yearCode, out string zoneCode,
            out string unitCode, out int serialNumber)
        {
            yearCode = "";
            zoneCode = "";
            unitCode = "";
            serialNumber = 0;

            if (string.IsNullOrEmpty(idNumber) || idNumber.Length != 12)
            {
                return false;
            }

            try
            {
                yearCode = idNumber.Substring(0, 2);
                zoneCode = idNumber.Substring(2, 2);
                unitCode = idNumber.Substring(4, 2);
                serialNumber = int.Parse(idNumber.Substring(6, 6));

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Format ID number for display (with dashes)
        /// Example: 25-01-02-000001
        /// </summary>
        public static string FormatIDNumber(string idNumber)
        {
            if (string.IsNullOrEmpty(idNumber) || idNumber.Length != 12)
            {
                return idNumber ?? "";
            }

            return $"{idNumber.Substring(0, 2)}-{idNumber.Substring(2, 2)}-{idNumber.Substring(4, 2)}-{idNumber.Substring(6, 6)}";
        }

        /// <summary>
        /// Validate ID number format
        /// </summary>
        public static bool IsValidIDNumber(string idNumber)
        {
            if (string.IsNullOrEmpty(idNumber) || idNumber.Length != 12)
            {
                return false;
            }

            // Check if all characters are digits
            foreach (char c in idNumber)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if ID number already exists
        /// </summary>
        public static bool IDNumberExists(string idNumber)
        {
            var employees = DatabaseService.SearchEmployees(idNumber);
            foreach (var emp in employees)
            {
                if (emp.IDCardNumber == idNumber)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
