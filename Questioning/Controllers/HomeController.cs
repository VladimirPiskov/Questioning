using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Questioning.Models;
namespace Questioning.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            SetEmployeeModel m = new SetEmployeeModel();
            return View(m);
        }
        public ActionResult QSucseess()
        {
         
            return View("QSucseess");
        }

        [HttpPost]
        public ActionResult QGreetings(int EmpId, string Greet1, string Greet2, string Greet3)
        {
            DataBaseManager db = new DataBaseManager();
            Models.QuestionsModel mod = new Models.QuestionsModel();
            mod.Empl = db.GetEmployeeById(EmpId);
            var s = Request.Form;
            mod.IpAddress = Request.UserHostAddress;
            mod.CompName = Request.UserHostName;
            mod.UserAgent = Request.UserAgent;
            mod.Greet1 = Greet1;
            mod.Greet2 = Greet2;
            mod.Greet3 = Greet3;

            string Err = "";
            if (db.InsertResultGreet(mod, out Err))
            {
                //return QSucseess();
                return QSucseess();
            }
            else
            {
                mod.NotRight = true;
                mod.ErrorMessage = "Ошибка сохранения результатов. Попробуйте позже..." + Err;
            }

            return View(mod);
        }



        [HttpPost]
        public ActionResult Questions(int EmpId, List<string> Questions)
      //  public ActionResult Questions(List<CQuestion> Questions)
        {
            //string s = Q1;
            var s = Request.Form;
            DataBaseManager db = new DataBaseManager();
            Models.QuestionsModel mod = new Models.QuestionsModel();

            mod.Empl = db.GetEmployeeById(EmpId);
            //mod.Questions = db.GetQuestionsByAnketaId(Questioning.DataBaseManager.CurentAncetaId);
            mod.Questions = db.GetQuestionsByAnketaId();
            mod.AnswerTypes = db.AnswerType();
            if ((Questions == null) || ((mod.Questions.Count) != Questions.Count))
            {
                mod.NotRight = true;
                mod.ErrorMessage = "Необходимо ответить на все вопросы";
            }
            else
            {

                for (int i = 0; i < mod.Questions.Count; i++)
                {
                    mod.Questions[i].Result = Convert.ToInt32(Questions[i]);
                }
                mod.IpAddress = Request.UserHostAddress;
                mod.CompName = Request.UserHostName;
                mod.UserAgent = Request.UserAgent;
                if (db.InsertResult(mod))
                {
                    //return QSucseess();

                    if (db.NeedGreetings())
                    {
                        return QGreetings(EmpId);
                    }
                    else
                    {
                        return QSucseess();
                    }
                }
                else
                {
                    mod.ErrorMessage = "Ошибка сохранения результатов. Попробуйте позже...";
                }
            }
            
            return View(mod);

        }
        public ActionResult QGreetings(int EmpId)
        {

            

            DataBaseManager db = new DataBaseManager();
            
            if (db.IsVoiting2(EmpId))
            {
                SetEmployeeModel m = new SetEmployeeModel();
                m.ErrorMsg = "Вы уже заполнили анкету";

                return View("Index", m);
            }
            Models.QuestionsModel mod = new Models.QuestionsModel();
            mod.Empl = db.GetEmployeeById(EmpId);
            if (mod.Empl == null)
            {
                SetEmployeeModel m = new SetEmployeeModel();
                m.ErrorMsg = "Неверный код";

                return View("Index", m);
            }
            mod.NotRight = false;
            return View("QGreetings", mod);
        }

        public ActionResult Questions(string Num)
        {
            int mNum = 0;
            if (!Int32.TryParse(Num,out mNum))
            {
                SetEmployeeModel m = new SetEmployeeModel();
                m.ErrorMsg = "Ведите корректный код";

                return View("Index", m);
            }


        //    return QGreetings(mNum);
          
            DataBaseManager db = new DataBaseManager();

            if (db.IsVoiting(mNum))
            {
                SetEmployeeModel m = new SetEmployeeModel();
                m.ErrorMsg = "Вы уже заполнили анкету";

                return View("Index", m);
            }

            Models.QuestionsModel mod = new Models.QuestionsModel();



            mod.Empl = db.GetEmployeeById(mNum);
            if (mod.Empl == null)
            {
                SetEmployeeModel m = new SetEmployeeModel();
                m.ErrorMsg = "Неверный код";
                
                return View("Index",m);
            }
            mod.Questions = db.GetQuestionsByAnketaId();
            mod.NotRight = false;
            mod.AnswerTypes = db.AnswerType();
            return View(mod);
          
        }
    }
}