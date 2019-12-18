using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestioningAdmin.Models;

namespace QuestioningAdmin.Controllers
{
    public class HomeController : Controller
    {

       static List<CEmployee> empls = new List<CEmployee>();
       static List<int> Voiting = new List<int>();
        public ActionResult Index()
        {
            QuestioningAdminModel model = new QuestioningAdminModel();
            ToBase b = new ToBase();
            empls = b.GetAllEmployees();
            Voiting = b.GetAllVoiting();
            XRep.XrepSoapClient cl = new XRep.XrepSoapClient();
            XRep.DepartmentInfo[] deps= cl.GetPointList3();
            foreach (XRep.DepartmentInfo dep in deps)
            {
                if (!dep.Enabled) { continue; }
                QuestioningResultModel res = new QuestioningResultModel();
                res.DepId = dep.Number;
                res.DepName = dep.Name;
                res.PeoplesCount = empls.Where(a => a.DepId == dep.Number ).Count();
              //res.PeoplesCount = empls.Where(a => a.DepId == dep.Number && (a.PosId==2 || a.PosId == 4 || a.PosId == 8)).Count();
                res.Voided = empls.Where(a => a.DepId == dep.Number && Voiting.Contains(a.Id)).Count()+b.GetVoitingByDep(dep.Number);
                model.Res.Add(res);
            }
            return View(model);
        }


        public ActionResult EmplsNotVoid(int DepId, string DepName)
        {
            EmplsNotVoidModel model = new EmplsNotVoidModel();
            
            model.Empls = empls.Where(a => a.DepId == DepId && !Voiting.Contains(a.Id)).ToList();

            model.DepName = DepName;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}