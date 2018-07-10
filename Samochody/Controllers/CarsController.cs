using Samochody.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samochody.Controllers
{
    public class CarsController : Controller
    {
        private CarDBContext baza = new CarDBContext();

        // GET: Car
        public ActionResult Index()
        {
            return View(baza.Cars.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult jakisKontroler(string id1, string id2)
        {
            ViewBag.id1 = id1;
            ViewBag.id2 = id2;
            return View();
        }

        // GET: /Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Cars/Create
        [HttpPost]
        //przesyłane wraz z POST, zabezpiecza przed złośliwą podmianą danych
        [ValidateAntiForgeryToken]
        //tutaj sprzężamy nasze pola z formularza z polami z modelu
        public ActionResult Create([Bind(Include = "Id,Brand,Model,Price,Bought,Sold")] Car car)
        {
            //sprawdzamy czy wystąpił jakiś błąd, np. błędny typ danych w formualrzu
            if (ModelState.IsValid)
            {
                using (var db = new CarDBContext())
                {
                    //dodanie samochodu
                    db.Cars.Add(car);
                    //zapsiane zmian
                    db.SaveChanges();
                    //przekierowanie do strony o akcji Index
                    return RedirectToAction("Index");
                }
               
            }
            //jeśli ModelState.IsValid wracamy z powrotem do formularza
            return View(car);
        }
    }
}