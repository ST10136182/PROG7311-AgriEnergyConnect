using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Agri_Energy_Connect_Platform.Models;
using Agri_Energy_Connect_Platform.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Agri_Energy_Connect_Platform.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FarmerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FarmerController
        public ActionResult FarmerIndex()
        {
            return View();
        }

        

        //GET: FarmerController/AddProduct
        public async Task<IActionResult> AddProductView()
        {
            return View();
        }
        //POST: FarmerController/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductView(Products product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(User);
                    var farmer = await _context.Farmers.Include(f => f.Products).FirstOrDefaultAsync(f => f.UserId == userId);

                    if (farmer != null)
                    {
                        product.FarmersId = farmer.FarmersId;
                        farmer.Products.Add(product); // Add product via navigation property

                        await _context.SaveChangesAsync();

                        TempData["Message"] = "Product added successfully.";
                        return RedirectToAction(nameof(FarmerIndex));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Farmer not found.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine(ex.Message);
                    ModelState.AddModelError("", "An error occurred while saving to the database.");
                }
            }

            // Log ModelState errors for debugging
            foreach (var state in ModelState)
            {
                Console.WriteLine($"Key: {state.Key}, Error: {state.Value.Errors.FirstOrDefault()?.ErrorMessage}");
            }

            return View(product);
        }
    }
}
