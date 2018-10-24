using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public class Example_1
    {
        public static void Basic()
        {

            IEnumerable<Job> jobs = new List<Job>()
            {
                new Job{ Title="Apple", Description="Need to buy Apples"},
                new Job { Title = "Bananna", Description = "Need to buy Bannana" },
                new Job { Title = "Pinapple", Description = "Need to buy Pinapple" }
            };

            foreach (var J in jobs.Where(j => j.Title.StartsWith("A")))
            {
                Console.WriteLine(J.Title + ' ' + J.Description);
            }           
        }
    }
}
