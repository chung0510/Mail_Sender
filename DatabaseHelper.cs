using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace NetMail
{
    // Helper class for SQLite database operations
    public static class DatabaseHelper
    {
        private static readonly string DbFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.db");
        private static string ConnectionString => $"Data Source={DbFile}";

        public static void EnsureDatabase()
        {
            bool exists = File.Exists(DbFile);
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    // Create users table
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        email TEXT NOT NULL UNIQUE,
                        password_hash TEXT NOT NULL,
                        verification_code TEXT,
                        is_verified INTEGER NOT NULL DEFAULT 0,
                        is_admin INTEGER NOT NULL DEFAULT 0
                    );";
                    cmd.ExecuteNonQuery();
                }

                // Create default admin account if not exists
                CreateDefaultAdmin();
            }
        }

        private static void CreateDefaultAdmin()
        {
            string adminEmail = "admin@netmail.com";
            var existing = GetUserByEmail(adminEmail);
            if (existing == null)
            {
                // Default admin password: Admin123
                string stored = HashHelper.CreateStoredPassword("Admin123");
                using (var con = new SqliteConnection(ConnectionString))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO users (email, password_hash, verification_code, is_verified, is_admin) VALUES (@email, @pwd, NULL, 1, 1);";
                        cmd.Parameters.AddWithValue("@email", adminEmail);
                        cmd.Parameters.AddWithValue("@pwd", stored);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static bool CreateUser(string email, string passwordHashWithSalt, string verificationCode)
        {
            try
            {
                using (var con = new SqliteConnection(ConnectionString))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO users (email, password_hash, verification_code, is_verified, is_admin) VALUES (@email, @pwd, @code, 0, 0);";
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@pwd", passwordHashWithSalt);
                        cmd.Parameters.AddWithValue("@code", verificationCode);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine("CreateUser error: " + ex.Message);
                return false;
            }
        }

        public class UserRecord
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string PasswordHashWithSalt { get; set; }
            public string VerificationCode { get; set; }
            public bool IsVerified { get; set; }
            public bool IsAdmin { get; set; }
        }

        public static UserRecord GetUserByEmail(string email)
        {
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, email, password_hash, verification_code, is_verified, is_admin FROM users WHERE email=@e LIMIT 1;";
                    cmd.Parameters.AddWithValue("@e", email);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return new UserRecord
                            {
                                Id = rdr.GetInt32(0),
                                Email = rdr.GetString(1),
                                PasswordHashWithSalt = rdr.GetString(2),
                                VerificationCode = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                                IsVerified = rdr.GetInt32(4) == 1,
                                IsAdmin = rdr.GetInt32(5) == 1
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static List<UserRecord> GetAllUsers()
        {
            var users = new List<UserRecord>();
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, email, password_hash, verification_code, is_verified, is_admin FROM users ORDER BY id;";
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            users.Add(new UserRecord
                            {
                                Id = rdr.GetInt32(0),
                                Email = rdr.GetString(1),
                                PasswordHashWithSalt = rdr.GetString(2),
                                VerificationCode = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                                IsVerified = rdr.GetInt32(4) == 1,
                                IsAdmin = rdr.GetInt32(5) == 1
                            });
                        }
                    }
                }
            }
            return users;
        }

        public static bool SetVerificationCode(string email, string code)
        {
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE users SET verification_code=@c WHERE email=@e;";
                    cmd.Parameters.AddWithValue("@c", code);
                    cmd.Parameters.AddWithValue("@e", email);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool VerifyUserByCode(string email, string code)
        {
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE users SET is_verified=1, verification_code=NULL WHERE email=@e AND verification_code=@c;";
                    cmd.Parameters.AddWithValue("@e", email);
                    cmd.Parameters.AddWithValue("@c", code);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool UpdatePassword(string email, string newPasswordHashWithSalt)
        {
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE users SET password_hash=@p WHERE email=@e;";
                    cmd.Parameters.AddWithValue("@p", newPasswordHashWithSalt);
                    cmd.Parameters.AddWithValue("@e", email);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool DeleteUser(int userId)
        {
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    // Prevent deleting the default admin
                    cmd.CommandText = "DELETE FROM users WHERE id=@id AND email != 'admin@netmail.com';";
                    cmd.Parameters.AddWithValue("@id", userId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool ToggleUserVerification(int userId)
        {
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE users SET is_verified = CASE WHEN is_verified = 1 THEN 0 ELSE 1 END WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@id", userId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool ResetUserPassword(int userId, string newPassword)
        {
            string stored = HashHelper.CreateStoredPassword(newPassword);
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE users SET password_hash=@p WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@p", stored);
                    cmd.Parameters.AddWithValue("@id", userId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}