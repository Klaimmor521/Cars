using System;
using System.Linq;

namespace DatabaseCars
{
    public class CarOwnerService
    {
        public void DisplayCarsWithOwners()
        {
            using (var context = new CarRegistryContext())
            {
                var cars = context.Cars.Include("Owner").ToList();

                foreach (var car in cars)
                {
                    Console.WriteLine($"Car: {car.CarModel}, Year: {car.RegistrationYear}, Owner: {car.Owner.OwnerName}");
                }
            }
        }
    }
}