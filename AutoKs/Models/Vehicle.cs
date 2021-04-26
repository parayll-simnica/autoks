using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoKs.Models
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        //[ExcludeText("Select Brand")]
        public string Brand { get; set; }
        [Required]
        //[ExcludeText("Select Model")]
        public string Model { get; set; }
        [Required]
        public double Price { get; set; }
        public int ProductionYear { get; set; }
        public double Kilometers { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        [Column(TypeName="nvarchar(100)")]
        public string Photo { get; set; }
        public string Fuel { get; set; }
        public double CubicCapacity { get; set; }
        public bool IsManual { get; set; }
        public bool IsFavorite { get; set; }
        public bool CostumerHasVisitedTheCar { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        [DisplayName("Upload")]
        public IFormFile ImageFile { get; set; }
        public string UserId { get; set; }
        public virtual AutoKsUser User { get; set; }
        
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<VhlDetails> VehicleDetails { get; set; }
    }
    public class ExcludeText : ValidationAttribute
    {
        private readonly string _text;
        public ExcludeText(string text)
            : base("{0} contains invalid text.")
        {
            _text = text;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString().ToLower().Contains(_text.ToLower()))
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
