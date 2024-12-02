using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Linq;
//Dapper

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
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Cars";
                return connection.Query<Car>(query).ToList();
            }
        }

        public void AddCar(string CarModel, int RegistrationYear, int OwnerId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Cars (CarModel, RegistrationYear, OwnerId) VALUES (@CarModel, @RegistrationYear, @OwnerId)";
                connection.Execute(query, new { CarModel, RegistrationYear, OwnerId });
            }
        }

        public void UpdateCar(int Id, string NewCarModel, int NewRegistrationYear)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE Cars SET CarModel = @CarModel, RegistrationYear = @RegistrationYear WHERE Id = @Id";
                connection.Execute(query, new { CarModel = NewCarModel, RegistrationYear = NewRegistrationYear, Id });
            }
        }

        public void DeleteCar(int Id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Cars WHERE Id = @Id";
                connection.Execute(query, new { Id });
            }
        }
    }
}