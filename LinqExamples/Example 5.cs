using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Sorts results based on car efficiency and returns the top 10 cars..Note 2 cars have same efficiency of 42...so a secondary sort on name assending is used.
/// Find most effienent 10 bmw cars in 2016
/// Find the top bmw car in 2016..using First and FirstorDefault
/// Note: Skip and Take operaters are used when paging is used.
/// </summary>
namespace LinqExamples
{
    class Example_5
    {
        public static void Tutorial()
        {
            var cars = ProcessFile(@".\Data\fuel.csv");

            //Lamda to find top 10 efficent cars and order them by name
            //var efficiencyquery = cars.OrderByDescending(c => c.Combined);
            //var efficiencyquery = cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name);

            //Linq to find top 10 efficent cars and order them by name
            var efficiencyquery = from car in cars
                                  orderby car.Combined descending, car.Name ascending
                                  select car;

            // Use Take 10 used only during actual execution of query. Note that this is deffered execution
            Utils.AddLineBreak("Top 10 cars with best effiecienty");
            foreach (var car in efficiencyquery.Take(10))
            {
                Console.WriteLine($"{car.Name} : {car.Combined}");
            }


            //Find top 10 efficent BMW cars made on 2016 and order them by name

            Utils.AddLineBreak("BMW cars with best effiecienty in 2016");

            //Lamda to find top 10 efficent BMW cars made on 2016 and order them by name
            //var bmwcarquery = cars.Where(c => c.Manufacturer.Equals("BMW") && c.Year.Equals(2016)).OrderByDescending(c => c.Combined).ThenBy(c => c.Name);

            //Linq to find top 10 efficent BMW cars made on 2016 and order them by name
            var bmwcarquery = from car in cars
                              where car.Manufacturer == "BMW" && car.Year == 2016
                              orderby car.Combined descending, car.Name ascending
                              select car;

            foreach (var car in bmwcarquery.Take(10))
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined} mpg");
            }

            //Get the top BMW or use the First operator..First is not a deffered operator so it returns just one car..so no need of foreach
            Utils.AddLineBreak("Top BMW car with best effiecienty for 2016");
            var topbmwcarquery = cars.Where(c => c.Manufacturer.Equals("BMW") && c.Year.Equals(2016)).OrderByDescending(c => c.Combined).ThenBy(c => c.Name).First();
            Console.WriteLine($"{topbmwcarquery.Manufacturer} {topbmwcarquery.Name} : {topbmwcarquery.Combined} mpg");

            //Get the top BMW or use the First operator..First is not a deffered operator so it returns just one car..so no need of foreach.
            //here we use FirstOrDefault as if it dosent find a record if wont raise and exception..it will give a null exception error..
            Utils.AddLineBreak("Top BMW car with best effiecienty for 2016 by using FirstOrDefault");
            var topbmwcarquerywithFirstofDefault = cars.Where(c => c.Manufacturer.Equals("BMW") && c.Year.Equals(2016)).OrderByDescending(c => c.Combined).ThenBy(c => c.Name).FirstOrDefault();

            if(topbmwcarquerywithFirstofDefault != null) // check this to make sure result is not null
            Console.WriteLine($"{topbmwcarquerywithFirstofDefault.Manufacturer} {topbmwcarquery.Name} : {topbmwcarquery.Combined} mpg");


        }

        /// <summary>
        /// The .Select delegates the reading the file to a TransformToCar method defined in the Cars.cs class
        /// </summary>
        /// <param name="path">path of the csv file</param>
        /// <returns>List or cars</returns>
        private static List<Car> ProcessFile(string path)
        {          
            var carscollectionquery = from line in File.ReadAllLines(path).Skip(1)
                                      where line.Length > 1
                                      select Car.ParseFromCSV(line);

            return carscollectionquery.ToList();

        }

    }
}
