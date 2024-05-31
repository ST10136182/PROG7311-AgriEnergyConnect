using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Agri_Energy_Connect_Platform.Models;
using Agri_Energy_Connect_Platform.Data;    
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agri_Energy_Connect_Platform.Controllers
{
    //[Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: EmployeeController
        public ActionResult EmployeeIndex()
        {
            return View();
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult AddFarmer()
        {
            return View();
        }

        

        // POST: Employee/AddFarmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFarmer(FarmerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create a new IdentityUser
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    // Create the user and assign the password
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        // Add the user to the "Farmer" role
                        await _userManager.AddToRoleAsync(user, "Farmer");

                        // Set the UserId of the farmer to the Id of the new user
                        var farmer = new Farmers
                        {
                            FullName = model.FullName,
                            ContactNumber = model.ContactNumber,
                            Address = model.Address,
                            UserId = user.Id
                        };

                        // Add the farmer to the database
                        _context.Farmers.Add(farmer);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(EmployeeIndex));
                    }
                    else
                    {
                        // If there was an error creating the user, add the errors to the ModelState
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine(ex.Message);
                    // Add an error message to the model state
                    ModelState.AddModelError("", "An error occurred while saving to the database.");
                }
            }
            return View(model);
        }


        // GET: Employee/ViewProducts
        public IActionResult ViewProducts()
        {
            ViewBag.Farmers = new SelectList(_context.Farmers, "FarmersId", "FullName");
            return View();
        }

        // POST: Employee/ViewProducts
        [HttpPost]
        public IActionResult ViewProducts(int farmersId, DateTime? startDate, DateTime? endDate, string category)
        {
            ViewBag.Farmers = new SelectList(_context.Farmers, "FarmersId", "FullName");

            var products = _context.Products.Where(p => p.FarmersId == farmersId).AsQueryable();

            if (startDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate >= startDate);
            }
            if (endDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate <= endDate);
            }
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
            }

            return View(products.ToList());
        }

    }
}
