using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using CliniKa.Models.DatabaseObject;
using Clinika.Models.Gateway;

namespace Clinika.Controllers
{
    public class MedicineController : Controller
    {
        private Gateway db = new Gateway();

        // GET: /Medicine/
        public ActionResult Index()
        {
            return View(db.Medicines.ToList());
        }

        // GET: /Medicine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // GET: /Medicine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Medicine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MedicineId,Name,Power,PowerUnit")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicine);
        }

        // GET: /Medicine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: /Medicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MedicineId,Name,Power,PowerUnit")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicine);
        }

        // GET: /Medicine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: /Medicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            db.Medicines.Remove(medicine);
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
        public JsonResult IsAvialable(string name)
        {
            Medicine aMedicine = new Medicine();

            aMedicine.Name = name;


            var check = db.Medicines.FirstOrDefault(c => c.Name == aMedicine.Name);
            if (check != null)
            {
                db.Medicines.Add(aMedicine);
                db.SaveChanges();
                return Json(aMedicine, JsonRequestBehavior.AllowGet);
            }
            return Json(aMedicine, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Chk(string Name,string Power,string PowerUnit)
        {
            Medicine aMedicine=new Medicine();
            aMedicine.Name = Name;
            aMedicine.Power = Power;
            aMedicine.PowerUnit = PowerUnit;
            string message = "";
            var check =db.Medicines.FirstOrDefault(p => p.Name == Name && p.Power == Power && p.PowerUnit == PowerUnit);
            if (check == null)
            {
                db.Medicines.Add(aMedicine);
                db.SaveChanges();
                message = "";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                message = "Already Exist";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
