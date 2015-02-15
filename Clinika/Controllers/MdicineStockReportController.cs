using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinika.Migrations;
using Clinika.Models.DatabaseObject;
using Clinika.Models.Gateway;

namespace Clinika.Controllers
{
    public class MdicineStockReportController : Controller
    {
        Gateway db = new Gateway();
        //
        // GET: /MdicineStockReport/
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetMedicineStockReport()
        {
            var serviceCenterCode = "Ha";
            var serciceCenterId = db.ServiceCenters.FirstOrDefault(p => p.Code == serviceCenterCode);
            var getAllMedicineTreatment = db.TreatmentMedicinegivens.Where(p => p.SevcieCenterCode == serviceCenterCode).ToList();
            List<int> allMedicineId = new List<int>();
           

            List<MedicineReport> medicineReports = new List<MedicineReport>();

            foreach (TreatmentMedicinegiven aMedicinegiven in getAllMedicineTreatment)
            {
                var alreadyexist = allMedicineId.Any(p => p == aMedicinegiven.MedicineId);
                if (alreadyexist == false)
                {
                    allMedicineId.Add(aMedicinegiven.MedicineId);
                }
            }
            foreach (int medicineId in allMedicineId)
            {
                MedicineReport aReport = new MedicineReport();
                 double medicineGiven = 0;
                 double medicineQuantity = 0;
                
                var medicineStock = db.AllocateMedicines.Where(p => p.MedicineId == medicineId && p.ServiceCenterId == serciceCenterId.Id).ToList();
                var givenMedicine = getAllMedicineTreatment.Where(p => p.MedicineId == medicineId).ToList();
                foreach (TreatmentMedicinegiven medicinegiven in givenMedicine)
                {
                    medicineGiven += medicinegiven.QuantityGiven;
                }
                foreach (AllocateMedicine medicine in medicineStock)
                {
                    medicineQuantity += medicine.Quantity;
                }
                var aMedicine = db.Medicines.FirstOrDefault(p => p.MedicineId == medicineId);
                aReport.MedicineQuantity = medicineQuantity - medicineGiven;
                aReport.MedicineName = aMedicine.Name;
                medicineReports.Add(aReport);
            }
            return Json(medicineReports, JsonRequestBehavior.AllowGet);
        }
    }

    internal class MedicineReport
    {
        public string MedicineName { set; get; }
        public double MedicineQuantity { set; get; }
    }
}