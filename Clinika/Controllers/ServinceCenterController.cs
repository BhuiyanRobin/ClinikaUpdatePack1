using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CliniKa.Models.DatabaseObject;
using Clinika.Models.Gateway;

namespace Clinika.Controllers
{
    public class ServinceCenterController : Controller
    {
        private Gateway db = new Gateway();

        // GET: /ServinceCenter/

        public ActionResult ShowCreatedServiceCenter()
        {
            return View();
        }
        public ActionResult Index()
        {
            var servicecenters = db.ServiceCenters.Include(s => s.ADistrict).Include(s => s.AUpazila);
            return View(servicecenters.ToList());
        }

        // GET: /ServinceCenter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCenter servicecenter = db.ServiceCenters.Find(id);
            if (servicecenter == null)
            {
                return HttpNotFound();
            }
            return View(servicecenter);
        }

        // GET: /ServinceCenter/Create
        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name");
            ViewBag.UpazilaId = new SelectList(db.Upazilas, "Id", "Name");
            return View();
        }

        // POST: /ServinceCenter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Code,Password,DistrictId,UpazilaId")] ServiceCenter servicecenter)
        {
            servicecenter.Code = CodeGenaretor();
            servicecenter.Password = servicecenter.Code;

            if (ModelState.IsValid)
            {
                db.ServiceCenters.Add(servicecenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", servicecenter.DistrictId);
            ViewBag.UpazilaId = new SelectList(db.Upazilas, "Id", "Name", servicecenter.UpazilaId);
            return View(servicecenter);
        }

        // GET: /ServinceCenter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCenter servicecenter = db.ServiceCenters.Find(id);
            if (servicecenter == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", servicecenter.DistrictId);
            ViewBag.UpazilaId = new SelectList(db.Upazilas, "Id", "Name", servicecenter.UpazilaId);
            return View(servicecenter);
        }

        // POST: /ServinceCenter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Code,Password,DistrictId,UpazilaId")] ServiceCenter servicecenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicecenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", servicecenter.DistrictId);
            ViewBag.UpazilaId = new SelectList(db.Upazilas, "Id", "Name", servicecenter.UpazilaId);
            return View(servicecenter);
        }

        // GET: /ServinceCenter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCenter servicecenter = db.ServiceCenters.Find(id);
            if (servicecenter == null)
            {
                return HttpNotFound();
            }
            return View(servicecenter);
        }

        // POST: /ServinceCenter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceCenter servicecenter = db.ServiceCenters.Find(id);
            db.ServiceCenters.Remove(servicecenter);
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

        public ActionResult InsertServiceCenter(int districtId, int upazilaId, string serviceCenterName)
        {
            
            var centerName = db.ServiceCenters.FirstOrDefault(x => x.Name == serviceCenterName);


            if (centerName == null)
            {
                ServiceCenter aServiceCenter=new ServiceCenter();
                aServiceCenter.Name = serviceCenterName;
                aServiceCenter.DistrictId = districtId;
                aServiceCenter.UpazilaId = upazilaId;
                aServiceCenter.Code = CodeGenaretor();
                aServiceCenter.Password = PasswordGenaretor();
                db.ServiceCenters.Add(aServiceCenter);
                db.SaveChanges();
                ViewBag.ServiceCenter = aServiceCenter;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult IsNameAvailable(string serviceCenterName)
        {
            var center = db.ServiceCenters.FirstOrDefault(x => x.Name == serviceCenterName);
            if (center != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public string CodeGenaretor()
        {
            string name = "jsfkljruw5r4127r9ojfd33nfHJ4545KSHF489iuai452uishdj48rijla45fkdvnf545vneroi89768ufhfajdklafj";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(name, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;

        }
        public string PasswordGenaretor()
        {
            string name = "jsfkljf45647567567sfsruw5r4127r53ffsfsfs5354fs9ojfds535ff33nff354sHJ453545KSHFffs48fs9iuai45fsfsaf2uishdj48rijla45fkdvnf545vneroi89768ufhfajdklafj";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(name, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;

        }

        public ActionResult FindThisDistrict(int districtId)
        {
            Gateway aCenterGateway = new Gateway();

            var relations = aCenterGateway.DistrictUpazilaRelation.Where(x => x.DistrictId == districtId).ToList();

            var upazilas = (aCenterGateway.DistrictUpazilaRelation.Join(aCenterGateway.Upazilas, p => p.Id, prd => prd.Id,
                (p, prd) => new { p, prd }).Where(@t => @t.p.DistrictId == districtId).Select(@t => new
                {
                    @t.prd.Id,
                    @t.prd.Name
                })).ToList();
            return Json(upazilas, JsonRequestBehavior.AllowGet);
        }
    }
}
