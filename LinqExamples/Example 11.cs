using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// shows top 3 fuel efficient cars by country..
/// eg
/// Japan
///      Prius 2  33 mpg
///      Prius 3  31 mpg
///      Camry    30 mpg
/// </summary>
namespace LinqExamples
{
    class Example_11
    {

        //Gropus the manufactures and gets the Max, Min and Average fuel efficiecy, results are ordered by fuel efficiecny 
        public static void Tutorial()
        {
            var cars = ProcessFileCars(@".\Data\fuel.csv");


            var query = from car in cars
                        group car by car.Manufacturer into cargroup
                        select new
                        {
                            Name = cargroup.Key,
                            Max = cargroup.Max(c => c.Combined),
                            Min = cargroup.Min(c => c.Combined),
                            Average = cargroup.Average(c => c.Combined)
                        };

            Utils.AddLineBreak("Linq :Gropus the manufactures and gets the Max, Min and Average fuel efficiecy, results are ordered by fuel efficiecny");

            foreach (var group in query.OrderByDescending(c => c.Max))
            {
                Console.WriteLine($"{group.Name}");
                Console.WriteLine($"\tMax:{group.Max}");
                Console.WriteLine($"\tMin:{group.Min}");
                Console.WriteLine($"\tAvg:{group.Average}");
            }

          
        }

        #region ReadfromCSV


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
