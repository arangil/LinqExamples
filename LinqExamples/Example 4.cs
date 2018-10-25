using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Reads from csv filr furl.csv in data folder.
/// </summary>
namespace LinqExamples
{
    class Example_4
    {
        public static void Tutorial()
        {
            var cars = ProcessFile(@".\Data\fuel.csv");
            foreach(var car in cars)
            {
                Console.WriteLine(car.Name);
            }
        }

        ///// <summary>
        ///// First way. The .Select delegates the reading the file to a TransformToCar method inside this class or could be moved to util class
        ///// </summary>
        ///// <param name="path">path of the csv file</param>
        ///// <returns>List or cars</returns>
        //private static List<Car> ProcessFile(string path)
        //{
        //    return
        //    File.ReadAllLines(path)
        //         .Skip(1)
        //         .Where(line => line.Length > 1)
        //         .Select(TransformToCar)
        //         .ToList();
        //}

        //private static Car TransformToCar(string line)
        //{
        //    var columns = line.Split(',');

        //    return new Car
        //    {
        //        Year = int.Parse(columns[0]),
        //        Manufacturer = columns[1],
        //        Name = columns[2],
        //        Displacement = double.Parse(columns[3]),
        //        Cylinders = int.Parse(columns[4]),
        //        City = int.Parse(columns[5]),
        //        Highway = int.Parse(columns[6]),
        //        Combined = int.Parse(columns[7]),
        //    };
        //}

        /// <summary>
        /// Second way. The .Select delegates the reading the file to a TransformToCar method defined in the Cars.cs class
        /// </summary>
        /// <param name="path">path of the csv file</param>
        /// <returns>List or cars</returns>
        private static List<Car> ProcessFile(string path)
        {
            //// Using Lamda expression
            //return
            //File.ReadAllLines(path)
            //     .Skip(1)
            //     .Where(line => line.Length > 1)
            //     .Select(Car.ParseFromCSV)
            //     .ToList();

            // Using Linq query
            var carscollectionquery = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select Car.ParseFromCSV(line);

            return carscollectionquery.ToList();
                                         
        }

    }
}
