using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Questioning.Models;
using System.Data.Odbc;
using System.Data;


namespace Questioning
{
    

    public class DataBaseManager
    {
        //private StaffBaseDataContext _StaffBaseDataContext;
        private VoitingBaseDataContext _VoitingBaseDataContext;
        string StaffConnStr = "Driver={Progress OpenEdge 10.1B driver};HOST=web;DB=staff;UID=sysprogress;PWD=progress;PORT=2520;";
        public DataBaseManager()
        {
            //_StaffBaseDataContext = new StaffBaseDataContext();
            _VoitingBaseDataContext = new VoitingBaseDataContext();
        }




        internal CEmployee GetEmployeeById(int EmplId)
        {


            OdbcConnection Conn = new OdbcConnection(StaffConnStr);
            Conn.Open();
            string CommandStr = "SELECT   PUB.EMPLOYEE.EMPLOYEE_ID, PUB.EMPLOYEE.LAST_NAME, PUB.EMPLOYEE.FIRST_NAME, PUB.EMPLOYEE.MIDDLE_NAME, PUB.EMPLOYEE.PHONE_EMAIL, " +
                         "PUB.EMPLOYEE.UPDATE_DATE, PUB.EMPLOYEE.ENTRY_DATE, PUB.EMPLOYEE.DISMISSAL_DATE, PUB.EMPLOYEE.DISMISSAL_REASON, " +
                         "PUB.EMPLOYEE.PAYROLL_TYPE_ID, PUB.EMPLOYEE.CODE, PUB.EMPLOYEE.ENROL_DATE, PUB.EMPLOYEE.SALARY, PUB.EMPLOYEE.POSITION_ID, " +
                         "PUB.EMPLOYEE.SUBDIVISION_ID, PUB.EMPLOYEE.SALARY_FROM_DATE, PUB.EMPLOYEE.BIRTHDAY, PUB.EMPLOYEE.Size_id, PUB.EMPLOYEE.Sex " +
                         
"FROM            PUB.EMPLOYEE " +
"WHERE   dismissal_date is null  and   PUB.EMPLOYEE.EMPLOYEE_ID = " + EmplId.ToString();


            OdbcCommand Comm = new OdbcCommand(CommandStr, Conn);

            OdbcDataReader OdR = Comm.ExecuteReader();

            CEmployee Emp = new CEmployee();

            if (!OdR.Read())
            {
                return null;

            }

         else
            {
                try
                {


                    Emp.Id = OdR.GetInt32(0);

                    
                    Emp.DepId = OdR.GetInt32(14);
                    
                    Emp.FirstName = OdR.GetString(2);
                    Emp.LastName = OdR.GetString(1);
                    Emp.MiddleName = OdR.GetString(3);

                    Emp.PosId= OdR.GetInt32(13);
}
                catch
                {
                }
            }


            Conn.Close();
            return Emp;

        }


        public int AnketaId
        {
            get
            {
                return _VoitingBaseDataContext.QuestSettings.Where(a => a.Name == "CurentAnketaId").Single().ValueInt.Value;
            }
        }

        private List<int> depList = null;

        private List<int> GetdepList()
        {
            return new List<int>();
        }
        public bool DepCorrect(int dep)
        {
            if (depList == null)
            {
                depList = GetdepList();
            }
            return true;
        }


        public bool IsVoiting(int EmpId)
        {
            return _VoitingBaseDataContext.QuestAnswers.Where(c => c.EmpId == EmpId && c.AnketaId== AnketaId).Count() >0;
        }

        public bool IsVoiting2(int EmpId)
        {
            return _VoitingBaseDataContext.QuestGreetings.Where(c => c.EmpId == EmpId && c.AnketaId == AnketaId).Count() > 0;
        }


        public bool InsertResultGreet(QuestionsModel Res,out string Err)
        {
            Err = "";
            try
            {
                if ((Res.Empl.Id > 0) && (_VoitingBaseDataContext.QuestGreetings.Any(a => a.EmpId == Res.Empl.Id && a.AnketaId==AnketaId)))
                {
                    return true;
                }

                QuestGreetings qg = new QuestGreetings()
                {
                    Greet1 = Res.Greet1.TrimEnd(),  
                    Greet2 = Res.Greet2.TrimEnd(),
                    Greet3 = Res.Greet3.TrimEnd(),
                    EmpId = Res.Empl.Id,
                    AnswerDate = DateTime.Now,
                    CompName = Res.CompName,
                    IP = Res.IpAddress,
                    Agent = Res.UserAgent,
                    AnketaId = AnketaId,
                    DepId = Res.Dep,
                    PosId =Res.Pos,
                    GuidId =Res.GuidId
                    

                };
                _VoitingBaseDataContext.QuestGreetings.InsertOnSubmit(qg);
                _VoitingBaseDataContext.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                Err = e.Message;
                return false;
            }
        }

        public bool NeedGreetings()
        {
            return (_VoitingBaseDataContext.QuestAnketa.Where(a => a.Id == AnketaId).Single().NeedGreetings);
        }

        public int AnswerType()
        {
            return (_VoitingBaseDataContext.QuestAnketa.Where(a => a.Id == AnketaId).Single().AnswerType);
        }


        public bool InsertResult(QuestionsModel Res, out string  guid)
        {
            Guid g = Guid.NewGuid();
            guid = g.ToString();
            try
            {
                
                if ((Res.Empl.Id>0) && (_VoitingBaseDataContext.QuestAnswers.Where(a => a.EmpId == Res.Empl.Id && a.AnketaId== AnketaId).Count() > 0))
                {
                    return true;
                }

                
                foreach (CQuestion Q in Res.Questions)
                {
                    QuestAnswers Qa = new QuestAnswers()
                    {
                        //AnketaId = Res.AnketaId,
                        AnketaId = AnketaId,
                        EmpDepNum = Res.Empl.DepId,
                        AnswerDate = DateTime.Now,
                        EmpDepPos = Res.Empl.PosId,
                        EmpId = Res.Empl.Id,
                        QuestionId = Q.QId,
                        Result = Q.Result,
                        Ip = Res.IpAddress,
                        CompName = Res.CompName,
                        Agent = Res.UserAgent,
                        Comment = Q.Comment,
                        SessionId = guid
                    };
                    _VoitingBaseDataContext.QuestAnswers.InsertOnSubmit(Qa);
                }
                _VoitingBaseDataContext.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Models.CQuestion> GetQuestionsByAnketaId()
        {
            List<Models.CQuestion> Tmp = new List<Models.CQuestion>();
            IQueryable<QuestQuestions> Qests = _VoitingBaseDataContext.QuestQuestions.Where(c => c.QuestAnketa.Id == AnketaId).OrderBy(a=>a.Id);
            int i = 1;
            foreach (QuestQuestions q in Qests)
            {
                Models.CQuestion nq = new Models.CQuestion()
                {
                    AnketaId = q.AnketaId.Value,
                    QId = q.Id,
                    QText = q.Text,
                    NumberInAnketa = i
                };
                i++;
                Tmp.Add(nq);
            }
            return Tmp;


        }

    }
}