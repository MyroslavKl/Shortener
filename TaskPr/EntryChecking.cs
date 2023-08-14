using Microsoft.Data.SqlClient;

namespace TaskPr
{
    public class EntryChecking
    {
        private string email;
        private string password;

        public EntryChecking(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public bool Veryfing()
        {
            bool isValid = false;
            string connectionString = @"Data Source =.\SQL2022; Initial Catalog = ProjectW; Integrated Security = True;TrustServerCertificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Password FROM PesonalInformations WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();
                            isValid = VerifyPassword(password, storedPassword);
                        }
                    }
                }

            }
            return isValid;
        }

        private bool VerifyPassword(string password, string? storedPassword)
        {
            return password == storedPassword;
        }
    }
}
