using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples.Models
{
    public class Manufacturer
    {
        public string Name { get; set; }
        public string Headquaters { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }

        internal static Manufacturer ParseFromCSV(string line)
        {
            var columns = line.Split(',');

            return new Manufacturer
            {
                Name = columns[0],
                Headquaters = columns[1],
                Year = int.Parse(columns[2]),
                Country  = columns[1]
            };            
        }
    }
}
