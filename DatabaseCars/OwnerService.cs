using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
//ADO.NET

namespace DatabaseCars
{
    public class OwnerService
    {
        private readonly string connectionString;
        public OwnerService(string connectionString)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["master"].ConnectionString;
        }

        public List<Owner> GetAllOwners()
        {
            var owners = new List<Owner>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Owners";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            owners.Add(new Owner
                            {
                                Id = (int)reader["Id"],
                                OwnerName = reader["OwnerName"].ToString(),
                                OwnerAddress = reader["OwnerAddress"].ToString()
                            });
                        }
                    }
                }
            }
            return owners;
        }

        public void AddOwner(string OwnerName, string OwnerAddress)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Owners (OwnerName, OwnerAddress) VALUES (@OwnerName, @OwnerAddress)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OwnerName", OwnerName);
                    command.Parameters.AddWithValue("@OwnerAddress", OwnerAddress);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateOwner(int Id, string NewName, string NewAddress)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE Owners SET OwnerName = @OwnerName, OwnerAddress = @OwnerAddress WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OwnerName", NewName);
                    command.Parameters.AddWithValue("@OwnerAddress", NewAddress);
                    command.Parameters.AddWithValue("@Id", Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteOwner(int Id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Owners WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}