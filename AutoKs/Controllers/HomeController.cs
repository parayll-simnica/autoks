
using AutoKs.Models;
using AutoKs.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;



namespace AutoKs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Make"] = _context.CarsList.Select(d => d.Make).Distinct().ToList().Select(d => new SelectListItem { Value = d, Text = d });
            return View(new SearchDB());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Search([Bind("IsCustomDutyPaid,NumberOfSeates,NumberOfDoors,IsRegistered,IsNonSmokingVehicle,//Owners,HasParkingCamera,HasParkingSensors,HasCentralDoorLock,HasPanoramaRoof,HasSportPackage,HasElectricHeatedSeats,HasAirCondition,IsUsed,Interior,Make,Model,City,Kilometers,Fuel,CubicCapacity,IsManual,Color,PriceFrom,PriceTo,ProductionYearFrom,ProductionYearTo,NumberOfDoors")] SearchDB search)
        {
            var result = _context.Vehicles.Where(d => d.Brand == search.Make);
            if (!string.IsNullOrEmpty(search.Model))
            {
                result = result.Where(d => d.Model == search.Model);
            }
            if (!string.IsNullOrEmpty(search.City))
            {
                result = result.Where(d => d.City == search.City);
            }
            if (!string.IsNullOrEmpty(search.Fuel))
            {
                result = result.Where(d => d.Fuel == search.Fuel);
            }
            if (search.Kilometers > 0)
            {
                result = result.Where(d => d.Kilometers == search.Kilometers);
            }
            if (search.CubicCapacity > 0)
            {
                result = result.Where(d => d.CubicCapacity == search.CubicCapacity);
            }
            if (search.IsManual==true)
            {
                result = result.Where(d => d.IsManual == search.IsManual);
            }
            if (search.IsCustomDutyPaid == true)
            {
                result = result.Where(d => d.CubicCapacity == search.CubicCapacity);
            }
            if (search.PriceFrom > 0)
            {
                result = result.Where(d => d.Price >= search.PriceFrom);
            }
            if (search.PriceTo > search.PriceFrom)
            {
                result = result.Where(d => d.Price <= search.PriceTo);
            }
            if (search.ProductionYearFrom >= DateTime.Now.AddYears(-100).Year)
            {
                result = result.Where(d => d.ProductionYear >= search.ProductionYearFrom);
            }
            if (search.ProductionYearTo > search.ProductionYearFrom)
            {
                result = result.Where(d => d.ProductionYear <= search.ProductionYearFrom);
            }
            //VhlDetails atributes
            if (!string.IsNullOrEmpty(search.Interior))
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.Interior == search.Interior));
            }
            if (search.NumberOfSeates > 0)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.NumberOfSeates == search.NumberOfSeates));
            }
            if (search.NumberOfDoors > 0)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.NumberOfDoors == search.NumberOfDoors));
            }
            if (search.IsRegistered == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.IsRegistered == search.IsRegistered));
            }
            if (search.IsNonSmokingVehicle == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.IsNonSmokingVehicle == search.IsNonSmokingVehicle));
            }
            if (search.Owners > 0)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.Owners == search.Owners));
            }
            if (search.HasParkingCamera == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.HasParkingCamera == search.HasParkingCamera));
            }
            if (search.HasParkingSensors == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.HasParkingSensors == search.HasParkingSensors));
            }
            if (search.HasCentralDoorLock == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.HasCentralDoorLock == search.HasCentralDoorLock));
            }
            if (search.HasPanoramaRoof ==true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.HasPanoramaRoof == search.HasPanoramaRoof));
            }
            if (search.HasSportPackage == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.HasSportPackage == search.HasSportPackage));
            }
            if (search.HasElectricHeatedSeats == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.HasElectricHeatedSeats == search.HasElectricHeatedSeats));
            }
            if (search.HasAirCondition == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.HasAirCondition == search.HasAirCondition));
            }
            if (search.IsUsed == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.IsUsed == search.IsUsed));
            }
            if (!string.IsNullOrEmpty(search.Color))
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.Color == search.Color));
            }
            if (search.IsCustomDutyPaid == true)
            {
                result = result.Where(d => d.VehicleDetails.Any(l => l.IsCustomDutyPaid == search.IsCustomDutyPaid));
            }

            search.Result = result.ToList();
            ViewData["Make"] = _context.CarsList.Select(d => d.Make).Distinct().ToList().Select(d => new SelectListItem { Value = d, Text = d });
            return View("Index", search);
        }
    }
}
