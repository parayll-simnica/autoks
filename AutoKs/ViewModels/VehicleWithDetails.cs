using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoKs.Models
{
    public class VehicleWithDetails
    {
        public VehicleWithDetails(Vehicle vehicle, VhlDetails details)
        {
            //Vehicle
            this.Brand = vehicle.Brand;
            this.Model = vehicle.Model;
            this.Price = vehicle.Price;
            this.ProductionYear = vehicle.ProductionYear;
            this.Kilometers = vehicle.Kilometers;
            this.City = vehicle.City;
            this.Description = vehicle.Description;
            this.Photo = vehicle.Photo;
            this.Fuel = vehicle.Fuel;
            this.CubicCapacity = vehicle.CubicCapacity;
            this.IsManual = vehicle.IsManual;
            this.IsFavorite = vehicle.IsFavorite;
            this.CostumerHasVisitedTheCar = vehicle.CostumerHasVisitedTheCar;
            this.ModifyDate = vehicle.ModifyDate;
            this.InsertDate = vehicle.InsertDate;
            this.IsActive = vehicle.IsActive;
            this.UserId = vehicle.UserId;
            this.User = vehicle.User;
            this.Posts = vehicle.Posts;
            this.Ratings = vehicle.Ratings;
            this.ImageFile = vehicle.ImageFile;
            //VehicleDetails
            this.Interior = details.Interior;
            this.IsUsed = details.IsUsed;
            this.HasAirCondition = details.HasAirCondition;
            this.HasElectricHeatedSeats = details.HasElectricHeatedSeats;
            this.HasSportPackage = details.HasSportPackage;
            this.HasPanoramaRoof = details.HasPanoramaRoof;
            this.HasCentralDoorLock = details.HasCentralDoorLock;
            this.HasParkingSensors = details.HasParkingSensors;
            this.HasParkingCamera = details.HasParkingCamera;
            this.Owners = details.Owners;
            this.IsNonSmokingVehicle = details.IsNonSmokingVehicle;
            this.IsRegistered = details.IsRegistered;
            this.NumberOfDoors = details.NumberOfDoors;
            this.NumberOfSeates = details.NumberOfSeates;
            this.IsCustomDutyPaid = details.IsCustomDutyPaid;
            this.Color = details.Color;
        }
        //Vehicle
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public int ProductionYear { get; set; }
        public double Kilometers { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Fuel { get; set; }
        public double CubicCapacity { get; set; }
        public bool IsManual { get; set; }
        public bool IsFavorite { get; set; }
        public bool CostumerHasVisitedTheCar { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public virtual AutoKsUser User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public IFormFile ImageFile { get; }

        //VehicleDetails
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
    }
}
