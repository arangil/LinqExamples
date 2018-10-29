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
    class Example_8
    {
        public static void Tutorial()
        {
            var cars = ProcessFileCars(@".\Data\fuel.csv");
            var manufacturers = ProcessFileManufacturers(@".\Data\manufacturers.csv");

            var carbymanuquery = from car in cars
                                 group car by car.Manufacturer.ToUpper() into manufacturer
                                 orderby manufacturer.Key
                                 select manufacturer;

            var carbymanylamda = cars.GroupBy(c => c.Manufacturer.ToUpper())
                                     .OrderBy(c => c.Key);
                                 
            //Linq
            //foreach(var manufacturingroup in carbymanuquery)
            //{
            //    Console.WriteLine(manufacturingroup.Key);
               
            //    foreach (var car in manufacturingroup.OrderByDescending(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}

            //Lamda
            foreach (var manufacturingroup in carbymanylamda)
            {
                Console.WriteLine(manufacturingroup.Key);

                foreach (var car in manufacturingroup.OrderByDescending(c => c.Combined).Take(2))
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
