using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StataIT.Data;
using StataIT.Models;

namespace StataIT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            //var data=_db.ProductTypes.ToList()
            return View(_db.ProductTypes.ToList());
        }

        //Create Get action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create Post action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if(ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type Saved Successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }

        //Get Edit action Method
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();

            }
            var productType = _db.ProductTypes.Find(id);
            if(productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Post Edit action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type Edited Successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }

        //Get Details action Method

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Post Details action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        }

        //Get Delete action Method

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Post Delete action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProductTypes productTypes)
        {
            if(id==null)
            {
                return NotFound();
            }

            if (id!=productTypes.ID)  /*if the route value and product value is NotFound same*/
            {
                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id); 
            if(productType==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }
    }
}