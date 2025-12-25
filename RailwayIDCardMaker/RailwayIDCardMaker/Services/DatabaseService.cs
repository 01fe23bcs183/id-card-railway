using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Utils;

namespace RailwayIDCardMaker.Services
{
    /// <summary>
    /// Database service for Microsoft Access operations
    /// </summary>
    public static class DatabaseService
    {
        private static string _connectionString;
        private static string _dbPath;

        /// <summary>
        /// Get database file path
        /// </summary>
        public static string DatabasePath
        {
            get
            {
                if (string.IsNullOrEmpty(_dbPath))
                {
                    _dbPath = Path.Combine(Helpers.GetAppDataDirectory(), "RailwayIDCard.mdb");
                }
                return _dbPath;
            }
        }

        /// <summary>
        /// Get database connection string (JET provider for Windows 7 compatibility)
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    // Use JET provider (built into Windows 7/8/10 32-bit)
                    _connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={DatabasePath};";
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// Initialize database and create tables if not exist
        /// </summary>
        public static void InitializeDatabase()
        {
            // Create database file if not exists
            if (!File.Exists(DatabasePath))
            {
                CreateAccessDatabase();
            }

            // Create tables
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                CreateTables(connection);
                InsertDefaultUser(connection);
                InsertDefaultSettings(connection);
                InsertDefaultZones(connection);
            }
        }

