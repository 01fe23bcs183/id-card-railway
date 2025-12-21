using System;

namespace RailwayIDCardMaker.Models
{
    /// <summary>
    /// Unit model representing divisions/units within a zone
    /// </summary>
    public class Unit
    {
        public int Id { get; set; }
        public string Code { get; set; }      // 2-digit unit code
        public string Name { get; set; }      // Full unit name
        public string ZoneCode { get; set; }  // Parent zone code
        public bool IsActive { get; set; }

        public Unit()
        {
            IsActive = true;
        }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }

        /// <summary>
        /// Get sample units (can be customized based on zone)
        /// </summary>
        public static Unit[] GetSampleUnits()
        {
            return new Unit[]
            {
                // Divisions
                new Unit { Code = "01", Name = "Headquarters", ZoneCode = "01" },
                new Unit { Code = "02", Name = "Division I", ZoneCode = "01" },
                new Unit { Code = "03", Name = "Division II", ZoneCode = "01" },
                new Unit { Code = "04", Name = "Division III", ZoneCode = "01" },
                new Unit { Code = "05", Name = "Division IV", ZoneCode = "01" },
                
                // Workshops
                new Unit { Code = "10", Name = "Main Workshop", ZoneCode = "01" },
                new Unit { Code = "11", Name = "Carriage Workshop", ZoneCode = "01" },
                new Unit { Code = "12", Name = "Loco Workshop", ZoneCode = "01" },
                
                // Sheds
                new Unit { Code = "20", Name = "Electric Loco Shed", ZoneCode = "01" },
                new Unit { Code = "21", Name = "Diesel Loco Shed", ZoneCode = "01" },
                new Unit { Code = "22", Name = "EMU Car Shed", ZoneCode = "01" },
                
                // Construction
                new Unit { Code = "30", Name = "Construction", ZoneCode = "01" },
                
                // Stores
                new Unit { Code = "40", Name = "Stores Depot", ZoneCode = "01" },
                
                // Training
                new Unit { Code = "50", Name = "Training Centre", ZoneCode = "01" },
                
                // Hospital
                new Unit { Code = "60", Name = "Railway Hospital", ZoneCode = "01" },
                
                // Security
                new Unit { Code = "70", Name = "RPF", ZoneCode = "01" },
            };
        }
    }
}
