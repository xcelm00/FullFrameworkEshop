using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FullFrameworkEshop.Models;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace FullFrameworkEshop.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Country).Include(c => c.Region);
            return View(customers.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.CountryIso3 = new SelectList(db.Countries, "Iso3", "CountryNameEnglish");
            ViewBag.RegionCode = new SelectList(db.Regions, "RegionCode", "RegionNameEnglish");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CustomerName,CountryIso3,RegionCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CustomerID = Guid.NewGuid();
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryIso3 = new SelectList(db.Countries, "Iso3", "CountryNameEnglish", customer.CountryIso3);
            ViewBag.RegionCode = new SelectList(db.Regions, "RegionCode", "Iso3", customer.RegionCode);
            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryIso3 = new SelectList(db.Countries, "Iso3", "CountryNameEnglish", customer.CountryIso3);
            ViewBag.RegionCode = new SelectList(db.Regions, "RegionCode", "Iso3", customer.RegionCode);
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CustomerName,CountryIso3,RegionCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryIso3 = new SelectList(db.Countries, "Iso3", "CountryNameEnglish", customer.CountryIso3);
            ViewBag.RegionCode = new SelectList(db.Regions, "RegionCode", "Iso3", customer.RegionCode);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetCountries()
        {

            //var countries = from c in db.Countries
            //                select new 
            //                {
            //                    c.CountryNameEnglish,
            //                    c.Iso3,
            //                    c.Regions = new List<Region>(
            //                        from r in c.Regions
            //                        select new Region()
            //                        {
            //                            RegionNameEnglish = r.RegionNameEnglish,
            //                            Iso3 = r.Iso3,
            //                            RegionCode = r.RegionCode,
            //                            Country = r.Country
            //                        }).Cast<Region>()
            //                };


            //db.Configuration.LazyLoadingEnabled = false;
            //db.Configuration.ProxyCreationEnabled = false;



            //var countries = db.Countries.Include(x => x.Regions).ToList();
            var countries = JsonConvert.SerializeObject(db.Countries.Include(x => x.Regions), Formatting.None, new JsonSerializerSettings() {ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });


            //    var countries = from c in db.Countries
            //        select new
            //        {
            //            c.CountryNameEnglish,
            //            c.Iso3,
            //            Region = new List<Region>(
            //                from r in c.Regions
            //                select new Region()
            //                {
            //                    RegionNameEnglish = r.RegionNameEnglish,
            //                    Iso3 = r.Iso3,
            //                    RegionCode = r.RegionCode,
            //                    Country = r.Country
            //                }).Cast<Region>()
            //        };


            //return Json(countries, JsonRequestBehavior.AllowGet);
            return Content(countries, "application/json");

        }

        //[HttpGet]
        //public ActionResult GetRegions(string iso3)
        //{
        //    List<SelectListItem> regions = db.Regions.AsNoTracking().Where(x=>x.Iso3 == iso3)
        //        .OrderBy(x => x.RegionNameEnglish)
        //        .Select(x => new SelectListItem
        //        {
        //            Value = x.RegionCode,
        //            Text = x.RegionNameEnglish
        //        }).ToList();

        //    regions.Insert(0, new SelectListItem
        //    {
        //        Value = null,
        //        Text = "---please select region---"
        //    });

        //    return Json(regions, JsonRequestBehavior.AllowGet);
        //}


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
