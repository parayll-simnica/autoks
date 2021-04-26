using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoKs.Data;
using AutoKs.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AutoKs.Controllers
{
    //[Authorize]
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public VehicleController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Vehicle
        public async Task<IActionResult> Index(string searchString)
        {
            var VhlQuery = from b in _context.Vehicles select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                string[] searchStringSplited = searchString.Split(' ');
                foreach (var word in searchStringSplited)
                {
                    VhlQuery = VhlQuery.Where(s => s.Brand.Contains(word)
                                    || s.Model.Contains(word)
                                    || s.ProductionYear.ToString().Contains(word)
                                    || s.City.Contains(word)
                                    || s.Description.Contains(word)
                    );
                }
            }
            ViewBag.Favorites = _context.MyFavourite
                .Where(favorite => favorite.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))//gjaje userin e logum 
                .Select(favorite => favorite.VehicleId).ToList();//edhe shfaqja krejt kerret favourite
            return View(await VhlQuery.ToListAsync());

            //var applicationDbContext = _context.Vehicles.Include(v => v.User);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicle/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var details = await _context.VhlDetails
                .FirstOrDefaultAsync(d => d.VehicleId == vehicle.Id);

            var VehicleWithDetails = new VehicleWithDetails(vehicle, details);
            return View(VehicleWithDetails);
        }

        // GET: Vehicle/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<AutoKsUser>(), "Id", "Id");
            ViewData["Make"] = _context.CarsList.Select(d => d.Make).Distinct().ToList().Select(d => new SelectListItem { Value = d, Text = d });


            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,Price,ProductionYear,Kilometers,City,Description,ImageFile,CostumerHasVisitedTheCar,IsManual,Fuel,CubicCapacity,UserId,ModifyDate,InsertDate")] Vehicle vehicle,
            [Bind("VehicleId,Interior,Color,IsCustomDutyPaid,IsRegistered,IsUsed,HasAirCondition,HasElectricHeatedSeats,HasSportPackage,HasPanoramaRoof,HasCentralDoorLock,HasParkingSensors,HasParkingCamera" +
            "Owners,IsNonSmokingVehicle,NumberOfDoors,NumberOfSeates")] VhlDetails vhlDetails)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(vehicle.ImageFile.FileName);
                string extension = Path.GetExtension(vehicle.ImageFile.FileName);
                vehicle.Photo=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwrootPath + "/Img/", fileName);
                using (var fileStream=new FileStream(path, FileMode.Create))
                {
                    await vehicle.ImageFile.CopyToAsync(fileStream);
                }

                vehicle.Id = Guid.NewGuid();
                vhlDetails.Id = Guid.NewGuid(); //Generating a new Id for vhlDetails
                vhlDetails.VehicleId = vehicle.Id; // Giving to Details the Vehicle ID
                vehicle.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Giving UserId the value of the Current User 
                _context.Add(vehicle);
                _context.VhlDetails.Add(vhlDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<AutoKsUser>(), "Id", "Id", vehicle.UserId);
            ViewData["Make"] = _context.CarsList.Select(d => d.Make).Distinct().ToList().Select(d => new SelectListItem { Value = d, Text = d });

            return View(vehicle);
        }

        // POST: Vehicle/Favorite
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Favorite(Guid id)
        {
            var CurrentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var FavoritesCount = _context.MyFavourite.Where(favorite => favorite.UserId == CurrentUser && favorite.VehicleId == id).Count();
            if(FavoritesCount == 0)
            {
                MyFavourite favorite = new MyFavourite();
                favorite.Id = Guid.NewGuid();
                favorite.UserId = CurrentUser;
                favorite.VehicleId = id;
                _context.MyFavourite.Add(favorite);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("DeleteFavorite")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteFavorite(Guid id)
        {
            var vehicle = await _context.MyFavourite.FirstOrDefaultAsync(d => d.VehicleId == id);

            _context.MyFavourite.Remove(vehicle);
            await _context.SaveChangesAsync();
           
            return RedirectToAction("Favorites", "Post");
        }

        private bool FavoriteExists(Guid id)
        {
            return _context.MyFavourite.Any(e => e.Id == id);
        }

        // GET: Vehicle/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            var details = await _context.VhlDetails
                .FirstOrDefaultAsync(d => d.VehicleId == vehicle.Id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var vehicleWithDetails = new VehicleWithDetails(vehicle, details);
            ViewData["UserId"] = new SelectList(_context.Set<AutoKsUser>(), "Id", "Id", vehicle.UserId);
            ViewData["Make"] = _context.CarsList.Select(d => d.Make).Distinct().ToList().Select(d => new SelectListItem { Value = d, Text = d });

            return View(vehicleWithDetails);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Brand,Model,Price,ProductionYear,Kilometers,City,Description,Photo,CostumerHasVisitedTheCar,IsManual,Fuel,CubicCapacity,UserId,ModifyDate,InsertDate")] Vehicle vehicle,
            [Bind("Id,VehicleId,Interior,Color,IsCustomDutyPaid,IsRegistered,IsUsed,HasAirCondition,HasElectricHeatedSeats,HasSportPackage,HasPanoramaRoof,HasCentralDoorLock,HasParkingSensors,HasParkingCamera" +
            "Owners,IsNonSmokingVehicle,NumberOfDoors,NumberOfSeates")] VhlDetails vhlDetails)
        {
            
            if (id != vehicle.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                var details = await _context.VhlDetails.FirstOrDefaultAsync(v => v.VehicleId == id);
                vhlDetails.Id = details.Id;
                vhlDetails.VehicleId = id;
                _context.Entry<VhlDetails>(details).State = EntityState.Detached;

                try
                {
                    _context.Vehicles.Update(vehicle);
                    _context.VhlDetails.Update(vhlDetails);
                    await _context.SaveChangesAsync();
                    //_context.Entry<VhlDetails>(vhlDetails).State = EntityState.Detached;
                    //_context.Entry<Vehicle>(vehicle).State = EntityState.Detached;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var vehicleWithDetails = new VehicleWithDetails(vehicle, vhlDetails);
            ViewData["UserId"] = new SelectList(_context.Set<AutoKsUser>(), "Id", "Id", vehicle.UserId);
            ViewData["Make"] = _context.CarsList.Select(d => d.Make).Distinct().ToList().Select(d => new SelectListItem { Value = d, Text = d });

            return View(vehicleWithDetails);
        }

        // GET: Vehicle/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(Guid id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
        [HttpGet]
        public JsonResult ModelList(string Id)
        {
            return Json(_context.CarsList.Where(d => d.Make == Id).Select(d => d.Model).Distinct().ToList());
        }
    }
}
