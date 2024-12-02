using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Configuration;

namespace DatabaseCars
{
    public class Owner
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAddress { get; set; }
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
    public class Car
    {
        public int Id { get; set; }
        public string CarModel { get; set; }
        public int RegistrationYear { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }

    class Program
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["master"].ConnectionString;
        private static readonly string masterConnectionString = "Data Source=KLAIMMOR\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
        static void Main(string[] args)
        {
            //CreateDatabaseAndTables();
            //Insert();
            var carService = new CarService(connectionString);
            var ownerService = new OwnerService(connectionString);

            //Работа с автомобилями
            //Добавление авто
            //carService.AddCar("Volvo XC90 I", 2019, 4);

            //Вывод авто
            //var cars = carService.GetAllCars();
            //foreach (var car in cars)
            //    Console.WriteLine($"Car: {car.CarModel}, Year: {car.RegistrationYear}");

            //Обновить авто
            //carService.UpdateCar(2, "Honda Odyssey", 2020);

            //Удалить авто
            //carService.DeleteCar(1);

            //Работа с владельцами
            //Вывод с владельцами
            //var owners = ownerService.GetAllOwners();
            //foreach (var owner in owners)
            //    Console.WriteLine($"Owner: {owner.OwnerName}, Address: {owner.OwnerAddress}");

            //Добавление владельца
            //ownerService.AddOwner("Danil", "210 Artic Pro");

            //Обновление владельца
            //ownerService.UpdateOwner(2, "Jenny Canoli", "456 Oak Avenie");

            //Удаление владельца
            //ownerService.DeleteOwner(1);

            //Вывод через EF авто с владельцами
            var carOwnerService = new CarOwnerService();
            carOwnerService.DisplayCarsWithOwners();
        }
        static void CreateDatabaseAndTables()
        {
            string createDatabase = @"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CarOwnerManager')
            BEGIN
                CREATE DATABASE CarOwnerManager;
            END";

            string createTables = @"
            USE CarOwnerManager;

            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Owners' AND xtype='U')
            BEGIN
                CREATE TABLE Owners (
                    Id INT PRIMARY KEY IDENTITY,
                    OwnerName NVARCHAR(100) NOT NULL,
                    OwnerAddress NVARCHAR(255) NOT NULL
                );
            END;

            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cars' AND xtype='U')
            BEGIN
                CREATE TABLE Cars (
                    Id INT PRIMARY KEY IDENTITY,
                    CarModel NVARCHAR(100) NOT NULL,
                    RegistrationYear INT NOT NULL,
                    OwnerId INT FOREIGN KEY REFERENCES Owners(Id)
                );
            END;";

            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();

                //Создание базы данных
                using (var command = new SqlCommand(createDatabase, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("База данных создана.");
                }

                //Создание таблиц
                using (var command = new SqlCommand(createTables, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Таблицы созданы.");
                }
            }
        }
        static void Insert()
        {
            string query1 = @"INSERT INTO Owners (OwnerName, OwnerAddress) VALUES 
                            ('John Doe', '123 Elm Street'),
                            ('Jane Smith', '456 Oak Avenue'),
                            ('Amina Arslanova', '569 Tik Preview');";


            string query2 = @"INSERT INTO Cars (CarModel, RegistrationYear, OwnerId) VALUES 
                            ('Toyota Corolla', 2020, 1),
                            ('Honda Civic', 2018, 2),
                            ('Ford Econoline', 1997, 3);";
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query1, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqlCommand(query2, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}