using Shiverleih.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shiverleih.Controllers
{
    [Authorize]
    public class RentalController : Controller
    {
        private readonly RentalRepo rentalRepo = new RentalRepo();
        // GET: Rental
        public ActionResult Index()
        {
            return View(rentalRepo.getRentals());
        }

        // GET: Rental/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rental = rentalRepo.getRentalDetail((int)id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        public ActionResult RentProduct()
        {
            ViewBag.CustomerID = new SelectList(rentalRepo.getCustomers(), "CustomerID", "Fname");
            ViewBag.ProductID = new SelectList(rentalRepo.getProducts(), "ProductID", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RentProduct(int? CustomerID, int? ProductID)
        {
            if (CustomerID == null || ProductID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = rentalRepo.RentProduct((int)ProductID, (int)CustomerID);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("ProductIDs", "Das gewählte produkt ist nichtmehr auf lager");
                ViewBag.CustomerID = new SelectList(rentalRepo.getCustomers(), "CustomerID", "Fname");
                ViewBag.ProductID = new SelectList(rentalRepo.getProducts(), "ProductID", "Title");
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rental = rentalRepo.getRental((int)id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(rentalRepo.getCustomers(), "CustomerID", "Fname",rental.CustomerID);
            ViewBag.ProductID = new SelectList(rentalRepo.getProducts(), "ProductID", "Title",rental.ProductID);
            return View(rental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? RentalID,int? CustomerID, int? ProductID)
        {
            if (RentalID == null || CustomerID == null || ProductID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = rentalRepo.EditRental((int)RentalID, (int)CustomerID, (int)ProductID);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("ProductID", "Das gewählte produkt ist nichtmehr auf lager");
                ViewBag.CustomerID = new SelectList(rentalRepo.getCustomers(), "CustomerID", "Fname");
                ViewBag.ProductID = new SelectList(rentalRepo.getProducts(), "ProductID", "Title");
                return View();
            }
        }

        public ActionResult ReturnProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rentalRepo.ReturnProduct((int)id);
            return RedirectToAction("Index");
        }

        // gibt eine Detail Ansicht zurück über das zu löschende Rental
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rental = rentalRepo.getRental((int)id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            rentalRepo.DeleteRental(rental.RentalID);
            return RedirectToAction("Index");
        }
    }
}