using System;
using System.Linq;
//Entity Framework

namespace DatabaseCars
{
    public class CarOwnerService
    {
        public void DisplayCarsWithOwners()
        {
            using (var context = new CarRegistryContext())
            {
                var carsWithOwners = context.Cars.Include("Owner").ToList();

                foreach (var car in carsWithOwners)
                {
                    Console.WriteLine($"Car: {car.CarModel}, Year: {car.RegistrationYear}, Owner: {car.Owner.OwnerName}");
                }
            }
        }
    }
}