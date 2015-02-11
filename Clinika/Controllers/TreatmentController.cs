using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml;
using Clinika.Models.DatabaseObject;
using CliniKa.Models.DatabaseObject;
using Clinika.Models.Relations;
using Clinika.Models.Gateway;

namespace Clinika.Controllers
{
    public class TreatmentController : Controller
    {
        private Gateway db = new Gateway();

        // GET: /PatientInformation/
        public ActionResult Index()
        {
            var treatmentses = db.Treatmentses.Include(t => t.ADiseases).Include(t => t.ADose).Include(t => t.AMeal).Include(t => t.AMedicine);
            return View(treatmentses.ToList());
        }

        // GET: /PatientInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatments treatments = db.Treatmentses.Find(id);
            if (treatments == null)
            {
                return HttpNotFound();
            }
            return View(treatments);
        }

        // GET: /PatientInformation/Create
        public ActionResult Create()
        {
            ViewBag.DiseasesId = new SelectList(db.Diseaseses, "DiseasesId", "Name");
            ViewBag.DoseId = new SelectList(db.Doses, "DoseId", "Name");
            ViewBag.MealId = new SelectList(db.Meals, "MealId", "Name");
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "Name");
            return View();
        }

        // POST: /PatientInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TreatmentsId,VoterId,Name,Address,DateOfBirht,ServiceGiven,Observation,Date,DoctorEntryId,DiseasesId,MedicineId,DoseId,MealId,QuantityGiven,Note")] Treatments treatments)
        {
            if (ModelState.IsValid)
            {
                db.Treatmentses.Add(treatments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiseasesId = new SelectList(db.Diseaseses, "DiseasesId", "Name", treatments.DiseasesId);
            ViewBag.DoseId = new SelectList(db.Doses, "DoseId", "Name", treatments.DoseId);
            ViewBag.MealId = new SelectList(db.Meals, "MealId", "Name", treatments.MealId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "Name", treatments.MedicineId);
            return View(treatments);
        }

        // GET: /PatientInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatments treatments = db.Treatmentses.Find(id);
            if (treatments == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiseasesId = new SelectList(db.Diseaseses, "DiseasesId", "Name", treatments.DiseasesId);
            ViewBag.DoseId = new SelectList(db.Doses, "DoseId", "Name", treatments.DoseId);
            ViewBag.MealId = new SelectList(db.Meals, "MealId", "Name", treatments.MealId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "Name", treatments.MedicineId);
            return View(treatments);
        }

        // POST: /PatientInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TreatmentsId,VoterId,Name,Address,DateOfBirht,ServiceGiven,Observation,Date,DoctorEntryId,DiseasesId,MedicineId,DoseId,MealId,QuantityGiven,Note")] Treatments treatments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiseasesId = new SelectList(db.Diseaseses, "DiseasesId", "Name", treatments.DiseasesId);
            ViewBag.DoseId = new SelectList(db.Doses, "DoseId", "Name", treatments.DoseId);
            ViewBag.MealId = new SelectList(db.Meals, "MealId", "Name", treatments.MealId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "Name", treatments.MedicineId);
            return View(treatments);
        }

        // GET: /PatientInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatments treatments = db.Treatmentses.Find(id);
            if (treatments == null)
            {
                return HttpNotFound();
            }
            return View(treatments);
        }

        // POST: /PatientInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treatments treatments = db.Treatmentses.Find(id);
            db.Treatmentses.Remove(treatments);
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
        public ActionResult GetVoterInformation(string voterId)
        {
            string url = "http://nerdcastlebd.com/web_service/voterdb/index.php/voters/voter/" + voterId + "";

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(response.GetResponseStream());
            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("voter");
            VoterInformation aVoter = new VoterInformation();
            foreach (XmlNode node in nodes)
            {
                aVoter.VoterId = node.SelectSingleNode("id").InnerText;
                aVoter.Name = node.SelectSingleNode("name").InnerText;
                aVoter.Address = node.SelectSingleNode("address").InnerText;
                aVoter.DateOfbirth = node.SelectSingleNode("date_of_birth").InnerText;
            }
            var servicegiven = db.TreatmentRelations.Count(p => voterId == p.VoterId);
            aVoter.Servicegiven = servicegiven;

            return Json(aVoter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllDoctor()
        {
            var doctors = db.Doctors;
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TreatmentAPatientEntry(TreatmentRelation aTreatmentRelation, TreatmentMedicinegiven[] medicinegivens, PatientInformation[] patientInformations)
        {
            var voterId = "";
            foreach (PatientInformation patient in patientInformations)
            {
                var patientInfo = db.PatientInformations.FirstOrDefault(p => p.VoterId == patient.VoterId);
                if (patientInfo==null)
                {
                    PatientInformation aPatientInformation = new PatientInformation();
                    aPatientInformation.VoterId = patient.VoterId;
                    aPatientInformation.Name = patient.Name;
                    aPatientInformation.Address = patient.Address;
                    aPatientInformation.DateOfBirth = Convert.ToDateTime(patient.DateOfBirth);
                    aPatientInformation.DateOfBirth = patient.DateOfBirth;
                    db.PatientInformations.Add(patient);
                    voterId = aPatientInformation.VoterId;
                }
                else
                {
                    voterId = patient.VoterId;
                }
               
            }
            db.SaveChanges();
            aTreatmentRelation.DateOfObservation = Convert.ToDateTime(aTreatmentRelation.DateOfObservation);
            aTreatmentRelation.ServiceCenterCode = "ha";
            aTreatmentRelation.VoterId = voterId;
            db.TreatmentRelations.Add(aTreatmentRelation);
            db.SaveChanges();
            var diseaseId = 0;
            foreach (TreatmentMedicinegiven medicinegiven in medicinegivens)
            {
                TreatmentMedicinegiven aMedicinegiven = new TreatmentMedicinegiven();
                diseaseId = aMedicinegiven.DiseasesId;
                aMedicinegiven.DoseId = medicinegiven.DoseId;
                aMedicinegiven.MealId = medicinegiven.MealId;
                aMedicinegiven.DiseasesId = medicinegiven.DiseasesId;
                aMedicinegiven.MedicineId = medicinegiven.MedicineId;
                aMedicinegiven.Note = medicinegiven.Note;
                aMedicinegiven.QuantityGiven = medicinegiven.QuantityGiven;
                aMedicinegiven.VoterId = voterId;
                db.TreatmentMedicinegivens.Add(aMedicinegiven);
                db.SaveChanges();
            }
            PatientCount aPatientCount = new PatientCount();
            var patientChk = db.PatientList.FirstOrDefault(p => p.VoterId == voterId && p.DiseasesId == p.DiseasesId);
            if (patientChk == null)
            {
                aPatientCount.DiseasesId = diseaseId;
                aPatientCount.VoterId = voterId;
                aPatientCount.DateTime = aTreatmentRelation.DateOfObservation;
                db.PatientList.Add(aPatientCount);
                db.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
