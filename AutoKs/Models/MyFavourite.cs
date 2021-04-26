using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoKs.Models
{
    public class MyFavourite
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public string UserId { get; set; }

        public Vehicle Vehicle { get; set; }
        public AutoKsUser User { get; set; }
    }
}
