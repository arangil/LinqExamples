using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    /// <summary>
    /// Shows the first use of linq query with select statement in the query
    /// </summary>
    class Example_3
    {
        public static void Tutorial()
        {            
            var jobsasvar = new List<Job>()
            {
                new Job{ Title="Apple", Description="Need to buy Apples"},
                new Job { Title = "Bananna", Description = "Need to buy Bannana" },
                new Job { Title = "Pinapple", Description = "Need to buy Pinapple" },
                new Job { Title = "Asperagus", Description = "Need to buy Asperagus" }
            };

            var orderedjobs = from j in jobsasvar
                              where j.Title.StartsWith("A")
                              orderby j.Title
                              select j;

            foreach (var j in orderedjobs)
            {
                Console.WriteLine(j.Title + ' ' + j.Description);
            }
        }

}
}
