using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// shows result ordered by manufactoers name, then the top 2 efficiency cars
/// </summary>
namespace LinqExamples
{
    class Example_9
    {
        public static void Tutorial()
        {
            var cars = ProcessFileCars(@".\Data\fuel.csv");
            var manufacturers = ProcessFileManufacturers(@".\Data\manufacturers.csv");

            var carbymanuquery = from manufacturer in manufacturers
                                 join car in cars on manufacturer.Name equals car.Manufacturer
                                 into manufacturercargroup
                                 select new
                                 {
                                     Manufacturer = manufacturer,
                                     Cars = manufacturercargroup
                                 };
            Utils.AddLineBreak("LINQ :Most efficient cars by headquaters");

            foreach (var group in carbymanuquery)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquaters}");

                foreach(var car in group.Cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            var carbymanulamda = manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer, (m, c) => new
            {
                Manufacturer = m,
                Cars = c
            }).OrderBy(m => m.Manufacturer.Name);

            Utils.AddLineBreak("Lamda :Most efficient cars by headquaters");

            foreach (var group in carbymanulamda)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquaters}");

                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

        }

        #region ReadfromCSV

        private static List<Manufacturer> ProcessFileManufacturers(string path)
        {
            var manufacturercollectionquery = from line in File.ReadAllLines(path)
                                              where line.Length > 1
                                              select Manufacturer.ParseFromCSV(line);

            return manufacturercollectionquery.ToList();
        }

        private static List<Car> ProcessFileCars(string path)
        {
            var carscollectionquery = from line in File.ReadAllLines(path).Skip(1)
                                      where line.Length > 1
                                      select Car.ParseFromCSV(line);

            return carscollectionquery.ToList();

        }
        #endregion

    }
}
