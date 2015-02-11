using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinika.Models.DatabaseObject;
using Clinika.Models.Gateway;

namespace Clinika.Controllers
{
    public class DoctorEntryController : Controller
    {
        private Gateway db = new Gateway();

        // GET: /DoctorEntry/
        public ActionResult Index()
        {
            return View(db.Doctors.ToList());
        }

        // GET: /DoctorEntry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorEntry doctorentry = db.Doctors.Find(id);
            if (doctorentry == null)
            {
                return HttpNotFound();
            }
            return View(doctorentry);
        }

        // GET: /DoctorEntry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DoctorEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DoctorId,Name,Degree,Specialization")] DoctorEntry doctorentry)
        {
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctorentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctorentry);
        }

        // GET: /DoctorEntry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorEntry doctorentry = db.Doctors.Find(id);
            if (doctorentry == null)
            {
                return HttpNotFound();
            }
            return View(doctorentry);
        }

        // POST: /DoctorEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DoctorId,Name,Degree,Specialization")] DoctorEntry doctorentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctorentry);
        }

        // GET: /DoctorEntry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorEntry doctorentry = db.Doctors.Find(id);
            if (doctorentry == null)
            {
                return HttpNotFound();
            }
            return View(doctorentry);
        }

        // POST: /DoctorEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorEntry doctorentry = db.Doctors.Find(id);
            db.Doctors.Remove(doctorentry);
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
