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
    class Example_10
    {
        public static void Tutorial()
        {
            var cars = ProcessFileCars(@".\Data\fuel.csv");
            var manufacturers = ProcessFileManufacturers(@".\Data\manufacturers.csv");

            var query = from car in cars
                        join manufacturer in manufacturers
                        on car.Manufacturer equals manufacturer.Name
                        orderby car.Combined descending, car.Name ascending    //cars are alredy sorted by fuel effencicy
                        select new  //new obj had headquaters too...the collection is already sorted by fuel effenciey
                        {
                            car.Name,
                            manufacturer.Country,
                            car.Combined
                        } into result  
                        group result by result.Country; //group results by country..

            Utils.AddLineBreak("Lamda :Most 3 efficient cars by country, ordereded by efficiency and showin by country");

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Key}");

                foreach (var car in group.Take(3))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            //or

            var query2 = from manufacturer in manufacturers
                        join car in cars on manufacturer.Name equals car.Manufacturer
                        into cargroup
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = cargroup
                        }
                        into result
                        group result by result.Manufacturer.Country;

            Utils.AddLineBreak("Lamda :Most 3 efficient cars by country");

            foreach (var group in query2)
            {
                Console.WriteLine($"{group.Key}");
                foreach (var car in group.SelectMany(c => c.Cars)
                                        .OrderByDescending(c => c.Combined)
                                        .Take(3))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            var carbymanulamda = manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer, (m, c) => new
            {
                Manufacturer = m,
                Cars = c
            })           
            .GroupBy(m => m.Manufacturer.Country);

            Utils.AddLineBreak("Lamda :Most efficient cars by headquaters");

            foreach (var group in carbymanulamda)
            {
                Console.WriteLine($"{group.Key}");

                foreach (var car in group.SelectMany(c => c.Cars).OrderByDescending(c => c.Combined).ThenBy(c => c.Name).Take(3))
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
