using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    class Program
    {
        static void Main(string[] args)
        {

            //Introduction to basic Linq statement
            //Example_1.Tutorial();

            // Shows use of Func, Action, var, chained queries on var and Ienumerable lists.
            //Example_2.Tutorial();

            //Shows the first use of linq query with select statement in the query
            //Example_3.Tutorial();

            //Shows reading data from csv file
            //Example_4.Tutorial();

            //Shows getting top bmw cars in 2016 and using FirstorDefault, also shows ordering, secondary ordering etc.
            //Example_5.Tutorial();

            //Shows to read from two data sources, join the data and show aggreate restults
            //Example_6.Tutorial();

            //Join the data with two fields Manufacturer and Year and show aggreate restults
            //Example_7.Tutorial();

            //shows result ordered by manufactoers name, then the top 2 efficiency cars
            Example_8.Tutorial();

            Console.ReadLine();
        }
    }
}
