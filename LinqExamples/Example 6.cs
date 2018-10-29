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
    class Example_6
    {
        public static void Tutorial()
        {
            var cars = ProcessFileCars(@".\Data\fuel.csv");
            var manufacturers = ProcessFileManufacturers(@".\Data\manufacturers.csv");

            // Join by manufactures name eg. Toyota  from cars.csv with Toyota from manufatures.csv, output headquaters from mnaufactures.csv 
            // Produces an annonimous type class with select new that has a string property Name, Headquaters and Combined.
            var carsbymanufacturerquery = from car in cars
                                          join manufacturer in manufacturers
                                          on car.Manufacturer equals manufacturer.Name
                                          orderby car.Combined descending, car.Name ascending
                                          select new
                                          {
                                              car.Name,
                                              manufacturer.Headquaters,
                                              car.Combined
                                          };
      
            var carsbymanulamda = manufacturers.Join(cars,
                                                m => m.Name, c => c.Manufacturer,
                                                (m, c) => new
                                                {
                                                    m.Headquaters,
                                                    c.Name,
                                                    c.Combined
                                                })
                                                .OrderByDescending(cannocls => cannocls.Combined)
                                                .ThenBy(cannocls => cannocls.Name);


            // Use Take 10 used only during actual execution of query. Note that this is deffered execution
            Utils.AddLineBreak("Most efficient cars by headquaters");
            
            ////Lopp over the annonymous list that has 3 string properties
            //foreach (var annomousclassC in carsbymanufacturerquery.Take(10))
            //{
            //    Console.WriteLine($"{annomousclassC.Name} - {annomousclassC.Headquaters} : {annomousclassC.Combined}");
            //}

            foreach (var annomousclassC in carsbymanulamda.Take(10))
            {
                Console.WriteLine($"{annomousclassC.Name} - {annomousclassC.Headquaters} : {annomousclassC.Combined}");
            }



        }

        private static  List<Manufacturer> ProcessFileManufacturers(string path)
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
