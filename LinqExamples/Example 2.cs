using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public class Example_2
    {
        public static void Tutorial()
        {

            // Func calls an external static method Square. A ending variable of a func always the return type/value. in this case the func 
            // takes an int and returns and int
            Func<int, int> f = Square;
            Console.WriteLine(f(3).ToString());

            //Func can evaluate the function inline.
            Func<int, int> SquareMe = x => x * x;
            Console.WriteLine(SquareMe(5).ToString());

            // If func has more than one inputs, distinguish each with seperate alphabets x,y,z..etc..
            Func<int, int, int> Add = (x, y) => x + y;
            Console.WriteLine(Add(5,6).ToString());

            // Complex calucluations can be done inline as shown below.
            Func<double, double> AreaOfCircle = x =>
            {
                double area = Math.PI * x * x;
                return area;
            };                        
            Console.WriteLine("Area of Circle with radius 5 cm is " + AreaOfCircle(5).ToString() + " cm.");

            // An Action Methods is like a func but does not return anything..or returns void..
            Action<int> SquareThis = x => Console.WriteLine(x * x);
            SquareThis(3);

            // Use of chained linq queries.
            IEnumerable<Job> jobs = new List<Job>()
            {
                new Job{ Title="Apple", Description="Need to buy Apples"},
                new Job { Title = "Bananna", Description = "Need to buy Bannana" },
                new Job { Title = "Pinapple", Description = "Need to buy Pinapple" },
                new Job { Title = "Asperagus", Description = "Need to buy Asperagus" }
            };

            // Get jobs whoes title starts with A and order the results by asc (by default)
            foreach (var job in jobs.Where(j => j.Title.StartsWith("A"))
                                    .OrderBy(j => j.Title)
                    ) 
            {
                Console.WriteLine(job.Title + ' ' + job.Description);
            }


           // Use var instead of IEnumerable for initializing a list.
           var jobsasvar = new List<Job>()
            {
                new Job{ Title="Apple", Description="Need to buy Apples"},
                new Job { Title = "Bananna", Description = "Need to buy Bannana" },
                new Job { Title = "Pinapple", Description = "Need to buy Pinapple" },
                new Job { Title = "Asperagus", Description = "Need to buy Asperagus" }
            };

            foreach (var job in jobsasvar.Where(j => j.Title.StartsWith("A"))
                                    .OrderBy(j => j.Title)
                                    .Select(j => j) // This line is not needed.
                    )
            {
                Console.WriteLine(job.Title + ' ' + job.Description);
            }
        }

        private static int Square(int arg)
        {
            return arg * arg;
        }
    }
}
