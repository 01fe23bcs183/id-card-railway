using System;

namespace RailwayIDCardMaker.Models
{
    /// <summary>
    /// User model for authentication
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }  // Admin, Operator, Viewer
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public User()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            Role = "Operator";
        }

        /// <summary>
        /// Verify password against stored hash
        /// </summary>
        public bool VerifyPassword(string password)
        {
            string hash = Utils.Helpers.HashPassword(password);
            return PasswordHash == hash;
        }

        /// <summary>
        /// Set password (stores hash)
        /// </summary>
        public void SetPassword(string password)
        {
            PasswordHash = Utils.Helpers.HashPassword(password);
        }

        public override string ToString()
        {
            return $"{FullName} ({Username})";
        }
    }

    /// <summary>
    /// User roles
    /// </summary>
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Operator = "Operator";
        public const string Viewer = "Viewer";
    }
}
