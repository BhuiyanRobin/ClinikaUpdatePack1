using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using CliniKa.Models.DatabaseObject;
using Clinika.Models.Gateway;
using Clinika.Models.Relations;

namespace Clinika.Controllers
{
    public class ShowAllPatientHistoryController : Controller
    {
        Gateway db = new Gateway();
        //
        // GET: /ShowAllPatientHistory/
        public ActionResult Show()
        {
            return View();
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

        public ActionResult GetPatientHistory(string voterId)
        {
            var history = db.TreatmentRelations.Where(p => p.VoterId == voterId);
            var serviceCenters = db.ServiceCenters.ToList(); 
            
            List<Information> informations = new List<Information>();
           
            foreach (TreatmentRelation relation in history)
            {
                
                Information aInformation = new Information();
                //string code = Convert.ToString(relation.ServiceCenterCode);
                var servciecenter = serviceCenters.FirstOrDefault(p => p.Code == relation.ServiceCenterCode);
               
                    aInformation.ServcieCenterCode = servciecenter.Code;
                    aInformation.ServiceCenterName=servciecenter.Name;
            
                aInformation.Dates = relation.DateOfObservation;
                informations.Add(aInformation);
            }
            return Json(informations, JsonRequestBehavior.AllowGet);
        }

        internal class Information
        {
            public DateTime Dates { set; get; }
            public string ServiceCenterName { set; get; }
            public string ServcieCenterCode { set; get; }
        }
    }
}