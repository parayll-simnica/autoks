using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoKs.Models
{
    public class SearchDB
    {
        public SearchDB()
        {
            Result = new List<Vehicle>();
            
        }
        [Required]
        public string Make { get; set; }
        public string Model { get; set; }
        public int ProductionYearFrom { get; set; }
        public int ProductionYearTo { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public string City { get; set; }
        public double Kilometers { get; set; }
        public string Fuel { get; set; }
        public double CubicCapacity { get; set; }
        public bool IsManual { get; set; }
        public string Interior { get; set; }
        public string Color { get; set; }
        public bool IsUsed { get; set; }
        public bool HasAirCondition { get; set; }
        public bool HasElectricHeatedSeats { get; set; }
        public bool HasSportPackage { get; set; }
        public bool HasPanoramaRoof { get; set; }
        public bool HasCentralDoorLock { get; set; }
        public bool HasParkingSensors { get; set; }
        public bool HasParkingCamera { get; set; }
        public byte Owners { get; set; }
        public bool IsNonSmokingVehicle { get; set; }
        public bool IsRegistered { get; set; }
        public byte NumberOfDoors { get; set; }
        public byte NumberOfSeates { get; set; }
        public bool IsCustomDutyPaid { get; set; }
        public List<Vehicle> Result { get; set; }
       
    }
}

