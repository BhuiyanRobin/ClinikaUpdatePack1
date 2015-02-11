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
    public class DiseasesReportGenerateController : Controller
    {
        private Gateway db = new Gateway();

        
        // GET: /DiseasesReportGenerate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DiseasesReportGenerate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,DistrictId,DiseasesId,FromDateTime,ToDateTime")] DieaseAffectInfo dieaseaffectinfo)
        {
            if (ModelState.IsValid)
            {
                db.DieaseAffectInfoes.Add(dieaseaffectinfo);
                db.SaveChanges();
                
            }

            return View(dieaseaffectinfo);
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
