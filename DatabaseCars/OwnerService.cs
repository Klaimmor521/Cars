using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace DatabaseCars
{
    public class OwnerService
    {
        private readonly string connectionString;
        public OwnerService(string connectionString)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["master"].ConnectionString;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Owner>("SELECT * FROM Owners").ToList();
            }
        }

        public void AddOwner(string OwnerName, string OwnerAddress)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Owners (OwnerName, OwnerAddress) VALUES (@OwnerName, @OwnerAddress)", connection);
                command.Parameters.AddWithValue("@OwnerName", OwnerName);
                command.Parameters.AddWithValue("@OwnerAddress", OwnerAddress);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateOwner(int id, string newName, string newAddress)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("UPDATE Owners SET OwnerName = @Name, OwnerAddress = @Address WHERE Id = @Id",
                    new { Name = newName, Address = newAddress, Id = id });
            }
        }

        public void DeleteOwner(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("DELETE FROM Owners WHERE Id = @Id", new { Id = id });
            }
        }
    }
}