        private static void CreateAccessDatabase()
        {
            // Ensure directory exists
            string dir = Path.GetDirectoryName(DatabasePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // Create Access .mdb database using ADOX Catalog (COM)
            // JET 4.0 provider is built into Windows 7
            Type catalogType = Type.GetTypeFromProgID("ADOX.Catalog");
            if (catalogType != null)
            {
                dynamic catalog = Activator.CreateInstance(catalogType);
                try
                {
                    string connStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={DatabasePath};Jet OLEDB:Engine Type=5;";
                    catalog.Create(connStr);
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(catalog);
                    catalog = null;
                    GC.Collect();
                }
            }
            else
            {
                throw new Exception("Cannot create database. ADOX is not available on this system.");
            }
        }

        private static void CreateTables(OleDbConnection connection)
        {
            // Create Users table
            if (!TableExists(connection, "Users"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Users (
                        Id COUNTER PRIMARY KEY,
                        Username TEXT(50) NOT NULL,
                        PasswordHash TEXT(255) NOT NULL,
                        FullName TEXT(100),
                        Designation TEXT(100),
                        Role TEXT(20) DEFAULT 'Operator',
                        IsActive YESNO DEFAULT TRUE,
                        CreatedDate DATETIME,
                        LastLoginDate DATETIME
                    )");
                ExecuteNonQuery(connection, "CREATE UNIQUE INDEX idx_Username ON Users (Username)");
            }

            // Create Employees table
            if (!TableExists(connection, "Employees"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Employees (
                        Id COUNTER PRIMARY KEY,
                        IDCardNumber TEXT(20),
                        Name TEXT(100) NOT NULL,
                        FatherName TEXT(100),
                        DateOfBirth DATETIME,
                        BloodGroup TEXT(10),
                        Gender TEXT(10),
                        Address MEMO,
                        MobileNumber TEXT(15),
                        AadhaarNumber TEXT(20),
                        Designation TEXT(100),
                        Department TEXT(100),
                        PlaceOfPosting TEXT(100),
                        ZoneCode TEXT(10),
                        ZoneName TEXT(100),
                        UnitCode TEXT(10),
                        UnitName TEXT(100),
                        PFNumber TEXT(20),
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
                        IsActive YESNO DEFAULT TRUE,
                        IsCardPrinted YESNO DEFAULT FALSE,
                        LastPrintedDate DATETIME,
                        PrintCount LONG DEFAULT 0,
                        CreatedDate DATETIME,
                        ModifiedDate DATETIME,
                        CreatedBy TEXT(50),
                        ModifiedBy TEXT(50),
                        Remarks MEMO
                    )");
                ExecuteNonQuery(connection, "CREATE UNIQUE INDEX idx_IDCardNumber ON Employees (IDCardNumber)");
            }

            // Create Zones table
            if (!TableExists(connection, "Zones"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Zones (
                        Id COUNTER PRIMARY KEY,
                        Code TEXT(10) NOT NULL,
                        Name TEXT(100) NOT NULL,
                        Abbreviation TEXT(20),
                        Headquarters TEXT(100),
                        IsActive YESNO DEFAULT TRUE
                    )");
                ExecuteNonQuery(connection, "CREATE UNIQUE INDEX idx_ZoneCode ON Zones (Code)");
            }

            // Create Units table
            if (!TableExists(connection, "Units"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Units (
                        Id COUNTER PRIMARY KEY,
                        Code TEXT(10) NOT NULL,
                        Name TEXT(100) NOT NULL,
                        ZoneCode TEXT(10),
                        IsActive YESNO DEFAULT TRUE
                    )");
            }

            // Create Settings table
            if (!TableExists(connection, "Settings"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Settings (
                        Id COUNTER PRIMARY KEY,
                        DefaultIssuingAuthority TEXT(100),
                        DefaultIssuingAuthorityDesignation TEXT(100),
                        DefaultAuthoritySignaturePath TEXT(255),
                        DefaultZoneCode TEXT(10),
                        DefaultZoneName TEXT(100),
                        DefaultUnitCode TEXT(10),
                        DefaultUnitName TEXT(100),
                        DefaultValidityYears LONG DEFAULT 5,
                        LastSerialNumber LONG DEFAULT 0,
                        DefaultPrinterName TEXT(100),
                        PrintFrontAndBack YESNO DEFAULT TRUE,
                        UseDuplexPrinting YESNO DEFAULT FALSE,
                        LogoPath TEXT(255),
                        OrganizationName TEXT(100) DEFAULT 'Indian Railways',
                        LastUpdated DATETIME
                    )");
            }

            // Create PrintLog table
            if (!TableExists(connection, "PrintLog"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE PrintLog (
                        Id COUNTER PRIMARY KEY,
                        EmployeeId LONG,
                        IDCardNumber TEXT(20),
                        PrintedDate DATETIME,
                        PrintedBy TEXT(50),
                        PrintType TEXT(20)
                    )");
            }
        }

        private static bool TableExists(OleDbConnection connection, string tableName)
        {
            DataTable schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                new object[] { null, null, tableName, "TABLE" });
            return schema.Rows.Count > 0;
        }

        private static void ExecuteNonQuery(OleDbConnection connection, string sql)
        {
            using (var command = new OleDbCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void InsertDefaultUser(OleDbConnection connection)
        {
            string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = ?";
            using (var command = new OleDbCommand(checkSql, connection))
            {
                command.Parameters.AddWithValue("?", Constants.DEFAULT_USERNAME);
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                {
                    string insertSql = @"INSERT INTO Users (Username, PasswordHash, FullName, Designation, Role, IsActive, CreatedDate)
                                        VALUES (?, ?, ?, ?, ?, TRUE, ?)";
                    using (var insertCommand = new OleDbCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.AddWithValue("?", Constants.DEFAULT_USERNAME);
                        insertCommand.Parameters.AddWithValue("?", Helpers.HashPassword(Constants.DEFAULT_PASSWORD));
                        insertCommand.Parameters.AddWithValue("?", "Administrator");
                        insertCommand.Parameters.AddWithValue("?", "System Admin");
                        insertCommand.Parameters.AddWithValue("?", UserRoles.Admin);
                        insertCommand.Parameters.AddWithValue("?", DateTime.Now);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void InsertDefaultSettings(OleDbConnection connection)
        {
            string checkSql = "SELECT COUNT(*) FROM Settings";
            using (var command = new OleDbCommand(checkSql, connection))
            {
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                {
                    string insertSql = @"INSERT INTO Settings (OrganizationName, DefaultValidityYears, LastSerialNumber, PrintFrontAndBack, UseDuplexPrinting, LastUpdated)
                                        VALUES (?, ?, ?, TRUE, FALSE, ?)";
                    using (var insertCommand = new OleDbCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.AddWithValue("?", "Indian Railways");
                        insertCommand.Parameters.AddWithValue("?", 5);
                        insertCommand.Parameters.AddWithValue("?", 0);
                        insertCommand.Parameters.AddWithValue("?", DateTime.Now);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void InsertDefaultZones(OleDbConnection connection)
        {
            string checkSql = "SELECT COUNT(*) FROM Zones";
            using (var command = new OleDbCommand(checkSql, connection))
            {
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                {
                    var zones = new[]
                    {
                        ("01", "Central Railway", "CR", "Mumbai"),
                        ("02", "Eastern Railway", "ER", "Kolkata"),
                        ("03", "East Central Railway", "ECR", "Hajipur"),
                        ("04", "East Coast Railway", "ECoR", "Bhubaneswar"),
                        ("05", "Northern Railway", "NR", "New Delhi"),
                        ("06", "North Central Railway", "NCR", "Prayagraj"),
                        ("07", "North Eastern Railway", "NER", "Gorakhpur"),
                        ("08", "Northeast Frontier Railway", "NFR", "Guwahati"),
                        ("09", "North Western Railway", "NWR", "Jaipur"),
                        ("10", "Southern Railway", "SR", "Chennai"),
                        ("11", "South Central Railway", "SCR", "Secunderabad"),
                        ("12", "South Eastern Railway", "SER", "Kolkata"),
                        ("13", "South East Central Railway", "SECR", "Bilaspur"),
                        ("14", "South Western Railway", "SWR", "Hubli"),
                        ("15", "Western Railway", "WR", "Mumbai"),
                        ("16", "West Central Railway", "WCR", "Jabalpur"),
                        ("17", "Metro Railway Kolkata", "MRK", "Kolkata")
                    };

                    foreach (var zone in zones)
                    {
                        string insertSql = "INSERT INTO Zones (Code, Name, Abbreviation, Headquarters, IsActive) VALUES (?, ?, ?, ?, TRUE)";
                        using (var insertCommand = new OleDbCommand(insertSql, connection))
                        {
                            insertCommand.Parameters.AddWithValue("?", zone.Item1);
                            insertCommand.Parameters.AddWithValue("?", zone.Item2);
                            insertCommand.Parameters.AddWithValue("?", zone.Item3);
                            insertCommand.Parameters.AddWithValue("?", zone.Item4);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        #region User Operations

        public static User AuthenticateUser(string username, string passwordHash)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Users WHERE Username = ? AND PasswordHash = ? AND IsActive = TRUE";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", username);
                    command.Parameters.AddWithValue("?", passwordHash);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var user = MapUser(reader);
                            UpdateLastLogin(user.Id);
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public static void UpdateLastLogin(int userId)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "UPDATE Users SET LastLoginDate = ? WHERE Id = ?";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", DateTime.Now);
                    command.Parameters.AddWithValue("?", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool ChangePassword(int userId, string newPasswordHash)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "UPDATE Users SET PasswordHash = ? WHERE Id = ?";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", newPasswordHash);
                    command.Parameters.AddWithValue("?", userId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        private static User MapUser(OleDbDataReader reader)
        {
            return new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"]?.ToString(),
                PasswordHash = reader["PasswordHash"]?.ToString(),
                FullName = reader["FullName"]?.ToString(),
                Designation = reader["Designation"]?.ToString(),
                Role = reader["Role"]?.ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                LastLoginDate = reader["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastLoginDate"]) : (DateTime?)null
            };
        }

        #endregion

        #region Employee Operations

        public static List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Employees WHERE IsActive = TRUE ORDER BY Name";
                using (var command = new OleDbCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(MapEmployee(reader));
                        }
                    }
                }
            }
            return employees;
        }

        public static Employee GetEmployeeById(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Employees WHERE Id = ?";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapEmployee(reader);
                        }
                    }
                }
            }
            return null;
        }

        public static int SaveEmployee(Employee employee)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();

                if (employee.Id == 0)
                {
                    // Insert new employee
                    string sql = @"INSERT INTO Employees (IDCardNumber, Name, FatherName, DateOfBirth, BloodGroup, Gender,
                        Address, MobileNumber, AadhaarNumber, Designation, Department, PlaceOfPosting,
                        ZoneCode, ZoneName, UnitCode, UnitName, PFNumber, DateOfJoining, DateOfRetirement,
                        DateOfIssue, ValidityDate, IssuingAuthority, IssuingAuthorityDesignation, SerialNumber,
                        PhotoPath, SignaturePath, AuthoritySignaturePath, IsActive, IsCardPrinted, PrintCount,
                        CreatedDate, CreatedBy, Remarks)
                        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, TRUE, FALSE, 0, ?, ?, ?)";

                    using (var command = new OleDbCommand(sql, connection))
                    {
                        AddEmployeeParameters(command, employee);
                        command.Parameters.AddWithValue("?", DateTime.Now);
                        command.Parameters.AddWithValue("?", employee.CreatedBy ?? "");
                        command.Parameters.AddWithValue("?", employee.Remarks ?? "");
                        command.ExecuteNonQuery();
                    }

                    // Get the new ID
                    using (var idCommand = new OleDbCommand("SELECT @@IDENTITY", connection))
                    {
                        employee.Id = Convert.ToInt32(idCommand.ExecuteScalar());
                    }
                }
                else
                {
                    // Update existing employee
                    string sql = @"UPDATE Employees SET 
                        IDCardNumber = ?, Name = ?, FatherName = ?, DateOfBirth = ?, BloodGroup = ?, Gender = ?,
                        Address = ?, MobileNumber = ?, AadhaarNumber = ?, Designation = ?, Department = ?, PlaceOfPosting = ?,
                        ZoneCode = ?, ZoneName = ?, UnitCode = ?, UnitName = ?, PFNumber = ?, DateOfJoining = ?, DateOfRetirement = ?,
                        DateOfIssue = ?, ValidityDate = ?, IssuingAuthority = ?, IssuingAuthorityDesignation = ?, SerialNumber = ?,
                        PhotoPath = ?, SignaturePath = ?, AuthoritySignaturePath = ?, ModifiedDate = ?, ModifiedBy = ?, Remarks = ?
                        WHERE Id = ?";

                    using (var command = new OleDbCommand(sql, connection))
                    {
                        AddEmployeeParameters(command, employee);
                        command.Parameters.AddWithValue("?", DateTime.Now);
                        command.Parameters.AddWithValue("?", employee.ModifiedBy ?? "");
                        command.Parameters.AddWithValue("?", employee.Remarks ?? "");
                        command.Parameters.AddWithValue("?", employee.Id);
                        command.ExecuteNonQuery();
                    }
                }

                return employee.Id;
            }
        }

        private static void AddEmployeeParameters(OleDbCommand command, Employee employee)
        {
            command.Parameters.AddWithValue("?", employee.IDCardNumber ?? "");
            command.Parameters.AddWithValue("?", employee.Name ?? "");
            command.Parameters.AddWithValue("?", employee.FatherName ?? "");
            command.Parameters.AddWithValue("?", employee.DateOfBirth.HasValue ? (object)employee.DateOfBirth.Value : DBNull.Value);
            command.Parameters.AddWithValue("?", employee.BloodGroup ?? "");
            command.Parameters.AddWithValue("?", employee.Gender ?? "");
            command.Parameters.AddWithValue("?", employee.Address ?? "");
            command.Parameters.AddWithValue("?", employee.MobileNumber ?? "");
            command.Parameters.AddWithValue("?", employee.AadhaarNumber ?? "");
            command.Parameters.AddWithValue("?", employee.Designation ?? "");
            command.Parameters.AddWithValue("?", employee.Department ?? "");
            command.Parameters.AddWithValue("?", employee.PlaceOfPosting ?? "");
            command.Parameters.AddWithValue("?", employee.ZoneCode ?? "");
            command.Parameters.AddWithValue("?", employee.ZoneName ?? "");
            command.Parameters.AddWithValue("?", employee.UnitCode ?? "");
            command.Parameters.AddWithValue("?", employee.UnitName ?? "");
            command.Parameters.AddWithValue("?", employee.PFNumber ?? "");
            command.Parameters.AddWithValue("?", employee.DateOfJoining.HasValue ? (object)employee.DateOfJoining.Value : DBNull.Value);
            command.Parameters.AddWithValue("?", employee.DateOfRetirement.HasValue ? (object)employee.DateOfRetirement.Value : DBNull.Value);
            command.Parameters.AddWithValue("?", employee.DateOfIssue.HasValue ? (object)employee.DateOfIssue.Value : DBNull.Value);
            command.Parameters.AddWithValue("?", employee.ValidityDate.HasValue ? (object)employee.ValidityDate.Value : DBNull.Value);
            command.Parameters.AddWithValue("?", employee.IssuingAuthority ?? "");
            command.Parameters.AddWithValue("?", employee.IssuingAuthorityDesignation ?? "");
            command.Parameters.AddWithValue("?", employee.SerialNumber);
            command.Parameters.AddWithValue("?", employee.PhotoPath ?? "");
            command.Parameters.AddWithValue("?", employee.SignaturePath ?? "");
            command.Parameters.AddWithValue("?", employee.AuthoritySignaturePath ?? "");
        }

        public static bool DeleteEmployee(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                // Soft delete
                string sql = "UPDATE Employees SET IsActive = FALSE WHERE Id = ?";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<Employee> SearchEmployees(string searchTerm)
        {
            var employees = new List<Employee>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = @"SELECT * FROM Employees 
                              WHERE IsActive = TRUE AND 
                              (Name LIKE ? OR IDCardNumber LIKE ? OR MobileNumber LIKE ? OR Department LIKE ?)
                              ORDER BY Name";
                using (var command = new OleDbCommand(sql, connection))
                {
                    string term = $"%{searchTerm}%";
                    command.Parameters.AddWithValue("?", term);
                    command.Parameters.AddWithValue("?", term);
                    command.Parameters.AddWithValue("?", term);
                    command.Parameters.AddWithValue("?", term);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(MapEmployee(reader));
                        }
                    }
                }
            }
            return employees;
        }

        private static Employee MapEmployee(OleDbDataReader reader)
        {
            return new Employee
            {
                Id = Convert.ToInt32(reader["Id"]),
                IDCardNumber = reader["IDCardNumber"]?.ToString(),
                Name = reader["Name"]?.ToString(),
                FatherName = reader["FatherName"]?.ToString(),
                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]) : (DateTime?)null,
                BloodGroup = reader["BloodGroup"]?.ToString(),
                Gender = reader["Gender"]?.ToString(),
                Address = reader["Address"]?.ToString(),
                MobileNumber = reader["MobileNumber"]?.ToString(),
                AadhaarNumber = reader["AadhaarNumber"]?.ToString(),
                Designation = reader["Designation"]?.ToString(),
                Department = reader["Department"]?.ToString(),
                PlaceOfPosting = reader["PlaceOfPosting"]?.ToString(),
                ZoneCode = reader["ZoneCode"]?.ToString(),
                ZoneName = reader["ZoneName"]?.ToString(),
                UnitCode = reader["UnitCode"]?.ToString(),
                UnitName = reader["UnitName"]?.ToString(),
                PFNumber = reader["PFNumber"]?.ToString(),
                DateOfJoining = reader["DateOfJoining"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfJoining"]) : (DateTime?)null,
                DateOfRetirement = reader["DateOfRetirement"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfRetirement"]) : (DateTime?)null,
                DateOfIssue = reader["DateOfIssue"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfIssue"]) : (DateTime?)null,
                ValidityDate = reader["ValidityDate"] != DBNull.Value ? Convert.ToDateTime(reader["ValidityDate"]) : (DateTime?)null,
                IssuingAuthority = reader["IssuingAuthority"]?.ToString(),
                IssuingAuthorityDesignation = reader["IssuingAuthorityDesignation"]?.ToString(),
                SerialNumber = reader["SerialNumber"] != DBNull.Value ? Convert.ToInt32(reader["SerialNumber"]) : 0,
                PhotoPath = reader["PhotoPath"]?.ToString(),
                SignaturePath = reader["SignaturePath"]?.ToString(),
                AuthoritySignaturePath = reader["AuthoritySignaturePath"]?.ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                IsCardPrinted = Convert.ToBoolean(reader["IsCardPrinted"]),
                LastPrintedDate = reader["LastPrintedDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastPrintedDate"]) : (DateTime?)null,
                PrintCount = reader["PrintCount"] != DBNull.Value ? Convert.ToInt32(reader["PrintCount"]) : 0,
                CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                CreatedBy = reader["CreatedBy"]?.ToString(),
                ModifiedBy = reader["ModifiedBy"]?.ToString(),
                Remarks = reader["Remarks"]?.ToString()
            };
        }

        #endregion

        #region Zone Operations

        public static List<Zone> GetAllZones()
        {
            var zones = new List<Zone>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Zones WHERE IsActive = TRUE ORDER BY Code";
                using (var command = new OleDbCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zones.Add(new Zone
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Code = reader["Code"]?.ToString(),
                                Name = reader["Name"]?.ToString(),
                                Abbreviation = reader["Abbreviation"]?.ToString(),
                                Headquarters = reader["Headquarters"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return zones;
        }

        #endregion

        #region Settings Operations

        public static CardSettings GetSettings()
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Settings";
                using (var command = new OleDbCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CardSettings
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                DefaultIssuingAuthority = reader["DefaultIssuingAuthority"]?.ToString(),
                                DefaultIssuingAuthorityDesignation = reader["DefaultIssuingAuthorityDesignation"]?.ToString(),
                                DefaultAuthoritySignaturePath = reader["DefaultAuthoritySignaturePath"]?.ToString(),
                                DefaultZoneCode = reader["DefaultZoneCode"]?.ToString(),
                                DefaultZoneName = reader["DefaultZoneName"]?.ToString(),
                                DefaultUnitCode = reader["DefaultUnitCode"]?.ToString(),
                                DefaultUnitName = reader["DefaultUnitName"]?.ToString(),
                                DefaultValidityYears = reader["DefaultValidityYears"] != DBNull.Value ? Convert.ToInt32(reader["DefaultValidityYears"]) : 5,
                                LastSerialNumber = reader["LastSerialNumber"] != DBNull.Value ? Convert.ToInt32(reader["LastSerialNumber"]) : 0,
                                DefaultPrinterName = reader["DefaultPrinterName"]?.ToString(),
                                PrintFrontAndBack = reader["PrintFrontAndBack"] != DBNull.Value && Convert.ToBoolean(reader["PrintFrontAndBack"]),
                                UseDuplexPrinting = reader["UseDuplexPrinting"] != DBNull.Value && Convert.ToBoolean(reader["UseDuplexPrinting"]),
                                LogoPath = reader["LogoPath"]?.ToString(),
                                OrganizationName = reader["OrganizationName"]?.ToString() ?? "Indian Railways"
                            };
                        }
                    }
                }
            }
            return new CardSettings();
        }

        public static void SaveSettings(CardSettings settings)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = @"UPDATE Settings SET 
                    DefaultIssuingAuthority = ?, DefaultIssuingAuthorityDesignation = ?,
                    DefaultAuthoritySignaturePath = ?, DefaultZoneCode = ?, DefaultZoneName = ?,
                    DefaultUnitCode = ?, DefaultUnitName = ?, DefaultValidityYears = ?,
                    LastSerialNumber = ?, DefaultPrinterName = ?, PrintFrontAndBack = ?,
                    UseDuplexPrinting = ?, LogoPath = ?, OrganizationName = ?, LastUpdated = ?
                    WHERE Id = ?";

                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", settings.DefaultIssuingAuthority ?? "");
                    command.Parameters.AddWithValue("?", settings.DefaultIssuingAuthorityDesignation ?? "");
                    command.Parameters.AddWithValue("?", settings.DefaultAuthoritySignaturePath ?? "");
                    command.Parameters.AddWithValue("?", settings.DefaultZoneCode ?? "");
                    command.Parameters.AddWithValue("?", settings.DefaultZoneName ?? "");
                    command.Parameters.AddWithValue("?", settings.DefaultUnitCode ?? "");
                    command.Parameters.AddWithValue("?", settings.DefaultUnitName ?? "");
                    command.Parameters.AddWithValue("?", settings.DefaultValidityYears);
                    command.Parameters.AddWithValue("?", settings.LastSerialNumber);
                    command.Parameters.AddWithValue("?", settings.DefaultPrinterName ?? "");
                    command.Parameters.AddWithValue("?", settings.PrintFrontAndBack);
                    command.Parameters.AddWithValue("?", settings.UseDuplexPrinting);
                    command.Parameters.AddWithValue("?", settings.LogoPath ?? "");
                    command.Parameters.AddWithValue("?", settings.OrganizationName ?? "Indian Railways");
                    command.Parameters.AddWithValue("?", DateTime.Now);
                    command.Parameters.AddWithValue("?", settings.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static int GetNextSerialNumber()
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT LastSerialNumber FROM Settings";
                using (var command = new OleDbCommand(sql, connection))
                {
                    int lastSerial = Convert.ToInt32(command.ExecuteScalar() ?? 0);
                    int nextSerial = lastSerial + 1;

                    // Update the serial number
                    string updateSql = "UPDATE Settings SET LastSerialNumber = ?";
                    using (var updateCommand = new OleDbCommand(updateSql, connection))
                    {
                        updateCommand.Parameters.AddWithValue("?", nextSerial);
                        updateCommand.ExecuteNonQuery();
                    }

                    return nextSerial;
                }
            }
        }

        #endregion

        #region Print Log Operations

        public static void LogPrint(int employeeId, string idCardNumber, string printedBy, string printType)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO PrintLog (EmployeeId, IDCardNumber, PrintedDate, PrintedBy, PrintType) VALUES (?, ?, ?, ?, ?)";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", employeeId);
                    command.Parameters.AddWithValue("?", idCardNumber ?? "");
                    command.Parameters.AddWithValue("?", DateTime.Now);
                    command.Parameters.AddWithValue("?", printedBy ?? "");
                    command.Parameters.AddWithValue("?", printType ?? "");
                    command.ExecuteNonQuery();
                }

                // Update employee print count
                string updateSql = "UPDATE Employees SET IsCardPrinted = TRUE, LastPrintedDate = ?, PrintCount = PrintCount + 1 WHERE Id = ?";
                using (var updateCommand = new OleDbCommand(updateSql, connection))
                {
                    updateCommand.Parameters.AddWithValue("?", DateTime.Now);
                    updateCommand.Parameters.AddWithValue("?", employeeId);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Backup Operations

        public static void BackupDatabase(string backupPath)
        {
            if (File.Exists(DatabasePath))
            {
                File.Copy(DatabasePath, backupPath, true);
            }
        }

        public static void RestoreDatabase(string backupPath)
        {
            if (File.Exists(backupPath))
            {
                // Close all connections first
                _connectionString = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                File.Copy(backupPath, DatabasePath, true);
            }
        }

        #endregion
    }
}
