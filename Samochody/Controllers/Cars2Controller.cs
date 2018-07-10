using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Samochody.Models;

namespace Samochody.Controllers
{
    public class Cars2Controller : Controller
    {
        private CarDBContext db = new CarDBContext();

        // GET: Cars2
        //[HttpPost]
        //public ActionResult Index(SzukajAuta Model)
        //{
        //    var cars = from i in db.Cars select i;

        //    //jeśli coś przesłano, to wyszukaj po tym
        //    if (Model != null)
        //    {
        //        cars = from i in db.Cars where i.Model.Equals(Model.ModelZnajdz) && i.Brand.Equals(Model.BrandZnajdz) select i;
        //    }

        //    return View(cars.ToList());
        //}

        public ActionResult Index(string sortowanie, SzukajAuta Model)
        {
            ViewBag.SortByModel = sortowanie == null ? "Model_Malejaco" : "";
            ViewBag.SortByPrice = sortowanie == "Price_Malejaco" ? "Price_Rosnaco" : "Price_Malejaco";

            var cars = from i in db.Cars select i;
            switch (sortowanie)
            {
                case "Model_Malejaco":
                    cars = cars.OrderByDescending(s => s.Model);
                    break;
                case "Price_Malejaco":
                    cars = cars.OrderByDescending(s => s.Price);
                    break;
                case "Price_Rosnaco":
                    cars = cars.OrderBy(s => s.Price);
                    break;
                default:
                    cars = cars.OrderBy(s => s.Model);
                    break;
            }

            if (ModelState.IsValid)
            {
                if (Model.BrandZnajdz != null && Model.ModelZnajdz != null)
                {
                    cars = from i in db.Cars
                           where i.Model.Equals(Model.ModelZnajdz)
                           && i.Brand.Equals(Model.BrandZnajdz)
                           select i;
                }
                else if (Model.BrandZnajdz != null)
                {
                    cars = from i in db.Cars
                           where i.Brand.Equals(Model.BrandZnajdz)
                           select i;
                }
                else if (Model.ModelZnajdz != null)
                {
                    cars = from i in db.Cars
                           where i.Model.Equals(Model.ModelZnajdz)
                           select i;
                }
            }
            return View(cars.ToList());
        }

        // GET: Cars2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Brand,Model,Price,Bought,Sold")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brand,Model,Price,Bought,Sold")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
