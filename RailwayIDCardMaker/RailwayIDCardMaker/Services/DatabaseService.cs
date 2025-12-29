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
    /// Database service for Microsoft Access operations - Simplified version without boolean columns
    /// </summary>
    public static class DatabaseService
    {
        private static string _connectionString;
        private static string _dbPath;

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

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={DatabasePath};";
                }
                return _connectionString;
            }
        }

        public static void InitializeDatabase()
        {
            if (!File.Exists(DatabasePath))
            {
                CreateAccessDatabase();
            }

            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                CreateTables(connection);
                MigrateDatabase(connection); // Add missing columns to existing tables
                InsertDefaultUser(connection);
                InsertDefaultSettings(connection);
                InsertDefaultZones(connection);
            }
        }

        private static void CreateAccessDatabase()
        {
            string dir = Path.GetDirectoryName(DatabasePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

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
                throw new Exception("Cannot create Access database. ADOX not available.");
            }
        }

        private static bool TableExists(OleDbConnection connection, string tableName)
        {
            var schema = connection.GetSchema("Tables");
            foreach (DataRow row in schema.Rows)
            {
                if (row["TABLE_NAME"].ToString().Equals(tableName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private static void ExecuteNonQuery(OleDbConnection connection, string sql)
        {
            using (var command = new OleDbCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Migrate database by adding missing columns
        /// </summary>
        private static void MigrateDatabase(OleDbConnection connection)
        {
            // Get existing columns in Employees table
            var existingColumns = new List<string>();
            try
            {
                var schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns,
                    new object[] { null, null, "Employees", null });
                if (schema != null)
                {
                    foreach (DataRow row in schema.Rows)
                    {
                        existingColumns.Add(row["COLUMN_NAME"].ToString().ToLower());
                    }
                }
            }
            catch { }

            // Define all expected columns with their types
            var expectedColumns = new Dictionary<string, string>
            {
                { "IDCardNumber", "TEXT(50)" },
                { "Name", "TEXT(100)" },
                { "FatherName", "TEXT(100)" },
                { "DateOfBirth", "DATETIME" },
                { "BloodGroup", "TEXT(10)" },
                { "Gender", "TEXT(10)" },
                { "Address", "MEMO" },
                { "MobileNumber", "TEXT(20)" },
                { "AadhaarNumber", "TEXT(20)" },
                { "Designation", "TEXT(100)" },
                { "Department", "TEXT(100)" },
                { "PlaceOfPosting", "TEXT(100)" },
                { "ZoneCode", "TEXT(10)" },
                { "ZoneName", "TEXT(100)" },
                { "UnitCode", "TEXT(20)" },
                { "UnitName", "TEXT(100)" },
                { "PFNumber", "TEXT(50)" },
                { "DateOfJoining", "DATETIME" },
                { "DateOfRetirement", "DATETIME" },
                { "DateOfIssue", "DATETIME" },
                { "ValidityDate", "DATETIME" },
                { "IssuingAuthority", "TEXT(100)" },
                { "IssuingAuthorityDesignation", "TEXT(100)" },
                { "QRCodeUrl", "TEXT(255)" },
                { "SerialNumber", "LONG" },
                { "PhotoPath", "TEXT(255)" },
                { "SignaturePath", "TEXT(255)" },
                { "AuthoritySignaturePath", "TEXT(255)" },
                { "LastPrintedDate", "DATETIME" },
                { "PrintCount", "LONG" },
                { "CreatedDate", "DATETIME" },
                { "ModifiedDate", "DATETIME" },
                { "CreatedBy", "TEXT(50)" },
                { "ModifiedBy", "TEXT(50)" },
                { "Remarks", "MEMO" }
            };

            // Add missing columns
            foreach (var col in expectedColumns)
            {
                if (!existingColumns.Contains(col.Key.ToLower()))
                {
                    try
                    {
                        string sql = $"ALTER TABLE Employees ADD COLUMN [{col.Key}] {col.Value}";
                        ExecuteNonQuery(connection, sql);
                    }
                    catch
                    {
                        // Column might already exist or other issue, ignore
                    }
                }
            }
        }

        private static void CreateTables(OleDbConnection connection)
        {
            // Users table - NO BOOLEAN COLUMNS
            if (!TableExists(connection, "Users"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Users (
                        Id COUNTER PRIMARY KEY,
                        Username TEXT(50) NOT NULL,
                        PasswordHash TEXT(255) NOT NULL,
                        FullName TEXT(100),
                        Designation TEXT(100),
                        Role TEXT(20),
                        CreatedDate DATETIME,
                        LastLoginDate DATETIME
                    )");
                ExecuteNonQuery(connection, "CREATE UNIQUE INDEX idx_Username ON Users (Username)");
            }

            // Employees table - NO BOOLEAN COLUMNS
            if (!TableExists(connection, "Employees"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Employees (
                        Id COUNTER PRIMARY KEY,
                        IDCardNumber TEXT(50),
                        Name TEXT(100) NOT NULL,
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
                        QRCodeUrl TEXT(255),
                        SerialNumber LONG,
                        PhotoPath TEXT(255),
                        SignaturePath TEXT(255),
                        AuthoritySignaturePath TEXT(255),
                        LastPrintedDate DATETIME,
                        PrintCount LONG,
                        CreatedDate DATETIME,
                        ModifiedDate DATETIME,
                        CreatedBy TEXT(50),
                        ModifiedBy TEXT(50),
                        Remarks MEMO
                    )");
                ExecuteNonQuery(connection, "CREATE UNIQUE INDEX idx_IDCardNumber ON Employees (IDCardNumber)");
            }

            // Zones table - NO BOOLEAN COLUMNS
            if (!TableExists(connection, "Zones"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Zones (
                        Id COUNTER PRIMARY KEY,
                        Code TEXT(10) NOT NULL,
                        Name TEXT(100) NOT NULL,
                        Abbreviation TEXT(20),
                        Headquarters TEXT(100)
                    )");
                ExecuteNonQuery(connection, "CREATE UNIQUE INDEX idx_ZoneCode ON Zones (Code)");
            }

            // Units table
            if (!TableExists(connection, "Units"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Units (
                        Id COUNTER PRIMARY KEY,
                        Code TEXT(10) NOT NULL,
                        Name TEXT(100) NOT NULL,
                        ZoneCode TEXT(10)
                    )");
            }

            // Settings table - NO BOOLEAN COLUMNS
            if (!TableExists(connection, "Settings"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE Settings (
                        Id COUNTER PRIMARY KEY,
                        DefaultZoneCode TEXT(10),
                        DefaultZoneName TEXT(100),
                        DefaultUnitCode TEXT(10),
                        DefaultUnitName TEXT(100),
                        DefaultValidityYears LONG,
                        LastSerialNumber LONG,
                        DefaultPrinterName TEXT(100),
                        LogoPath TEXT(255),
                        OrganizationName TEXT(100),
                        LastUpdated DATETIME
                    )");
            }

            // PrintLog table
            if (!TableExists(connection, "PrintLog"))
            {
                ExecuteNonQuery(connection, @"
                    CREATE TABLE PrintLog (
                        Id COUNTER PRIMARY KEY,
                        EmployeeId LONG,
                        IDCardNumber TEXT(50),
                        PrintedDate DATETIME,
                        PrintedBy TEXT(50),
                        PrintType TEXT(20)
                    )");
            }
        }

        private static void InsertDefaultUser(OleDbConnection connection)
        {
            string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = ?";
            using (var command = new OleDbCommand(checkSql, connection))
            {
                command.Parameters.Add("?", OleDbType.VarChar, 50).Value = Constants.DEFAULT_USERNAME;
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                {
                    string insertSql = "INSERT INTO Users (Username, PasswordHash, FullName, Designation, Role, CreatedDate) VALUES (?, ?, ?, ?, ?, ?)";
                    using (var insertCommand = new OleDbCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("?", OleDbType.VarChar, 50).Value = "admin";
                        insertCommand.Parameters.Add("?", OleDbType.VarChar, 255).Value = Helpers.HashPassword("admin123");
                        insertCommand.Parameters.Add("?", OleDbType.VarChar, 100).Value = "Administrator";
                        insertCommand.Parameters.Add("?", OleDbType.VarChar, 100).Value = "System Admin";
                        insertCommand.Parameters.Add("?", OleDbType.VarChar, 20).Value = "Admin";
                        insertCommand.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
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
                    string insertSql = "INSERT INTO Settings (OrganizationName, DefaultValidityYears, LastSerialNumber, LastUpdated) VALUES (?, ?, ?, ?)";
                    using (var insertCommand = new OleDbCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("?", OleDbType.VarChar, 100).Value = "Indian Railways";
                        insertCommand.Parameters.Add("?", OleDbType.Integer).Value = 5;
                        insertCommand.Parameters.Add("?", OleDbType.Integer).Value = 0;
                        insertCommand.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
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
                        ("1", "Central Railway", "CR", "Mumbai"),
                        ("2", "Eastern Railway", "ER", "Kolkata"),
                        ("3", "East Central Railway", "ECR", "Hajipur"),
                        ("4", "East Coast Railway", "ECoR", "Bhubaneswar"),
                        ("5", "Northern Railway", "NR", "New Delhi"),
                        ("6", "North Central Railway", "NCR", "Prayagraj"),
                        ("7", "North Eastern Railway", "NER", "Gorakhpur"),
                        ("8", "Northeast Frontier Railway", "NFR", "Guwahati"),
                        ("9", "North Western Railway", "NWR", "Jaipur"),
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
                        string insertSql = "INSERT INTO Zones (Code, Name, Abbreviation, Headquarters) VALUES (?, ?, ?, ?)";
                        using (var insertCommand = new OleDbCommand(insertSql, connection))
                        {
                            insertCommand.Parameters.Add("?", OleDbType.VarChar, 10).Value = zone.Item1;
                            insertCommand.Parameters.Add("?", OleDbType.VarChar, 100).Value = zone.Item2;
                            insertCommand.Parameters.Add("?", OleDbType.VarChar, 20).Value = zone.Item3;
                            insertCommand.Parameters.Add("?", OleDbType.VarChar, 100).Value = zone.Item4;
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
                string sql = "SELECT * FROM Users WHERE Username = ? AND PasswordHash = ?";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.Add("?", OleDbType.VarChar, 50).Value = username;
                    command.Parameters.Add("?", OleDbType.VarChar, 255).Value = passwordHash;

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
                    command.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
                    command.Parameters.Add("?", OleDbType.Integer).Value = userId;
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
                    command.Parameters.Add("?", OleDbType.VarChar, 255).Value = newPasswordHash;
                    command.Parameters.Add("?", OleDbType.Integer).Value = userId;
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static void UpdateUserPassword(int userId, string newPassword)
        {
            string passwordHash = Helpers.HashPassword(newPassword);
            ChangePassword(userId, passwordHash);
        }

        public static User ValidateUser(string username, string password)
        {
            string passwordHash = Helpers.HashPassword(password);
            return AuthenticateUser(username, passwordHash);
        }

        public static Employee GetEmployee(int id)
        {
            return GetEmployeeById(id);
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
                IsActive = true, // Always active since we removed the column
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
                string sql = "SELECT * FROM Employees ORDER BY Name";
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
                    command.Parameters.Add("?", OleDbType.Integer).Value = id;
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
                        DateOfIssue, ValidityDate, IssuingAuthority, IssuingAuthorityDesignation, QRCodeUrl, SerialNumber,
                        PhotoPath, SignaturePath, AuthoritySignaturePath, PrintCount, CreatedDate, CreatedBy, Remarks)
                        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    using (var command = new OleDbCommand(sql, connection))
                    {
                        AddEmployeeParameters(command, employee);
                        command.Parameters.Add("?", OleDbType.Integer).Value = 0; // PrintCount
                        command.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
                        command.Parameters.Add("?", OleDbType.VarChar, 50).Value = employee.CreatedBy ?? "";
                        command.Parameters.Add("?", OleDbType.VarChar, 255).Value = employee.Remarks ?? "";
                        command.ExecuteNonQuery();

                        // Get the new ID
                        command.CommandText = "SELECT @@IDENTITY";
                        command.Parameters.Clear();
                        var result = command.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
                else
                {
                    // Update existing employee
                    string sql = @"UPDATE Employees SET 
                        IDCardNumber = ?, Name = ?, FatherName = ?, DateOfBirth = ?, BloodGroup = ?, Gender = ?,
                        Address = ?, MobileNumber = ?, AadhaarNumber = ?, Designation = ?, Department = ?, PlaceOfPosting = ?,
                        ZoneCode = ?, ZoneName = ?, UnitCode = ?, UnitName = ?, PFNumber = ?, DateOfJoining = ?, DateOfRetirement = ?,
                        DateOfIssue = ?, ValidityDate = ?, IssuingAuthority = ?, IssuingAuthorityDesignation = ?, QRCodeUrl = ?, SerialNumber = ?,
                        PhotoPath = ?, SignaturePath = ?, AuthoritySignaturePath = ?, ModifiedDate = ?, ModifiedBy = ?, Remarks = ?
                        WHERE Id = ?";

                    using (var command = new OleDbCommand(sql, connection))
                    {
                        AddEmployeeParameters(command, employee);
                        command.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
                        command.Parameters.Add("?", OleDbType.VarChar, 50).Value = employee.ModifiedBy ?? "";
                        command.Parameters.Add("?", OleDbType.VarChar, 255).Value = employee.Remarks ?? "";
                        command.Parameters.Add("?", OleDbType.Integer).Value = employee.Id;
                        command.ExecuteNonQuery();
                        return employee.Id;
                    }
                }
            }
        }

        private static void AddEmployeeParameters(OleDbCommand command, Employee e)
        {
            command.Parameters.Add("?", OleDbType.VarChar, 50).Value = e.IDCardNumber ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.Name ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.FatherName ?? "";
            command.Parameters.Add("?", OleDbType.Date).Value = e.DateOfBirth.HasValue ? (object)e.DateOfBirth.Value : DBNull.Value;
            command.Parameters.Add("?", OleDbType.VarChar, 10).Value = e.BloodGroup ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 10).Value = e.Gender ?? "";
            command.Parameters.Add("?", OleDbType.LongVarChar).Value = e.Address ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 20).Value = e.MobileNumber ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 20).Value = e.AadhaarNumber ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.Designation ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.Department ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.PlaceOfPosting ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 10).Value = e.ZoneCode ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.ZoneName ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 20).Value = e.UnitCode ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.UnitName ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 50).Value = e.PFNumber ?? "";
            command.Parameters.Add("?", OleDbType.Date).Value = e.DateOfJoining.HasValue ? (object)e.DateOfJoining.Value : DBNull.Value;
            command.Parameters.Add("?", OleDbType.Date).Value = e.DateOfRetirement.HasValue ? (object)e.DateOfRetirement.Value : DBNull.Value;
            command.Parameters.Add("?", OleDbType.Date).Value = e.DateOfIssue.HasValue ? (object)e.DateOfIssue.Value : DBNull.Value;
            command.Parameters.Add("?", OleDbType.Date).Value = e.ValidityDate.HasValue ? (object)e.ValidityDate.Value : DBNull.Value;
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.IssuingAuthority ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 100).Value = e.IssuingAuthorityDesignation ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 255).Value = e.QRCodeUrl ?? "";
            command.Parameters.Add("?", OleDbType.Integer).Value = e.SerialNumber;
            command.Parameters.Add("?", OleDbType.VarChar, 255).Value = e.PhotoPath ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 255).Value = e.SignaturePath ?? "";
            command.Parameters.Add("?", OleDbType.VarChar, 255).Value = e.AuthoritySignaturePath ?? "";
        }

        public static void DeleteEmployee(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Employees WHERE Id = ?";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.Add("?", OleDbType.Integer).Value = id;
                    command.ExecuteNonQuery();
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
                              WHERE (Name LIKE ? OR IDCardNumber LIKE ? OR MobileNumber LIKE ? OR Department LIKE ?)
                              ORDER BY Name";
                using (var command = new OleDbCommand(sql, connection))
                {
                    string pattern = $"%{searchTerm}%";
                    command.Parameters.Add("?", OleDbType.VarChar, 100).Value = pattern;
                    command.Parameters.Add("?", OleDbType.VarChar, 50).Value = pattern;
                    command.Parameters.Add("?", OleDbType.VarChar, 20).Value = pattern;
                    command.Parameters.Add("?", OleDbType.VarChar, 100).Value = pattern;

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
                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateOfBirth"]) : null,
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
                DateOfJoining = reader["DateOfJoining"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateOfJoining"]) : null,
                DateOfRetirement = reader["DateOfRetirement"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateOfRetirement"]) : null,
                DateOfIssue = reader["DateOfIssue"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateOfIssue"]) : null,
                ValidityDate = reader["ValidityDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ValidityDate"]) : null,
                IssuingAuthority = reader["IssuingAuthority"]?.ToString(),
                IssuingAuthorityDesignation = reader["IssuingAuthorityDesignation"]?.ToString(),
                QRCodeUrl = reader["QRCodeUrl"]?.ToString(),
                SerialNumber = reader["SerialNumber"] != DBNull.Value ? Convert.ToInt32(reader["SerialNumber"]) : 0,
                PhotoPath = reader["PhotoPath"]?.ToString(),
                SignaturePath = reader["SignaturePath"]?.ToString(),
                AuthoritySignaturePath = reader["AuthoritySignaturePath"]?.ToString(),
                IsActive = true,
                IsCardPrinted = (reader["PrintCount"] != DBNull.Value && Convert.ToInt32(reader["PrintCount"]) > 0),
                LastPrintedDate = reader["LastPrintedDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["LastPrintedDate"]) : null,
                PrintCount = reader["PrintCount"] != DBNull.Value ? Convert.ToInt32(reader["PrintCount"]) : 0,
                CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.Now,
                ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ModifiedDate"]) : null,
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
                string sql = "SELECT * FROM Zones ORDER BY Code";
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
                                IsActive = true
                            });
                        }
                    }
                }
            }
            return zones;
        }

        public static Zone GetZoneByCode(string code)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Zones WHERE Code = ?";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.Add("?", OleDbType.VarChar, 10).Value = code;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Zone
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Code = reader["Code"]?.ToString(),
                                Name = reader["Name"]?.ToString(),
                                Abbreviation = reader["Abbreviation"]?.ToString(),
                                Headquarters = reader["Headquarters"]?.ToString(),
                                IsActive = true
                            };
                        }
                    }
                }
            }
            return null;
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
                                DefaultZoneCode = reader["DefaultZoneCode"]?.ToString(),
                                DefaultZoneName = reader["DefaultZoneName"]?.ToString(),
                                DefaultUnitCode = reader["DefaultUnitCode"]?.ToString(),
                                DefaultUnitName = reader["DefaultUnitName"]?.ToString(),
                                DefaultValidityYears = reader["DefaultValidityYears"] != DBNull.Value ? Convert.ToInt32(reader["DefaultValidityYears"]) : 5,
                                LastSerialNumber = reader["LastSerialNumber"] != DBNull.Value ? Convert.ToInt32(reader["LastSerialNumber"]) : 0,
                                DefaultPrinterName = reader["DefaultPrinterName"]?.ToString(),
                                PrintFrontAndBack = true,
                                UseDuplexPrinting = false,
                                LogoPath = reader["LogoPath"]?.ToString(),
                                OrganizationName = reader["OrganizationName"]?.ToString() ?? "Indian Railways"
                            };
                        }
                    }
                }
            }
            return new CardSettings { OrganizationName = "Indian Railways", DefaultValidityYears = 5 };
        }

        public static void SaveSettings(CardSettings settings)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();
                string sql = @"UPDATE Settings SET 
                    DefaultZoneCode = ?, DefaultZoneName = ?, DefaultUnitCode = ?, DefaultUnitName = ?,
                    DefaultValidityYears = ?, LastSerialNumber = ?, DefaultPrinterName = ?,
                    LogoPath = ?, OrganizationName = ?, LastUpdated = ?
                    WHERE Id = ?";
                using (var command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.Add("?", OleDbType.VarChar, 10).Value = settings.DefaultZoneCode ?? "";
                    command.Parameters.Add("?", OleDbType.VarChar, 100).Value = settings.DefaultZoneName ?? "";
                    command.Parameters.Add("?", OleDbType.VarChar, 10).Value = settings.DefaultUnitCode ?? "";
                    command.Parameters.Add("?", OleDbType.VarChar, 100).Value = settings.DefaultUnitName ?? "";
                    command.Parameters.Add("?", OleDbType.Integer).Value = settings.DefaultValidityYears;
                    command.Parameters.Add("?", OleDbType.Integer).Value = settings.LastSerialNumber;
                    command.Parameters.Add("?", OleDbType.VarChar, 100).Value = settings.DefaultPrinterName ?? "";
                    command.Parameters.Add("?", OleDbType.VarChar, 255).Value = settings.LogoPath ?? "";
                    command.Parameters.Add("?", OleDbType.VarChar, 100).Value = settings.OrganizationName ?? "";
                    command.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
                    command.Parameters.Add("?", OleDbType.Integer).Value = settings.Id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public static int GetNextSerialNumber()
        {
            var settings = GetSettings();
            settings.LastSerialNumber++;
            SaveSettings(settings);
            return settings.LastSerialNumber;
        }

        #endregion

        #region Print Log Operations

        public static void LogPrint(int employeeId, string idCardNumber, string printedBy, string printType)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open();

                string insertSql = "INSERT INTO PrintLog (EmployeeId, IDCardNumber, PrintedDate, PrintedBy, PrintType) VALUES (?, ?, ?, ?, ?)";
                using (var command = new OleDbCommand(insertSql, connection))
                {
                    command.Parameters.Add("?", OleDbType.Integer).Value = employeeId;
                    command.Parameters.Add("?", OleDbType.VarChar, 50).Value = idCardNumber ?? "";
                    command.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
                    command.Parameters.Add("?", OleDbType.VarChar, 50).Value = printedBy ?? "";
                    command.Parameters.Add("?", OleDbType.VarChar, 20).Value = printType ?? "";
                    command.ExecuteNonQuery();
                }

                // Update employee print count
                string updateSql = "UPDATE Employees SET LastPrintedDate = ?, PrintCount = PrintCount + 1 WHERE Id = ?";
                using (var updateCommand = new OleDbCommand(updateSql, connection))
                {
                    updateCommand.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
                    updateCommand.Parameters.Add("?", OleDbType.Integer).Value = employeeId;
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}
