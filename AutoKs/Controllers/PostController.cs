using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoKs.Data;
using AutoKs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AutoKs.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context, UserManager<AutoKsUser> userManager)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            var VhlQuery = from b in _context.Vehicles select b;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                VhlQuery = VhlQuery.Where(s => s.UserId == userId);
            }
            return View(await VhlQuery.ToListAsync());
        }

        // GET: Favorites
        public async Task<IActionResult> Favorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorites = _context.MyFavourite
                .Where(favorite => favorite.UserId == userId)
                .Select(favorite => favorite.VehicleId).ToList();

            var query = _context.Vehicles.Where(v => favorites.Contains(v.Id));
            return View("Index", await query.ToListAsync());
        }
    }
}
