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
    public class DiseaseController : Controller
    {
        private Gateway db = new Gateway();

        // GET: /Disease/
        public ActionResult Index()
        {
            return View(db.Diseaseses.ToList());
        }

        // GET: /Disease/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diseases diseases = db.Diseaseses.Find(id);
            if (diseases == null)
            {
                return HttpNotFound();
            }
            return View(diseases);
        }

        // GET: /Disease/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Disease/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DiseasesId,Name,Description,TreatmentProcedure,PreferdMedicien")] Diseases diseases)
        {
            if (ModelState.IsValid)
            {
                db.Diseaseses.Add(diseases);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diseases);
        }

        // GET: /Disease/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diseases diseases = db.Diseaseses.Find(id);
            if (diseases == null)
            {
                return HttpNotFound();
            }
            return View(diseases);
        }

        // POST: /Disease/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DiseasesId,Name,Description,TreatmentProcedure,PreferdMedicien")] Diseases diseases)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diseases).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diseases);
        }

        // GET: /Disease/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diseases diseases = db.Diseaseses.Find(id);
            if (diseases == null)
            {
                return HttpNotFound();
            }
            return View(diseases);
        }

        // POST: /Disease/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diseases diseases = db.Diseaseses.Find(id);
            db.Diseaseses.Remove(diseases);
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

        public JsonResult NameExists(string name)
        {
            var adisease = db.Diseaseses.FirstOrDefault(x => x.Name == name);


            if (adisease != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
