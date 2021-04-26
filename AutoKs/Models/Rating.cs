using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoKs.Models
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string UserId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime Date { get; set; }
        public virtual AutoKsUser User { get; set; }
        public virtual Vehicle Vehicle { get; set; }

    }
}