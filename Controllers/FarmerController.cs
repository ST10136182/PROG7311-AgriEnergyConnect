using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Agri_Energy_Connect_Platform.Models;
using Agri_Energy_Connect_Platform.Data;
using Microsoft.AspNetCore.Identity;

namespace Agri_Energy_Connect_Platform.Controllers
{
    //[Authorize(Roles = "Farmer")]
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

        /* Commenting out the following methods
        // GET: FarmerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FarmerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FarmerController/Create
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

        // GET: FarmerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FarmerController/Edit/5
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

        // GET: FarmerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FarmerController/Delete/5
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
        */

        //GET: FarmerController/AddProduct
        public async Task<IActionResult> AddProductView()
        {
            return View();
        }
        //GET: FarmerController/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductView(Products product)
        {

            if (ModelState.IsValid)
            {
                var uuserId = _userManager.GetUserId(User);
                var farmer = _context.Farmers.Where(f => f.UserId == uuserId).FirstOrDefault();
                if (farmer != null)
                {
                    product.FarmersId = farmer.FarmersId;
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Product added successfully.";

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }
    }
}
