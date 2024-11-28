using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace DatabaseCars
{
    public class CarService
    {
        private readonly string connectionString;
        public CarService(string connectionString)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["master"].ConnectionString; ;
        }

        public List<Car> GetAllCars()
        {
            var cars = new List<Car>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Cars", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(new Car
                            {
                                Id = (int)reader["Id"],
                                CarModel = reader["CarModel"].ToString(),
                                RegistrationYear = (int)reader["RegistrationYear"],
                                OwnerId = (int)reader["OwnerId"]
                            });
                        }
                    }
                }
            }
            return cars;
        }

        public void AddCar(string model, int year, int ownerId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Cars (CarModel, RegistrationYear, OwnerId) VALUES (@Model, @Year, @OwnerId)", connection);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@Year", year);
                command.Parameters.AddWithValue("@OwnerId", ownerId);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateCar(int id, string model, int year)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Cars SET CarModel = @Model, RegistrationYear = @Year WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@Year", year);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCar(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Cars WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}