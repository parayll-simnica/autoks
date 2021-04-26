using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoKs.Models
{
    public class CarList
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string Year { get; set; }
    }
}
