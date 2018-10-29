using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///
/// </summary>
namespace LinqExamples
{
    class Example_7
    {
        public static void Tutorial()
        {
            var cars = ProcessFileCars(@".\Data\fuel.csv");
            var manufacturers = ProcessFileManufacturers(@".\Data\manufacturers.csv");

            // Join by manufactures name and year             

            //Linq
            var carsbymanufacturerandyearquery = from car in cars
                                          join manufacturer in manufacturers
                                          on new { car.Manufacturer, car.Year}
                                          equals
                                             new {Manufacturer =  manufacturer.Name, manufacturer.Year}
                                          orderby car.Combined descending, car.Name ascending
                                          select new
                                          {
                                              car.Name,
                                              manufacturer.Headquaters,
                                              car.Combined
                                          };

            //Lambda
            var carsbymanuandyearlamda = manufacturers.Join(cars,
                                                m => new { Manufacturer = m.Name, m.Year},
                                                c => new {c.Manufacturer, c.Year},
                                                (m, c) => new
                                                {
                                                    m.Headquaters,
                                                    c.Name,
                                                    c.Combined
                                                })
                                                .OrderByDescending(c => c.Combined)
                                                .ThenBy(c => c.Name);


            // Use Take 10 used only during actual execution of query. Note that this is deffered execution
            Utils.AddLineBreak("LINQ :Most efficient cars by headquaters and year match");

            //Lopp over the annonymous list that has 3 string properties
            foreach (var annomousclassC in carsbymanufacturerandyearquery.Take(10))
            {
                Console.WriteLine($"{annomousclassC.Name} - {annomousclassC.Headquaters} : {annomousclassC.Combined}");
            }

            Utils.AddLineBreak("LAMBDA :Most efficient cars by headquaters and year match");

            foreach (var annomousclassC in carsbymanuandyearlamda.Take(10))
            {
                Console.WriteLine($"{annomousclassC.Name} - {annomousclassC.Headquaters} : {annomousclassC.Combined}");
            }



        }

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

    }
}
