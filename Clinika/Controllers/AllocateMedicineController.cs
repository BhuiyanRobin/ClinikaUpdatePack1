using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinika.Models.DatabaseObject;
using CliniKa.Models.DatabaseObject;
using Clinika.Models.Gateway;
using Clinika.Models.Relations;
using Microsoft.Ajax.Utilities;

namespace Clinika.Controllers
{
    public class AllocateMedicineController : Controller
    {
        private Gateway db = new Gateway();

        // GET: /AllocateMedicine/
        public ActionResult Index()
        {
            var allocatemedicines = db.AllocateMedicines.Include(a => a.AMedicine).Include(a => a.AServiceCenter);
            return View(allocatemedicines.ToList());
        }

        // GET: /AllocateMedicine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateMedicine allocatemedicine = db.AllocateMedicines.Find(id);
            if (allocatemedicine == null)
            {
                return HttpNotFound();
            }
            return View(allocatemedicine);
        }

        // GET: /AllocateMedicine/Create
        public ActionResult Create()
        {
            var medicines = db.Medicines;
            List<Medicine>allMedicine=new List<Medicine>();
            foreach (Medicine medicine in medicines)
            {
                Medicine aMedicine=new Medicine();
                aMedicine.Name = medicine.Name + "-" + medicine.Power + medicine.PowerUnit;
                aMedicine.MedicineId = medicine.MedicineId;
                allMedicine.Add(aMedicine);

            }
            ViewBag.Districts = new SelectList(db.Districts, "Id", "Name");
            ViewBag.MedicineId = new SelectList(allMedicine, "MedicineId", "Name");
            ViewBag.ServiceCenterId = new SelectList(db.ServiceCenters, "Id", "Name");
            return View();
        }

        // POST: /AllocateMedicine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,ServiceCenterId,Quantity,MedicineId,DistrictUpazilaId")] AllocateMedicine
                allocatemedicine)
        {

            return RedirectToAction("Create");
        }

        // GET: /AllocateMedicine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateMedicine allocatemedicine = db.AllocateMedicines.Find(id);
            if (allocatemedicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "Name", allocatemedicine.MedicineId);
            ViewBag.ServiceCenterId = new SelectList(db.ServiceCenters, "Id", "Name", allocatemedicine.ServiceCenterId);
            return View(allocatemedicine);
        }

        // POST: /AllocateMedicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,ServiceCenterId,Quantity,MedicineId,DistrictUpazilaId")] AllocateMedicine
                allocatemedicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocatemedicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "Name", allocatemedicine.MedicineId);
            ViewBag.ServiceCenterId = new SelectList(db.ServiceCenters, "Id", "Name", allocatemedicine.ServiceCenterId);
            return View(allocatemedicine);
        }

        // GET: /AllocateMedicine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateMedicine allocatemedicine = db.AllocateMedicines.Find(id);
            if (allocatemedicine == null)
            {
                return HttpNotFound();
            }
            return View(allocatemedicine);
        }

        // POST: /AllocateMedicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllocateMedicine allocatemedicine = db.AllocateMedicines.Find(id);
            db.AllocateMedicines.Remove(allocatemedicine);
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

        public ActionResult FindThisDistrict(int districtId)
        {
            Gateway aCenterGateway = new Gateway();

            var relations = aCenterGateway.DistrictUpazilaRelation.Where(x => x.DistrictId == districtId).ToList();

            var upazilas =
                (aCenterGateway.DistrictUpazilaRelation.Join(aCenterGateway.Upazilas, p => p.Id, prd => prd.Id,
                    (p, prd) => new { p, prd }).Where(@t => @t.p.DistrictId == districtId).Select(@t => new
                    {
                        @t.prd.Id,
                        @t.prd.Name
                    })).ToList();
            return Json(upazilas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindServiceCenter(int upazilaId)
        {
            var serviceCenters = (from p in db.ServiceCenters
                                  where p.UpazilaId == upazilaId
                                  select new
                                  {
                                      Name = p.Name,
                                      ServiceCenterId = p.Id,
                                  });


            return Json(serviceCenters, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Chk(Collection0[] message)
        {
            foreach (Collection0 aCollection0 in message)
            {
                AllocateMedicine allocateMedicine = new AllocateMedicine();
                int district = aCollection0.DistrictId;
                int upazila = aCollection0.UpazilaId;
                var relations =
                    db.DistrictUpazilaRelation.FirstOrDefault(p => p.UpazilaId == upazila && p.DistrictId == district);

                if (relations.Id == 0)
                {

                }
                else
                {
                    allocateMedicine.MedicineId = Convert.ToInt16(aCollection0.MedicineId);
                    allocateMedicine.DistrictUpazilaId = relations.Id;
                    allocateMedicine.ServiceCenterId = Convert.ToInt16(aCollection0.ServiceCenterId);
                    ;
                    allocateMedicine.Quantity = Convert.ToInt16(aCollection0.Quantity);
                    db.AllocateMedicines.Add(allocateMedicine);
                    db.SaveChanges();
                }

            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
