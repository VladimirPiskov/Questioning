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
        public ActionResult QGreetings(int EmpId, int depId, int selectedPos, string Greet1, string Greet2, string Greet3, string guidId)
        {
            DataBaseManager db = new DataBaseManager();
            Models.QuestionsModel mod = new Models.QuestionsModel();
            if (EmpId > 0)
            {
                mod.Empl = db.GetEmployeeById(EmpId);
            }
            else
            {
                mod.Empl = new CEmployee()
                {
                    DepId = depId,
                    Id = -1,
                    FirstName = "Аноним",
                    PosId = selectedPos
                };
                mod.Dep = depId;
                mod.Pos = selectedPos;
            }
            var s = Request.Form;
            mod.IpAddress = Request.UserHostAddress;
            mod.CompName = Request.UserHostName;
            mod.UserAgent = Request.UserAgent;
            mod.Greet1 = Greet1;
            mod.Greet2 = Greet2;
            mod.Greet3 = Greet3;
            mod.GuidId = guidId;

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
        public ActionResult Questions(int EmpId,  int depId, int selectedPos,List<string> Questions, List<string> QuestionsReasons)
      //  public ActionResult Questions(List<CQuestion> Questions)
        {
            //string s = Q1;
            var s = Request.Form;
            DataBaseManager db = new DataBaseManager();
            Models.QuestionsModel mod = new Models.QuestionsModel();
            if (EmpId > 0)
            {
                mod.Empl = db.GetEmployeeById(EmpId);
            }
            else
            {
                mod.Empl = new CEmployee()
                {
                    DepId = depId,
                    Id = -1,
                    FirstName = "Аноним",
                    PosId = selectedPos
                };
            }
            mod.Dep = depId;
            mod.Pos = selectedPos;
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
                    mod.Questions[i].Comment = QuestionsReasons[i];
                }
                mod.IpAddress = Request.UserHostAddress;
                mod.CompName = Request.UserHostName;
                mod.UserAgent = Request.UserAgent;
                string g = "";
                if (db.InsertResult(mod,out g))
                {
                    //return QSucseess();
                    
                    if (db.NeedGreetings())
                    {
                        return QGreetings(EmpId,depId,selectedPos,g);
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
        public ActionResult QGreetings(int EmpId, int depId, int selectedPos,string guidId)
        {
            DataBaseManager db = new DataBaseManager();
            Models.QuestionsModel mod = new Models.QuestionsModel()
            {
                GuidId = guidId
            };
            if (EmpId > 0)
            {
                if (db.IsVoiting2(EmpId))
                {
                    SetEmployeeModel m = new SetEmployeeModel();
                    m.ErrorMsg = "Вы уже заполнили анкету";

                    return View("Index", m);
                }
                
                mod.Empl = db.GetEmployeeById(EmpId);
                if (mod.Empl == null)
                {
                    SetEmployeeModel m = new SetEmployeeModel();
                    m.ErrorMsg = "Неверный код";

                    return View("Index", m);
                }
                mod.NotRight = false;
            }
            else
            {
                mod.Empl = new CEmployee()
                {
                    DepId = depId,
                    Id = -1,
                    FirstName = "Аноним",
                    PosId = selectedPos
                };
                mod.Dep = depId;
                mod.Pos = selectedPos;
                mod.NotRight = false;
            }
            return View("QGreetings", mod);
        }

        public ActionResult Questions(string Num, bool anon, string depId, string selectedPos)
        {
            DataBaseManager db = new DataBaseManager();
            int mNum = 0;
            int depNum = 0;
            if (anon)
            {
                
                if (!Int32.TryParse(depId, out depNum))
                {
                    SetEmployeeModel m = new SetEmployeeModel();
                    m.ErrorMsg = "Ведите номер подразделения";

                    return View("Index", m);
                }
                if (!db.DepCorrect(depNum))
                {
                    SetEmployeeModel m = new SetEmployeeModel();
                    m.ErrorMsg = "Некорректный номер подразделения";
                    return View("Index", m);
                }
            }
            else
            {
                if (!Int32.TryParse(Num, out mNum))
                {
                    SetEmployeeModel m = new SetEmployeeModel();
                    m.ErrorMsg = "Ведите корректный код";
                    return View("Index", m);
                }

                if (db.IsVoiting(mNum))
                {
                    SetEmployeeModel m = new SetEmployeeModel();
                    m.ErrorMsg = "Вы уже заполнили анкету";

                    return View("Index", m);
                }
            }

            Models.QuestionsModel mod = new Models.QuestionsModel();


            
            //mod.Dep = depNum;
            //mod.Pos = posId;
            if (anon)
            {
                mod.Empl = new CEmployee()
                {
                    DepId = depNum,
                    Id = -1,
                    FirstName = "Аноним",
                    PosId = Convert.ToInt32(SetEmployeeModel.PosItems.IndexOf(selectedPos))
                };
                mod.Pos = Convert.ToInt32(SetEmployeeModel.PosItems.IndexOf(selectedPos));
                mod.Dep = depNum;
            }
            else
            {
                mod.Empl = db.GetEmployeeById(mNum);
                if (!anon && mod.Empl == null)
                {
                    SetEmployeeModel m = new SetEmployeeModel();
                    m.ErrorMsg = "Неверный код";

                    return View("Index", m);
                }
            }
            mod.Questions = db.GetQuestionsByAnketaId();
            mod.NotRight = false;
            mod.AnswerTypes = db.AnswerType();
            return View(mod);
          
        }
    }
}