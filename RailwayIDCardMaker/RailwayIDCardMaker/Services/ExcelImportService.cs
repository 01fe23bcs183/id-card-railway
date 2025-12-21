using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using RailwayIDCardMaker.Models;

namespace RailwayIDCardMaker.Services
{
    /// <summary>
    /// Service for importing employee data from Excel files
    /// </summary>
    public class ExcelImportService
    {
        /// <summary>
        /// Import employees from Excel file (.xls or .xlsx)
        /// </summary>
        public static List<Employee> ImportFromExcel(string filePath)
        {
            List<Employee> employees = new List<Employee>();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Excel file not found", filePath);
            }

            string extension = Path.GetExtension(filePath).ToLower();
            string connectionString = GetConnectionString(filePath, extension);

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Get the first worksheet
                DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString();

                string query = $"SELECT * FROM [{sheetName}]";

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            Employee employee = new Employee
                            {
                                Name = GetStringValue(row, "Name"),
                                FatherName = GetStringValue(row, "FatherName"),
                                Designation = GetStringValue(row, "Designation"),
                                Department = GetStringValue(row, "Department"),
                                ZoneName = GetStringValue(row, "Zone"),
                                UnitCode = GetStringValue(row, "UnitCode"),
                                BloodGroup = GetStringValue(row, "BloodGroup"),
                                Gender = GetStringValue(row, "Gender"),
                                MobileNumber = GetStringValue(row, "Mobile"),
                                AadhaarNumber = GetStringValue(row, "AadhaarNumber"),
                                Address = GetStringValue(row, "Address"),
                                IssuingAuthority = GetStringValue(row, "IssuingAuthority"),
                                DateOfBirth = GetDateValue(row, "DateOfBirth"),
                                DateOfJoining = GetDateValue(row, "DateOfJoining"),
                                DateOfRetirement = GetDateValue(row, "DateOfRetirement"),
                                DateOfIssue = GetDateValue(row, "DateOfIssue") ?? DateTime.Now,
                                ValidityDate = GetDateValue(row, "ValidityDate")
                            };

                            // Skip rows without required data
                            if (string.IsNullOrWhiteSpace(employee.Name) ||
                                string.IsNullOrWhiteSpace(employee.Designation))
                            {
                                continue;
                            }

                            employees.Add(employee);
                        }
                        catch (Exception ex)
                        {
                            // Log error but continue with other rows
                            System.Diagnostics.Debug.WriteLine($"Error importing row: {ex.Message}");
                        }
                    }
                }
            }

            return employees;
        }

        /// <summary>
        /// Get connection string based on Excel file extension
        /// </summary>
        private static string GetConnectionString(string filePath, string extension)
        {
            string connectionString = "";

            if (extension == ".xls")
            {
                // Excel 2003
                connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            }
            else if (extension == ".xlsx")
            {
                // Excel 2007+
                connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            }
            else
            {
                throw new NotSupportedException("File format not supported. Please use .xls or .xlsx files.");
            }

            return connectionString;
        }

        /// <summary>
        /// Safely get string value from DataRow
        /// </summary>
        private static string GetStringValue(DataRow row, string columnName)
        {
            try
            {
                if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
                {
                    return row[columnName].ToString().Trim();
                }
            }
            catch { }

            return string.Empty;
        }

        /// <summary>
        /// Safely get DateTime value from DataRow
        /// </summary>
        private static DateTime? GetDateValue(DataRow row, string columnName)
        {
            try
            {
                if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
                {
                    if (row[columnName] is DateTime)
                    {
                        return (DateTime)row[columnName];
                    }

                    DateTime result;
                    if (DateTime.TryParse(row[columnName].ToString(), out result))
                    {
                        return result;
                    }
                }
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Export employee template to Excel
        /// </summary>
        public static void ExportTemplate(string filePath)
        {
            // Create a simple CSV template since we can't easily create .xlsx without additional libraries
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("Name,FatherName,Designation,Department,Zone,UnitCode,BloodGroup,Gender,Mobile,AadhaarNumber,Address,IssuingAuthority,DateOfBirth,DateOfJoining,DateOfRetirement,DateOfIssue,ValidityDate");

                // Write sample row
                writer.WriteLine("John Doe,Father Name,Station Master,Operations,CR,01,A+,Male,9876543210,1234-5678-9012,Sample Address,DRM/Mumbai,01/01/1990,01/01/2020,31/12/2050,01/01/2024,31/12/2029");
            }
        }
    }
}
