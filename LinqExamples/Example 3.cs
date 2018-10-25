using LinqExamples.LinqExtensionMethods;
using LinqExamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinqExamples
{
    /// <summary>
    /// Shows the use of deffered methods like where. Count,ToList execute immediatly as opposed to Take() or Where operator. Look at MSDN link or article on 
    /// google docs for table of differnent operators.
    /// </summary>
    class Example_3
    {
        public static void Tutorial()
        {            
            var jobs = new List<Job>()
            {
                new Job { Title = "Apple",     Description = "Need to buy Apples",    ModifiedDate = Convert.ToDateTime("10/12/2018")},
                new Job { Title = "Bananna",   Description = "Need to buy Bannana" ,  ModifiedDate = Convert.ToDateTime("10/15/2018")},
                new Job { Title = "Pinapple",  Description = "Need to buy Pinapple",  ModifiedDate = Convert.ToDateTime("10/22/2018")},
                new Job { Title = "Asperagus", Description = "Need to buy Asperagus", ModifiedDate = Convert.ToDateTime("10/25/2018")}
            };

            // Basic LINQ query with select
            var jobsstartingwithA = from j in jobs
                              where j.Title.StartsWith("A")
                              orderby j.Title
                              select j;

            Utils.AddLineBreak("Jobs that start with A");            

            foreach (var j in jobsstartingwithA)
            {               
                Console.WriteLine(j.Title + ' ' + j.Description);               
            }

            /// Return jobs modified after 10/18 with lamda expression
            
            var jobsgreaterthan10182018 = jobs.Where(j => j.ModifiedDate > Convert.ToDateTime("10/18/2018"));

            Utils.AddLineBreak("Jobs that was modified after 10/18/2018");          

            foreach (var j in jobsgreaterthan10182018)
            {             
                Console.WriteLine(j.Title + ' ' + j.Description);
            }

            // Uses custom Filter that uses yield to deffer execution untill the foreach is used.
            Utils.AddLineBreak("Jobs filtered using extension method");

            var jobsFiltered = jobs.Filter(j => j.ModifiedDate > Convert.ToDateTime("10/18/2018"));

            foreach (var j in jobsgreaterthan10182018)
            {
                Console.WriteLine(j.Title + ' ' + j.Description);
            }

            // To prevent deferred execution and have results immediatory look for ToArray(), ToDictionary methods.
            // Usually methods that offers to return IEnumerable often supports yield or deffered execution as opposed to
            // methods that return concrete Type like list or array.
            // for eg. to get immediate results back..use ToList

            Utils.AddLineBreak("Jobs brought back immediatly..no deffered execution");
            var jobsImmediatly = jobs.Filter(j => j.ModifiedDate > Convert.ToDateTime("10/18/2018")).ToList(); // Imp: ToList()
            foreach (var j in jobsImmediatly)
            {
                Console.WriteLine(j.Title + ' ' + j.Description);
            }


        }

       

}
}
