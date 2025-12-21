using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Utils;

namespace RailwayIDCardMaker.Services
{
    /// <summary>
    /// Database service for SQLite operations
    /// </summary>
    public static class DatabaseService
    {
        private static string _connectionString;

        /// <summary>
        /// Get database connection string
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    string dbPath = Path.Combine(Helpers.GetAppDataDirectory(), Constants.DATABASE_FILENAME);
                    _connectionString = $"Data Source={dbPath};Version=3;BusyTimeout=5000;Journal Mode=WAL;";
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// Initialize database and create tables if not exist
        /// </summary>
        public static void InitializeDatabase()
        {
            string dbPath = Path.Combine(Helpers.GetAppDataDirectory(), Constants.DATABASE_FILENAME);

            // Create database file if not exists
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            // Create tables
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                // Create Users table
                ExecuteNonQuery(connection, @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL UNIQUE,
                        PasswordHash TEXT NOT NULL,
                        FullName TEXT,
                        Designation TEXT,
                        Role TEXT DEFAULT 'Operator',
                        IsActive INTEGER DEFAULT 1,
                        CreatedDate TEXT,
                        LastLoginDate TEXT
                    )");

                // Create Employees table
                ExecuteNonQuery(connection, @"
                    CREATE TABLE IF NOT EXISTS Employees (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        IDCardNumber TEXT UNIQUE,
                        Name TEXT NOT NULL,
                        FatherName TEXT,
                        DateOfBirth TEXT,
                        BloodGroup TEXT,
                        Gender TEXT,
                        Address TEXT,
                        MobileNumber TEXT,
                        AadhaarNumber TEXT,
                        Designation TEXT,
                        Department TEXT,
                        PlaceOfPosting TEXT,
                        ZoneCode TEXT,
                        ZoneName TEXT,
                        UnitCode TEXT,
                        UnitName TEXT,
                        PFNumber TEXT,
                        DateOfJoining TEXT,
                        DateOfRetirement TEXT,
                        DateOfIssue TEXT,
                        ValidityDate TEXT,
                        IssuingAuthority TEXT,
                        IssuingAuthorityDesignation TEXT,
                        SerialNumber INTEGER,
                        PhotoPath TEXT,
                        SignaturePath TEXT,
                        AuthoritySignaturePath TEXT,
                        IsActive INTEGER DEFAULT 1,
                        IsCardPrinted INTEGER DEFAULT 0,
                        LastPrintedDate TEXT,
                        PrintCount INTEGER DEFAULT 0,
                        CreatedDate TEXT,
                        ModifiedDate TEXT,
                        CreatedBy TEXT,
                        ModifiedBy TEXT,
                        Remarks TEXT
                    )");

                // Create Zones table
                ExecuteNonQuery(connection, @"
                    CREATE TABLE IF NOT EXISTS Zones (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Code TEXT NOT NULL UNIQUE,
                        Name TEXT NOT NULL,
                        Abbreviation TEXT,
                        Headquarters TEXT,
                        IsActive INTEGER DEFAULT 1
                    )");

                // Create Units table
                ExecuteNonQuery(connection, @"
                    CREATE TABLE IF NOT EXISTS Units (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Code TEXT NOT NULL,
                        Name TEXT NOT NULL,
                        ZoneCode TEXT,
                        IsActive INTEGER DEFAULT 1,
                        UNIQUE(Code, ZoneCode)
                    )");

                // Create Settings table
                ExecuteNonQuery(connection, @"
                    CREATE TABLE IF NOT EXISTS Settings (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DefaultIssuingAuthority TEXT,
                        DefaultIssuingAuthorityDesignation TEXT,
                        DefaultAuthoritySignaturePath TEXT,
                        DefaultZoneCode TEXT,
                        DefaultZoneName TEXT,
                        DefaultUnitCode TEXT,
                        DefaultUnitName TEXT,
                        DefaultValidityYears INTEGER DEFAULT 5,
                        LastSerialNumber INTEGER DEFAULT 0,
                        DefaultPrinterName TEXT,
                        PrintFrontAndBack INTEGER DEFAULT 1,
                        UseDuplexPrinting INTEGER DEFAULT 0,
                        LogoPath TEXT,
                        OrganizationName TEXT DEFAULT 'Indian Railways',
                        LastUpdated TEXT
                    )");

                // Create PrintLog table
                ExecuteNonQuery(connection, @"
                    CREATE TABLE IF NOT EXISTS PrintLog (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        EmployeeId INTEGER,
                        IDCardNumber TEXT,
                        PrintedDate TEXT,
                        PrintedBy TEXT,
                        PrintType TEXT,
                        FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
                    )");

                // Insert default admin user if not exists
                InsertDefaultUser(connection);

                // Insert default settings if not exists
                InsertDefaultSettings(connection);

                // Insert default zones if not exists
                InsertDefaultZones(connection);
            }
        }

        private static void ExecuteNonQuery(SQLiteConnection connection, string sql)
        {
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void InsertDefaultUser(SQLiteConnection connection)
        {
            string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = @username";
            using (var command = new SQLiteCommand(checkSql, connection))
            {
                command.Parameters.AddWithValue("@username", Constants.DEFAULT_USERNAME);
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                {
                    string insertSql = @"INSERT INTO Users (Username, PasswordHash, FullName, Designation, Role, IsActive, CreatedDate)
                                        VALUES (@username, @password, @fullname, @designation, @role, 1, @created)";
                    using (var insertCommand = new SQLiteCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@username", Constants.DEFAULT_USERNAME);
                        insertCommand.Parameters.AddWithValue("@password", Helpers.HashPassword(Constants.DEFAULT_PASSWORD));
                        insertCommand.Parameters.AddWithValue("@fullname", "Administrator");
                        insertCommand.Parameters.AddWithValue("@designation", "System Admin");
                        insertCommand.Parameters.AddWithValue("@role", UserRoles.Admin);
                        insertCommand.Parameters.AddWithValue("@created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void InsertDefaultSettings(SQLiteConnection connection)
        {
            string checkSql = "SELECT COUNT(*) FROM Settings";
            using (var command = new SQLiteCommand(checkSql, connection))
            {
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                {
                    string insertSql = @"INSERT INTO Settings (DefaultValidityYears, LastSerialNumber, PrintFrontAndBack, OrganizationName)
                                        VALUES (5, 0, 1, 'Indian Railways')";
                    using (var insertCommand = new SQLiteCommand(insertSql, connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void InsertDefaultZones(SQLiteConnection connection)
        {
            string checkSql = "SELECT COUNT(*) FROM Zones";
            using (var command = new SQLiteCommand(checkSql, connection))
            {
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                {
                    foreach (var zone in Zone.GetAllZones())
                    {
                        string insertSql = @"INSERT INTO Zones (Code, Name, Abbreviation, Headquarters, IsActive)
                                            VALUES (@code, @name, @abbr, @hq, 1)";
                        using (var insertCommand = new SQLiteCommand(insertSql, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@code", zone.Code);
                            insertCommand.Parameters.AddWithValue("@name", zone.Name);
                            insertCommand.Parameters.AddWithValue("@abbr", zone.Abbreviation);
                            insertCommand.Parameters.AddWithValue("@hq", zone.Headquarters);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        #region User Operations

        /// <summary>
        /// Validate user login
        /// </summary>
        public static User ValidateUser(string username, string password)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"SELECT * FROM Users WHERE Username = @username AND IsActive = 1";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = MapReaderToUser(reader);
                            if (user.VerifyPassword(password))
                            {
                                // Update last login date
                                UpdateLastLogin(user.Id);
                                return user;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private static void UpdateLastLogin(int userId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string sql = "UPDATE Users SET LastLoginDate = @date WHERE Id = @id";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@id", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private static User MapReaderToUser(SQLiteDataReader reader)
        {
            return new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"]?.ToString(),
                PasswordHash = reader["PasswordHash"]?.ToString(),
                FullName = reader["FullName"]?.ToString(),
                Designation = reader["Designation"]?.ToString(),
                Role = reader["Role"]?.ToString(),
                IsActive = Convert.ToInt32(reader["IsActive"]) == 1,
                CreatedDate = DateTime.Parse(reader["CreatedDate"]?.ToString() ?? DateTime.Now.ToString()),
                LastLoginDate = string.IsNullOrEmpty(reader["LastLoginDate"]?.ToString()) ?
                    (DateTime?)null : DateTime.Parse(reader["LastLoginDate"].ToString())
            };
        }

        /// <summary>
        /// Authenticate user with username and password
        /// </summary>
        public static User AuthenticateUser(string username, string password)
        {
            return ValidateUser(username, password);
        }

        /// <summary>
        /// Update user password
        /// </summary>
        public static void UpdateUserPassword(int userId, string newPassword)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = "UPDATE Users SET PasswordHash = @password WHERE Id = @id";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@password", Helpers.HashPassword(newPassword));
                    command.Parameters.AddWithValue("@id", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Employee Operations

        /// <summary>
        /// Save employee (insert or update)
        /// </summary>
        public static int SaveEmployee(Employee employee)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                if (employee.Id == 0)
                {
                    // Insert new employee
                    return InsertEmployee(connection, employee);
                }
                else
                {
                    // Update existing employee
                    UpdateEmployee(connection, employee);
                    return employee.Id;
                }
            }
        }

        private static int InsertEmployee(SQLiteConnection connection, Employee emp)
        {
            string sql = @"INSERT INTO Employees (
                IDCardNumber, Name, FatherName, DateOfBirth, BloodGroup, Gender,
                Address, MobileNumber, AadhaarNumber, Designation, Department,
                PlaceOfPosting, ZoneCode, ZoneName, UnitCode, UnitName, PFNumber,
                DateOfJoining, DateOfRetirement, DateOfIssue, ValidityDate,
                IssuingAuthority, IssuingAuthorityDesignation, SerialNumber,
                PhotoPath, SignaturePath, AuthoritySignaturePath,
                IsActive, IsCardPrinted, PrintCount, CreatedDate, CreatedBy, Remarks
            ) VALUES (
                @idcard, @name, @father, @dob, @blood, @gender,
                @address, @mobile, @aadhaar, @designation, @department,
                @posting, @zonecode, @zonename, @unitcode, @unitname, @pf,
                @joining, @retirement, @issue, @validity,
                @authority, @authoritydesig, @serial,
                @photo, @signature, @authsig,
                @active, @printed, @printcount, @created, @createdby, @remarks
            );
            SELECT last_insert_rowid();";

            using (var command = new SQLiteCommand(sql, connection))
            {
                AddEmployeeParameters(command, emp);
                command.Parameters.AddWithValue("@created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@createdby", emp.CreatedBy ?? "");

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static void UpdateEmployee(SQLiteConnection connection, Employee emp)
        {
            string sql = @"UPDATE Employees SET
                IDCardNumber = @idcard, Name = @name, FatherName = @father,
                DateOfBirth = @dob, BloodGroup = @blood, Gender = @gender,
                Address = @address, MobileNumber = @mobile, AadhaarNumber = @aadhaar,
                Designation = @designation, Department = @department,
                PlaceOfPosting = @posting, ZoneCode = @zonecode, ZoneName = @zonename,
                UnitCode = @unitcode, UnitName = @unitname, PFNumber = @pf,
                DateOfJoining = @joining, DateOfRetirement = @retirement,
                DateOfIssue = @issue, ValidityDate = @validity,
                IssuingAuthority = @authority, IssuingAuthorityDesignation = @authoritydesig,
                SerialNumber = @serial, PhotoPath = @photo, SignaturePath = @signature,
                AuthoritySignaturePath = @authsig, IsActive = @active,
                IsCardPrinted = @printed, PrintCount = @printcount,
                ModifiedDate = @modified, ModifiedBy = @modifiedby, Remarks = @remarks
                WHERE Id = @id";

            using (var command = new SQLiteCommand(sql, connection))
            {
                AddEmployeeParameters(command, emp);
                command.Parameters.AddWithValue("@id", emp.Id);
                command.Parameters.AddWithValue("@modified", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@modifiedby", emp.ModifiedBy ?? "");

                command.ExecuteNonQuery();
            }
        }

        private static void AddEmployeeParameters(SQLiteCommand command, Employee emp)
        {
            command.Parameters.AddWithValue("@idcard", emp.IDCardNumber ?? "");
            command.Parameters.AddWithValue("@name", emp.Name ?? "");
            command.Parameters.AddWithValue("@father", emp.FatherName ?? "");
            command.Parameters.AddWithValue("@dob", emp.DateOfBirth?.ToString("yyyy-MM-dd") ?? "");
            command.Parameters.AddWithValue("@blood", emp.BloodGroup ?? "");
            command.Parameters.AddWithValue("@gender", emp.Gender ?? "");
            command.Parameters.AddWithValue("@address", emp.Address ?? "");
            command.Parameters.AddWithValue("@mobile", emp.MobileNumber ?? "");
            command.Parameters.AddWithValue("@aadhaar", emp.AadhaarNumber ?? "");
            command.Parameters.AddWithValue("@designation", emp.Designation ?? "");
            command.Parameters.AddWithValue("@department", emp.Department ?? "");
            command.Parameters.AddWithValue("@posting", emp.PlaceOfPosting ?? "");
            command.Parameters.AddWithValue("@zonecode", emp.ZoneCode ?? "");
            command.Parameters.AddWithValue("@zonename", emp.ZoneName ?? "");
            command.Parameters.AddWithValue("@unitcode", emp.UnitCode ?? "");
            command.Parameters.AddWithValue("@unitname", emp.UnitName ?? "");
            command.Parameters.AddWithValue("@pf", emp.PFNumber ?? "");
            command.Parameters.AddWithValue("@joining", emp.DateOfJoining?.ToString("yyyy-MM-dd") ?? "");
            command.Parameters.AddWithValue("@retirement", emp.DateOfRetirement?.ToString("yyyy-MM-dd") ?? "");
            command.Parameters.AddWithValue("@issue", emp.DateOfIssue?.ToString("yyyy-MM-dd") ?? "");
            command.Parameters.AddWithValue("@validity", emp.ValidityDate?.ToString("yyyy-MM-dd") ?? "");
            command.Parameters.AddWithValue("@authority", emp.IssuingAuthority ?? "");
            command.Parameters.AddWithValue("@authoritydesig", emp.IssuingAuthorityDesignation ?? "");
            command.Parameters.AddWithValue("@serial", emp.SerialNumber);
            command.Parameters.AddWithValue("@photo", emp.PhotoPath ?? "");
            command.Parameters.AddWithValue("@signature", emp.SignaturePath ?? "");
            command.Parameters.AddWithValue("@authsig", emp.AuthoritySignaturePath ?? "");
            command.Parameters.AddWithValue("@active", emp.IsActive ? 1 : 0);
            command.Parameters.AddWithValue("@printed", emp.IsCardPrinted ? 1 : 0);
            command.Parameters.AddWithValue("@printcount", emp.PrintCount);
            command.Parameters.AddWithValue("@remarks", emp.Remarks ?? "");
        }

        /// <summary>
        /// Get employee by ID
        /// </summary>
        public static Employee GetEmployee(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Employees WHERE Id = @id";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToEmployee(reader);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        public static List<Employee> GetAllEmployees(bool activeOnly = true)
        {
            var employees = new List<Employee>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = activeOnly ?
                    "SELECT * FROM Employees WHERE IsActive = 1 ORDER BY Name" :
                    "SELECT * FROM Employees ORDER BY Name";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(MapReaderToEmployee(reader));
                        }
                    }
                }
            }

            return employees;
        }

        /// <summary>
        /// Search employees by name, ID, or PF number
        /// </summary>
        public static List<Employee> SearchEmployees(string searchTerm)
        {
            var employees = new List<Employee>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"SELECT * FROM Employees 
                              WHERE IsActive = 1 AND (
                                  Name LIKE @search OR 
                                  IDCardNumber LIKE @search OR 
                                  PFNumber LIKE @search OR
                                  AadhaarNumber LIKE @search
                              ) ORDER BY Name";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@search", $"%{searchTerm}%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(MapReaderToEmployee(reader));
                        }
                    }
                }
            }

            return employees;
        }

        /// <summary>
        /// Delete employee (soft delete)
        /// </summary>
        public static void DeleteEmployee(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string sql = "UPDATE Employees SET IsActive = 0 WHERE Id = @id";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private static Employee MapReaderToEmployee(SQLiteDataReader reader)
        {
            var emp = new Employee
            {
                Id = Convert.ToInt32(reader["Id"]),
                IDCardNumber = reader["IDCardNumber"]?.ToString(),
                Name = reader["Name"]?.ToString(),
                FatherName = reader["FatherName"]?.ToString(),
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
                IssuingAuthority = reader["IssuingAuthority"]?.ToString(),
                IssuingAuthorityDesignation = reader["IssuingAuthorityDesignation"]?.ToString(),
                SerialNumber = Convert.ToInt32(reader["SerialNumber"]),
                PhotoPath = reader["PhotoPath"]?.ToString(),
                SignaturePath = reader["SignaturePath"]?.ToString(),
                AuthoritySignaturePath = reader["AuthoritySignaturePath"]?.ToString(),
                IsActive = Convert.ToInt32(reader["IsActive"]) == 1,
                IsCardPrinted = Convert.ToInt32(reader["IsCardPrinted"]) == 1,
                PrintCount = Convert.ToInt32(reader["PrintCount"]),
                Remarks = reader["Remarks"]?.ToString()
            };

            // Parse dates
            DateTime tempDate;
            if (DateTime.TryParse(reader["DateOfBirth"]?.ToString(), out tempDate))
                emp.DateOfBirth = tempDate;
            if (DateTime.TryParse(reader["DateOfJoining"]?.ToString(), out tempDate))
                emp.DateOfJoining = tempDate;
            if (DateTime.TryParse(reader["DateOfRetirement"]?.ToString(), out tempDate))
                emp.DateOfRetirement = tempDate;
            if (DateTime.TryParse(reader["DateOfIssue"]?.ToString(), out tempDate))
                emp.DateOfIssue = tempDate;
            if (DateTime.TryParse(reader["ValidityDate"]?.ToString(), out tempDate))
                emp.ValidityDate = tempDate;
            if (DateTime.TryParse(reader["CreatedDate"]?.ToString(), out tempDate))
                emp.CreatedDate = tempDate;
            if (DateTime.TryParse(reader["ModifiedDate"]?.ToString(), out tempDate))
                emp.ModifiedDate = tempDate;
            if (DateTime.TryParse(reader["LastPrintedDate"]?.ToString(), out tempDate))
                emp.LastPrintedDate = tempDate;

            emp.CreatedBy = reader["CreatedBy"]?.ToString();
            emp.ModifiedBy = reader["ModifiedBy"]?.ToString();

            return emp;
        }

        #endregion

        #region Zone Operations

        /// <summary>
        /// Get all zones
        /// </summary>
        public static List<Zone> GetAllZones()
        {
            var zones = new List<Zone>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Zones WHERE IsActive = 1 ORDER BY Code";
                using (var command = new SQLiteCommand(sql, connection))
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
                                IsActive = Convert.ToInt32(reader["IsActive"]) == 1
                            });
                        }
                    }
                }
            }

            return zones;
        }

        #endregion

        #region Settings Operations

        /// <summary>
        /// Get card settings
        /// </summary>
        public static CardSettings GetSettings()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Settings LIMIT 1";
                using (var command = new SQLiteCommand(sql, connection))
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
                                DefaultValidityYears = Convert.ToInt32(reader["DefaultValidityYears"]),
                                LastSerialNumber = Convert.ToInt32(reader["LastSerialNumber"]),
                                DefaultPrinterName = reader["DefaultPrinterName"]?.ToString(),
                                PrintFrontAndBack = Convert.ToInt32(reader["PrintFrontAndBack"]) == 1,
                                UseDuplexPrinting = Convert.ToInt32(reader["UseDuplexPrinting"]) == 1,
                                LogoPath = reader["LogoPath"]?.ToString(),
                                OrganizationName = reader["OrganizationName"]?.ToString()
                            };
                        }
                    }
                }
            }

            return new CardSettings();
        }

        /// <summary>
        /// Save card settings
        /// </summary>
        public static void SaveSettings(CardSettings settings)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"UPDATE Settings SET
                    DefaultIssuingAuthority = @authority,
                    DefaultIssuingAuthorityDesignation = @authoritydesig,
                    DefaultAuthoritySignaturePath = @authsig,
                    DefaultZoneCode = @zonecode,
                    DefaultZoneName = @zonename,
                    DefaultUnitCode = @unitcode,
                    DefaultUnitName = @unitname,
                    DefaultValidityYears = @validity,
                    LastSerialNumber = @serial,
                    DefaultPrinterName = @printer,
                    PrintFrontAndBack = @printboth,
                    UseDuplexPrinting = @duplex,
                    LogoPath = @logo,
                    OrganizationName = @org,
                    LastUpdated = @updated
                    WHERE Id = @id";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", settings.Id);
                    command.Parameters.AddWithValue("@authority", settings.DefaultIssuingAuthority ?? "");
                    command.Parameters.AddWithValue("@authoritydesig", settings.DefaultIssuingAuthorityDesignation ?? "");
                    command.Parameters.AddWithValue("@authsig", settings.DefaultAuthoritySignaturePath ?? "");
                    command.Parameters.AddWithValue("@zonecode", settings.DefaultZoneCode ?? "");
                    command.Parameters.AddWithValue("@zonename", settings.DefaultZoneName ?? "");
                    command.Parameters.AddWithValue("@unitcode", settings.DefaultUnitCode ?? "");
                    command.Parameters.AddWithValue("@unitname", settings.DefaultUnitName ?? "");
                    command.Parameters.AddWithValue("@validity", settings.DefaultValidityYears);
                    command.Parameters.AddWithValue("@serial", settings.LastSerialNumber);
                    command.Parameters.AddWithValue("@printer", settings.DefaultPrinterName ?? "");
                    command.Parameters.AddWithValue("@printboth", settings.PrintFrontAndBack ? 1 : 0);
                    command.Parameters.AddWithValue("@duplex", settings.UseDuplexPrinting ? 1 : 0);
                    command.Parameters.AddWithValue("@logo", settings.LogoPath ?? "");
                    command.Parameters.AddWithValue("@org", settings.OrganizationName ?? "Indian Railways");
                    command.Parameters.AddWithValue("@updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get next serial number for ID card
        /// </summary>
        public static int GetNextSerialNumber()
        {
            var settings = GetSettings();
            settings.LastSerialNumber++;
            SaveSettings(settings);
            return settings.LastSerialNumber;
        }

        #endregion
    }
}
