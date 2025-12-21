using System;

namespace RailwayIDCardMaker.Models
{
    /// <summary>
    /// Railway Zone model
    /// </summary>
    public class Zone
    {
        public int Id { get; set; }
        public string Code { get; set; }      // 2-digit zone code
        public string Name { get; set; }      // Full zone name
        public string Abbreviation { get; set; } // Short form (e.g., WR, CR, SR)
        public string Headquarters { get; set; }
        public bool IsActive { get; set; }

        public Zone()
        {
            IsActive = true;
        }

        public override string ToString()
        {
            return $"{Code} - {Abbreviation} ({Name})";
        }

        /// <summary>
        /// Get all Indian Railway zones with their codes
        /// </summary>
        public static Zone[] GetAllZones()
        {
            return new Zone[]
            {
                new Zone { Code = "01", Name = "Central Railway", Abbreviation = "CR", Headquarters = "Mumbai CST" },
                new Zone { Code = "02", Name = "Eastern Railway", Abbreviation = "ER", Headquarters = "Kolkata" },
                new Zone { Code = "03", Name = "Northern Railway", Abbreviation = "NR", Headquarters = "New Delhi" },
                new Zone { Code = "04", Name = "North Eastern Railway", Abbreviation = "NER", Headquarters = "Gorakhpur" },
                new Zone { Code = "05", Name = "Northeast Frontier Railway", Abbreviation = "NFR", Headquarters = "Maligaon" },
                new Zone { Code = "06", Name = "Southern Railway", Abbreviation = "SR", Headquarters = "Chennai" },
                new Zone { Code = "07", Name = "South Central Railway", Abbreviation = "SCR", Headquarters = "Secunderabad" },
                new Zone { Code = "08", Name = "South Eastern Railway", Abbreviation = "SER", Headquarters = "Kolkata" },
                new Zone { Code = "09", Name = "Western Railway", Abbreviation = "WR", Headquarters = "Mumbai Church Gate" },
                new Zone { Code = "10", Name = "South Western Railway", Abbreviation = "SWR", Headquarters = "Hubli" },
                new Zone { Code = "11", Name = "North Western Railway", Abbreviation = "NWR", Headquarters = "Jaipur" },
                new Zone { Code = "12", Name = "West Central Railway", Abbreviation = "WCR", Headquarters = "Jabalpur" },
                new Zone { Code = "13", Name = "North Central Railway", Abbreviation = "NCR", Headquarters = "Allahabad" },
                new Zone { Code = "14", Name = "South East Central Railway", Abbreviation = "SECR", Headquarters = "Bilaspur" },
                new Zone { Code = "15", Name = "East Coast Railway", Abbreviation = "ECoR", Headquarters = "Bhubaneswar" },
                new Zone { Code = "16", Name = "East Central Railway", Abbreviation = "ECR", Headquarters = "Hajipur" },
                new Zone { Code = "17", Name = "Kolkata Metro Railway", Abbreviation = "KMR", Headquarters = "Kolkata" },
                new Zone { Code = "18", Name = "Konkan Railway", Abbreviation = "KR", Headquarters = "Navi Mumbai" },
                
                // Production Units and Training Institutes
                new Zone { Code = "31", Name = "Chittaranjan Locomotive Works", Abbreviation = "CLW", Headquarters = "Chittaranjan" },
                new Zone { Code = "32", Name = "Diesel Locomotive Works", Abbreviation = "DLW", Headquarters = "Varanasi" },
                new Zone { Code = "33", Name = "Integral Coach Factory", Abbreviation = "ICF", Headquarters = "Chennai" },
                new Zone { Code = "34", Name = "Rail Coach Factory", Abbreviation = "RCF", Headquarters = "Kapurthala" },
                new Zone { Code = "35", Name = "Rail Wheel Factory", Abbreviation = "RWF", Headquarters = "Bangalore" },
                new Zone { Code = "36", Name = "Modern Coach Factory", Abbreviation = "MCF", Headquarters = "Raebareli" },
                new Zone { Code = "37", Name = "Rail Wheel Plant", Abbreviation = "RWP", Headquarters = "Bela" },
                
                // Research and others
                new Zone { Code = "41", Name = "RDSO", Abbreviation = "RDSO", Headquarters = "Lucknow" },
                new Zone { Code = "42", Name = "Railway Board", Abbreviation = "RB", Headquarters = "New Delhi" },
                new Zone { Code = "43", Name = "IRITM", Abbreviation = "IRITM", Headquarters = "Lucknow" },
            };
        }
    }
}
