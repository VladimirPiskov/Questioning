using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ResultCreator
{
    class Program
    {
        static bool OneDep = false ;
        static int OneDepNum = 108;
        static void Main(string[] args)
        {
           // CreatePoorRestoransResultByEmp(270);
         CreateGreetResult();
         
            if (AnketaAnswerTypeId == 2)
            {
                List<int> WPos = new List<int>() { 18 };
                CreateAllYesNoResult(false, WPos);


            }
            else
            {
                //CreateAllRestoransResultAllBalls();
                //CreateAllResultAllballs();
                /*
                CreateAllPoorRestoransResult();
                CreateAllPoorResult();
                
                CreateAllRestoransResult();
                CreateAllResult();
                */

            }
         
        }




        public static int AnketaId
        {
            get
            {
                QDataDataContext QDB = new QDataDataContext();
                return QDB.QuestSettings.Where(a => a.Name == "CurentAnketaId").Single().ValueInt.Value;
            }
        }

        public static int AnketaAnswerTypeId
        {
            get
            {
                QDataDataContext QDB = new QDataDataContext();
                return QDB.QuestAnketa.Where(a => a.Id == AnketaId).Single().AnswerType;
                
            }
        }


        static void CreateGreetResult()
        {
            try
            {
                Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook Wb = app.Workbooks.Add(true);
                Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
                app.Visible = true;
                StaffDataDataContext SDB = new StaffDataDataContext();
                QDataDataContext QDB = new QDataDataContext();
                IQueryable<int> Deps = QDB.QuestAnswers.Select(a => a.EmpDepNum.Value).Distinct();
                IQueryable<QuestGreetings> QGr = QDB.QuestGreetings.Where(a => ((a.Greet1.Length > 2 || a.Greet2.Length > 2 || a.Greet3.Length > 2) && (a.AnketaId == AnketaId)));
                /*
                var res = QGr.Select(g => new
                {
                    g1 = g.Greet1,
                    g2 = g.Greet2,
                    g3 = g.Greet3,
                    Dn = SDB.StaffDepartments.Where(a => a.DepID == QDB.QuestAnswers.Where(b => b.EmpId == g.EmpId && b.AnketaId == AnketaId).First().EmpDepNum).First().DepName,
                    Pn = SDB.Staffposition.Where(a => a.POSITION_ID == QDB.QuestAnswers.Where(b => b.EmpId == g.EmpId && b.AnketaId == AnketaId).First().EmpDepPos).First().NAME,
                }
                );
                */
                int row = 1;

                int c = QDB.QuestGreetings.Where(a => ((a.Greet1.Length > 2 || a.Greet2.Length > 2 || a.Greet3.Length > 2) && (a.AnketaId == AnketaId))).Count();
                foreach (var G in QDB.QuestGreetings.Where(a => ((a.Greet1.Length > 2 || a.Greet2.Length > 2 || a.Greet3.Length > 2) && (a.AnketaId == AnketaId))))
                {
                    try
                    {
                        
                        int EmplId = G.EmpId;
                        int PosId = 0;
                        int DepNum = 0;
                        string DepName = "";
                        if (EmplId > 0)
                        {
                            PosId = QDB.QuestAnswers.Where(b => b.EmpId == G.EmpId && b.AnketaId == AnketaId).First().EmpDepPos.Value;
                            DepNum = QDB.QuestAnswers.Where(b => b.EmpId == G.EmpId && b.AnketaId == AnketaId).First().EmpDepNum.Value;
                        }
                        else
                        {
                            int dep = G.DepId.GetValueOrDefault(); ;
                            if (SDB.StaffDepartments.Any(a => a.DepID == dep))
                            {
                                DepName = SDB.StaffDepartments.FirstOrDefault(a => a.DepID == dep).DepName;
                            }
                        }
                        if (OneDep)
                        {
                            if (DepNum != OneDepNum) continue;
                        }


                        Ws.Cells[row, 1] = G.Greet1;
                        Ws.Cells[row, 2] = G.Greet2;
                        Ws.Cells[row, 3] = G.Greet3;
                        Ws.Cells[row, 4] = EmplId > 0? SDB.StaffDepartments.Where(a => a.DepID == DepNum).First().DepName: DepName;
                        Ws.Cells[row, 5] = EmplId > 0 ? SDB.Staffposition.Where(a => a.POSITION_ID == PosId).First().NAME:"";
                        Ws.Cells[row, 6] = EmplId > 0 ? SDB.StaffEmployee.Where(a => a.EMPLOYEE_ID == EmplId).First().LAST_NAME + " " + SDB.StaffEmployee.Where(a => a.EMPLOYEE_ID == EmplId).FirstOrDefault().FIRST_NAME:"Аноним";
                        Ws.Cells[row, 7] = EmplId;
                        row++;
                    }
                    catch (Exception e)
                    {
                        string s = e.Message;
                    }
                }

            }
            catch(Exception ee)
            {
                string ss = ee.Message;
            }
        }


        static void CreatePoorRestoransResultByEmp(int dep)
        {
            Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook Wb = app.Workbooks.Add(true);
            Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
            app.Visible = true;

            QDataDataContext QDB = new QDataDataContext();
            IQueryable<QuestAnswers> AllAnwers = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId);
            var PoorAnswEmps = AllAnwers.Where(a => (a.Result == 1||a.Result == 2) && (a.AnketaId == AnketaId)&&a.EmpDepNum==dep).Select(c => new { empId = c.EmpId.GetValueOrDefault(), posit = c.EmpDepPos.GetValueOrDefault() }).Distinct();
            int row = 1;

            StaffDataDataContext SDB = new StaffDataDataContext();
            foreach (var emp in PoorAnswEmps)
            {
                //Ws.Cells[row, 4] = SDB.StaffDepartments.Where(a => a.DepID == DepNum).First().DepName;
                Ws.Cells[row, 3] = SDB.Staffposition.Where(a => a.POSITION_ID == emp.posit).First().NAME;
                Ws.Cells[row, 2] = SDB.StaffEmployee.Where(a => a.EMPLOYEE_ID == emp.empId).First().LAST_NAME + " " + SDB.StaffEmployee.Where(a => a.EMPLOYEE_ID == emp.empId).FirstOrDefault().FIRST_NAME;
                row++;
                foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                {
                    //Ws.Cells[row, 1] = (row - 1).ToString();
                    Ws.Cells[row, 2] = QQ.Text;
                    Ws.Cells[row, 3] = AllAnwers.Where(a => (a.AnketaId == AnketaId) && a.EmpId == emp.empId && a.QuestionId == QQ.Id).FirstOrDefault().Result.ToString();
                    row++;
                }
                row++;
                row++;
            }






         
        }


        static void CreateAllPoorRestoransResult()
        {

            List<int> KitchenPos = new List<int>() { 2, 8, 4, 49, 121 };
            List<int> StoykaPos = new List<int>() { 5, 6, 12 };
            List<int> WPos = new List<int>() { 27, 3 };
            Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook Wb = app.Workbooks.Add(true);
            Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
            app.Visible = true;

            QDataDataContext QDB = new QDataDataContext();
            IQueryable<int> Deps = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId).Select(a => a.EmpDepNum.Value).Distinct();



            StaffDataDataContext SDB = new StaffDataDataContext();
            Ws.Name = "Все рестораны";
            Dictionary<int, Tuple<int, int>> Res = new Dictionary<int, Tuple<int, int>>();
            IQueryable<QuestAnswers> AllAnwers = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId);
            IQueryable<int> GoodAnsw = AllAnwers.Where(a => a.Result == 1 && (a.AnketaId == AnketaId)).Select(c => c.QuestionId.Value);
            int QCount = QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId).Count();

            int VCount = AllAnwers.Where(a => a.AnketaId == AnketaId).Count() / QCount;
            int row = 1;
            Ws.Cells[row, 1] = "Id";
            Ws.Cells[row, 2] = "Вопрос";
            Ws.Cells[row, 3] = "Всего";
            Ws.Cells[row, 4] = "Оценок 1";
            Ws.Cells[row, 5] = "____%____";
            row = 2;
            foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
            {
                Ws.Cells[row, 1] = (row - 1).ToString();
                Ws.Cells[row, 2] = QQ.Text;
                int FiveCount = GoodAnsw.Where(a => a == QQ.Id).Count();
                Ws.Cells[row, 3] = VCount.ToString();
                Ws.Cells[row, 4] = FiveCount.ToString();
                Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                row++;
            }

        }

        static void CreateAllRestoransResult()
        {

            List<int> KitchenPos = new List<int>() { 2, 8, 4, 49, 121 };
            List<int> StoykaPos = new List<int>() { 5, 6, 12 };
            List<int> WPos = new List<int>() { 27, 3 };
            Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook Wb = app.Workbooks.Add(true);
            Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
            app.Visible = true;

            QDataDataContext QDB = new QDataDataContext();
            IQueryable<int> Deps = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId).Select(a => a.EmpDepNum.Value).Distinct();

           

            StaffDataDataContext SDB = new StaffDataDataContext();
                Ws.Name = "Все рестораны";
                Dictionary<int, Tuple<int, int>> Res = new Dictionary<int, Tuple<int, int>>();
                IQueryable<QuestAnswers> AllAnwers = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId);
                IQueryable<int> GoodAnsw = AllAnwers.Where(a => a.Result == 5 && (a.AnketaId == AnketaId)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswK = AllAnwers.Where(a => a.Result == 5 && (a.AnketaId == AnketaId) && KitchenPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswS = AllAnwers.Where(a => a.Result == 5 && (a.AnketaId == AnketaId) && StoykaPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswW = AllAnwers.Where(a => a.Result == 5 && (a.AnketaId == AnketaId) && WPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                int QCount = QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId).Count();

                int VCount = AllAnwers.Where(a => a.AnketaId == AnketaId).Count() / QCount;
                int row = 1;
                Ws.Cells[row, 1] = "Id";
                Ws.Cells[row, 2] = "Вопрос";
                Ws.Cells[row, 3] = "Всего";
                Ws.Cells[row, 4] = "Оценок 5";
                Ws.Cells[row, 5] = "____%____";
                row = 2;
                foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                {
                    Ws.Cells[row, 1] = (row - 1).ToString();
                    Ws.Cells[row, 2] = QQ.Text;
                    int FiveCount = GoodAnsw.Where(a => a == QQ.Id).Count();
                    Ws.Cells[row, 3] = VCount.ToString();
                    Ws.Cells[row, 4] = FiveCount.ToString();
                    Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                    row++;
                }
            row++;
            if (!AllPosOnly)
            {
                VCount = AllAnwers.Where(a => KitchenPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                Ws.Cells[row, 2] = "Кухня";
                row++;
                if (VCount > 0)
                {
                    foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                    {
                        Ws.Cells[row, 1] = (row - 1).ToString();
                        Ws.Cells[row, 2] = QQ.Text;
                        int FiveCount = GoodAnswK.Where(a => a == QQ.Id).Count();
                        Ws.Cells[row, 3] = VCount.ToString();
                        Ws.Cells[row, 4] = FiveCount.ToString();
                        Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                        row++;
                    }
                }
                row++;
                VCount = AllAnwers.Where(a => StoykaPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                if (VCount > 0)
                {
                    Ws.Cells[row, 2] = "Стойка";
                    row++;

                    foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                    {
                        Ws.Cells[row, 1] = (row - 1).ToString();
                        Ws.Cells[row, 2] = QQ.Text;
                        int FiveCount = GoodAnswS.Where(a => a == QQ.Id).Count();
                        Ws.Cells[row, 3] = VCount.ToString();
                        Ws.Cells[row, 4] = FiveCount.ToString();
                        Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                        row++;
                    }
                }
                row++;
                VCount = AllAnwers.Where(a => WPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                if (VCount > 0)
                {
                    Ws.Cells[row, 2] = "Официанты";
                    row++;
                    foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                    {
                        Ws.Cells[row, 1] = (row - 1).ToString();
                        Ws.Cells[row, 2] = QQ.Text;
                        int FiveCount = GoodAnswW.Where(a => a == QQ.Id).Count();
                        Ws.Cells[row, 3] = VCount.ToString();
                        Ws.Cells[row, 4] = FiveCount.ToString();
                        Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                        row++;
                    }
                }
            }

        }


        static void CreateAllResultAllballs()
        {

            List<int> KitchenPos = new List<int>() { 2, 8, 4, 49, 121 };
            List<int> StoykaPos = new List<int>() { 5, 6, 12 };
            List<int> WPos = new List<int>() { 27, 3 };
            Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook Wb = app.Workbooks.Add(true);
            Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
            app.Visible = true;

            QDataDataContext QDB = new QDataDataContext();
            IQueryable<int> Deps = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId).Select(a => a.EmpDepNum.Value).Distinct();

            int row = 1;

            foreach (int Dep in Deps)
            {
                StaffDataDataContext SDB = new StaffDataDataContext();
                string DName = $"Подразд. № { Dep.ToString()}";
                if (SDB.StaffDepartments.Any(a => a.DepID == Dep))
                {
                
                    DName = SDB.StaffDepartments.Where(a => a.DepID == Dep).First().DepName;
                }
                else
                {
                    continue;
                }
                Ws.Name = DName.Substring(0, Math.Min(DName.Count(), 15));
                Dictionary<int, Tuple<int, int>> Res = new Dictionary<int, Tuple<int, int>>();
                IQueryable<QuestAnswers> AllAnwers = QDB.QuestAnswers.Where(a => a.EmpDepNum == Dep && a.AnketaId == AnketaId && a.Id > 99326);
                int QCount = QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId).Count();

                int VCountAll = AllAnwers.Where(a => a.AnketaId == AnketaId).Count() / QCount;
                
                Ws.Cells[1, 1] = "Id";
                Ws.Cells[1, 2] = "Вопрос";
                Ws.Cells[1, 3] = "Всего";



                for (int i = 5; i > 0; i--)
                {
                    row = 1;
                    IQueryable<int> GoodAnsw = AllAnwers.Where(a => a.Result == i && (a.AnketaId == AnketaId)).Select(c => c.QuestionId.Value);
                    IQueryable<int> GoodAnswK = AllAnwers.Where(a => a.Result == i && (a.AnketaId == AnketaId) && KitchenPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                    IQueryable<int> GoodAnswS = AllAnwers.Where(a => a.Result == i && (a.AnketaId == AnketaId) && StoykaPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                    IQueryable<int> GoodAnswW = AllAnwers.Where(a => a.Result == i && (a.AnketaId == AnketaId) && WPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);

                    int col = 4 + 2 * (i - 1);
                    Ws.Cells[row, col] = $"Оценок {i}";
                    Ws.Cells[row, col+1] = "____%____";
                    row = 2;
                    foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                    {
                        Ws.Cells[row, 1] = (row - 1).ToString();
                        Ws.Cells[row, 2] = QQ.Text;
                        int FiveCount = GoodAnsw.Where(a => a == QQ.Id).Count();
                        Ws.Cells[row, 3] = VCountAll.ToString();
                        Ws.Cells[row, col] = FiveCount.ToString();
                        Ws.Cells[row, col+1] = (FiveCount * 100) / VCountAll;
                        row++;
                    }

                    row++;
                    if (!AllPosOnly)
                    {
                      int  VCount = AllAnwers.Where(a => KitchenPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                        Ws.Cells[row, 2] = "Кухня";
                        row++;
                        if (VCount > 0)
                        {
                            foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                            {
                                Ws.Cells[row, 1] = (row - 1).ToString();
                                Ws.Cells[row, 2] = QQ.Text;
                                int FiveCount = GoodAnswK.Where(a => a == QQ.Id).Count();
                                Ws.Cells[row, 3] = VCount.ToString();
                                Ws.Cells[row, col] = FiveCount.ToString();
                                Ws.Cells[row, col+1] = (FiveCount * 100) / VCount;
                                row++;
                            }
                        }
                        row++;
                        VCount = AllAnwers.Where(a => StoykaPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                        if (VCount > 0)
                        {
                            Ws.Cells[row, 2] = "Стойка";
                            row++;

                            foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                            {
                                Ws.Cells[row, 1] = (row - 1).ToString();
                                Ws.Cells[row, 2] = QQ.Text;
                                int FiveCount = GoodAnswS.Where(a => a == QQ.Id).Count();
                                Ws.Cells[row, 3] = VCount.ToString();
                                Ws.Cells[row, col] = FiveCount.ToString();
                                Ws.Cells[row, col+1] = (FiveCount * 100) / VCount;
                                row++;
                            }
                        }
                        row++;
                        VCount = AllAnwers.Where(a => WPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                        if (VCount > 0)
                        {
                            Ws.Cells[row, 2] = "Официанты";
                            row++;
                            foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId ))
                            {
                                Ws.Cells[row, 1] = (row - 1).ToString();
                                Ws.Cells[row, 2] = QQ.Text;
                                int FiveCount = GoodAnswW.Where(a => a == QQ.Id).Count();
                                Ws.Cells[row, 3] = VCount.ToString();
                                Ws.Cells[row, col] = FiveCount.ToString();
                                Ws.Cells[row, col+1] = (FiveCount * 100) / VCount;
                                row++;
                            }
                        }
                    }
                    

                }
                Ws.get_Range("A1:Z1").EntireColumn.AutoFit();
                row++;
                var comm = AllAnwers.Where(a => a.Comment.Length > 5).Select(a => new { q = QDB.QuestQuestions.SingleOrDefault(b => b.Id == a.QuestionId).Text, comm = a.Comment });
                foreach (var c in comm)
                {
                    Ws.Cells[row, 2] = c.q;
                    Ws.Cells[row, 3] = c.comm;
                    row++;

                }



                Ws = Wb.Sheets.Add();

            }
        }

        static void CreateAllRestoransResultAllBalls()
        {

            List<int> KitchenPos = new List<int>() { 2, 8, 4, 49, 121 };
            List<int> StoykaPos = new List<int>() { 5, 6, 12 };
            List<int> WPos = new List<int>() { 27, 3 };
            Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook Wb = app.Workbooks.Add(true);
            Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
            app.Visible = true;

            QDataDataContext QDB = new QDataDataContext();
            IQueryable<int> Deps = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId).Select(a => a.EmpDepNum.Value).Distinct();



            StaffDataDataContext SDB = new StaffDataDataContext();
            Ws.Name = "Все рестораны";
            Dictionary<int, Tuple<int, int>> Res = new Dictionary<int, Tuple<int, int>>();
            IQueryable<QuestAnswers> AllAnwers = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId && a.Id> 99326);
            int QCount = QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId).Count();
            int VCountAll = AllAnwers.Where(a => a.AnketaId == AnketaId).Count() / QCount;
            Ws.Cells[1, 1] = "Id";
            Ws.Cells[1, 2] = "Вопрос";
            Ws.Cells[1, 3] = "Всего";
            for (int i = 5; i > 0; i--)
            {
                IQueryable<int> GoodAnsw = AllAnwers.Where(a => a.Result == i && (a.AnketaId == AnketaId)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswK = AllAnwers.Where(a => a.Result == i && (a.AnketaId == AnketaId) && KitchenPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswS = AllAnwers.Where(a => a.Result == i && (a.AnketaId == AnketaId) && StoykaPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswW = AllAnwers.Where(a => a.Result == i && (a.AnketaId == AnketaId) && WPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                

                
                int row = 1;
                int col = 4 + 2 * (i - 1);
                Ws.Cells[row, col] = $"Оценок {i}";
                Ws.Cells[row, col+1] = "____%____";
                row = 2;
                foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                {
                    Ws.Cells[row, 1] = (row - 1).ToString();
                    Ws.Cells[row, 2] = QQ.Text;
                    int FiveCount = GoodAnsw.Where(a => a == QQ.Id).Count();
                    Ws.Cells[row, 3] = VCountAll.ToString();
                    Ws.Cells[row, col] = FiveCount.ToString();
                    Ws.Cells[row, col+1] = (FiveCount * 100) / VCountAll;
                    row++;
                }
                row++;
                if (!AllPosOnly)
                {
                    int VCountPos = AllAnwers.Where(a => KitchenPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    Ws.Cells[row, 2] = "Кухня";
                    row++;
                    if (VCountPos > 0)
                    {
                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswK.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCountPos.ToString();
                            Ws.Cells[row, col] = FiveCount.ToString();
                            Ws.Cells[row, col+1] = (FiveCount * 100) / VCountPos;
                            row++;
                        }
                    }
                    row++;
                    VCountPos = AllAnwers.Where(a => StoykaPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    if (VCountPos > 0)
                    {
                        Ws.Cells[row, 2] = "Стойка";
                        row++;

                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswS.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCountPos.ToString();
                            Ws.Cells[row, col] = FiveCount.ToString();
                            Ws.Cells[row, col+1] = (FiveCount * 100) / VCountPos;
                            row++;
                        }
                    }
                    row++;
                    VCountPos = AllAnwers.Where(a => WPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    if (VCountPos > 0)
                    {
                        Ws.Cells[row, 2] = "Официанты";
                        row++;
                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswW.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCountPos.ToString();
                            Ws.Cells[row, col] = FiveCount.ToString();
                            Ws.Cells[row, col+1] = (FiveCount * 100) / VCountPos;
                            row++;
                        }
                    }
                }
            }

        }



        static bool AllPosOnly = false;
            static void CreateAllResult()
        {

            List<int> KitchenPos = new List<int>() {2,8,4,49,121 };
            List<int> StoykaPos = new List<int>() { 5,6,12};
            List<int> WPos = new List<int>() { 27,3 };
            Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook Wb = app.Workbooks.Add(true);
            Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
            app.Visible = true;

            QDataDataContext QDB = new QDataDataContext();
            IQueryable<int> Deps = QDB.QuestAnswers.Where(a=>a.AnketaId == AnketaId).Select(a => a.EmpDepNum.Value).Distinct();



            foreach (int Dep in Deps)
            {
                StaffDataDataContext SDB = new StaffDataDataContext();
                string DName = SDB.StaffDepartments.Where(a => a.DepID == Dep).First().DepName;
                Ws.Name = DName.Substring(0,Math.Min(DName.Count(), 20));
                Dictionary<int, Tuple<int, int>> Res = new Dictionary<int, Tuple<int, int>>();
                IQueryable< QuestAnswers > AllAnwers = QDB.QuestAnswers.Where(a => a.EmpDepNum == Dep && a.AnketaId == AnketaId);
                IQueryable<int> GoodAnsw = AllAnwers.Where(a => a.Result == 5 && (a.AnketaId == AnketaId)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswK = AllAnwers.Where(a => a.Result == 5 && (a.AnketaId == AnketaId) && KitchenPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswS = AllAnwers.Where(a => a.Result == 5 && (a.AnketaId == AnketaId) && StoykaPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswW = AllAnwers.Where(a => a.Result == 5 && (a.AnketaId == AnketaId) && WPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                int QCount = QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId).Count();

                int VCount = AllAnwers.Where(a => a.AnketaId == AnketaId).Count() / QCount;
                int row = 1;
                Ws.Cells[row, 1] = "Id";
                Ws.Cells[row, 2] = "Вопрос";
                Ws.Cells[row, 3] = "Всего";
                Ws.Cells[row, 4] = "Оценок 5";
                Ws.Cells[row, 5] = "____%____";
                row = 2;
                foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                {
                    Ws.Cells[row, 1] = (row-1).ToString();
                    Ws.Cells[row, 2] = QQ.Text;
                    int FiveCount = GoodAnsw.Where(a => a == QQ.Id).Count();
                    Ws.Cells[row, 3] = VCount.ToString();
                    Ws.Cells[row, 4] = FiveCount.ToString();
                    Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                    row++;
                }

                row++;
                if (!AllPosOnly)
                {
                    VCount = AllAnwers.Where(a => KitchenPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    Ws.Cells[row, 2] = "Кухня";
                    row++;
                    if (VCount > 0)
                    {
                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswK.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCount.ToString();
                            Ws.Cells[row, 4] = FiveCount.ToString();
                            Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                            row++;
                        }
                    }
                    row++;
                    VCount = AllAnwers.Where(a => StoykaPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    if (VCount > 0)
                    {
                        Ws.Cells[row, 2] = "Стойка";
                        row++;

                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswS.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCount.ToString();
                            Ws.Cells[row, 4] = FiveCount.ToString();
                            Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                            row++;
                        }
                    }
                    row++;
                    VCount = AllAnwers.Where(a => WPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    if (VCount > 0)
                    {
                        Ws.Cells[row, 2] = "Официанты";
                        row++;
                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswW.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCount.ToString();
                            Ws.Cells[row, 4] = FiveCount.ToString();
                            Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                            row++;
                        }
                    }
                }
                Ws.get_Range("A1:Z1").EntireColumn.AutoFit();
                Ws = Wb.Sheets.Add();
                

            }
            }


        static void CreateAllYesNoResult(bool AllPos, List<int> WPos)
        {
            
            Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook Wb = app.Workbooks.Add(true);
            Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
            app.Visible = true;
            QDataDataContext QDB = new QDataDataContext();
            IQueryable<int> Deps = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId).Select(a => a.EmpDepNum.Value).Distinct();

            foreach (int Dep in Deps)
            {
                StaffDataDataContext SDB = new StaffDataDataContext();
                string DName = SDB.StaffDepartments.Where(a => a.DepID == Dep).First().DepName;
                Ws.Name = DName.Substring(0, Math.Min(DName.Count(), 20));
                Dictionary<int, Tuple<int, int>> Res = new Dictionary<int, Tuple<int, int>>();
                IQueryable<QuestAnswers> AllAnwers = QDB.QuestAnswers.Where(a => a.EmpDepNum == Dep && a.AnketaId == AnketaId && WPos.Contains(a.EmpDepPos.Value));

                if (AllPos)
                {
                    AllAnwers = QDB.QuestAnswers.Where(a => a.EmpDepNum == Dep && a.AnketaId == AnketaId );
                }
                CreateWsPage(Ws, AllAnwers, QDB);
                /*
                IQueryable<int> GoodAnsw = AllAnwers.Where(a => a.Result == 5).Select(c => c.QuestionId.Value);
                //IQueryable<int> GoodAnswW = AllAnwers.Where(a => a.Result == 1 && (a.AnketaId == AnketaId) && WPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                int QCount = QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId).Count();
                int VCount = AllAnwers.Where(a => a.AnketaId == AnketaId).Count() / QCount;

                int row = 1;
                Ws.Cells[row, 1] = "Id";
                Ws.Cells[row, 2] = "Вопрос";
                Ws.Cells[row, 3] = "Всего";
                Ws.Cells[row, 4] = "Ответов Да";
                Ws.Cells[row, 5] = "____%____";
                Ws.Cells[row, 6] = "Ответов Нет";
                Ws.Cells[row, 7] = "____%____";
                row = 2;
                foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                {
                    Ws.Cells[row, 1] = (row - 1).ToString();
                    Ws.Cells[row, 2] = QQ.Text;
                    int FiveCount = GoodAnsw.Where(a => a == QQ.Id).Count();
                    Ws.Cells[row, 3] = VCount.ToString();
                    Ws.Cells[row, 4] = FiveCount.ToString();
                    Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                    Ws.Cells[row, 6] = (VCount-FiveCount).ToString();
                    Ws.Cells[row, 7] = 100- (FiveCount * 100) / VCount;
                    row++;
                }

                row++;
                
                Ws.get_Range("A1:Z1").EntireColumn.AutoFit();
                */
                Ws = Wb.Sheets.Add();
            }
                Ws.Name ="Все рестораны";
            IQueryable<QuestAnswers> AllAnwers2 = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId && WPos.Contains(a.EmpDepPos.Value));

            if (AllPos)
            {
                AllAnwers2 = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId);
            }
            CreateWsPage(Ws, AllAnwers2, QDB);


        }

        private static void CreateWsPage(Worksheet Ws, IQueryable<QuestAnswers> AllAnwers, QDataDataContext QDB)
        {
            IQueryable<int> GoodAnsw = AllAnwers.Where(a => a.Result == 5).Select(c => c.QuestionId.Value);
            int QCount = QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId).Count();
            int VCount = AllAnwers.Where(a => a.AnketaId == AnketaId).Count() / QCount;

            int row = 1;
            Ws.Cells[row, 1] = "Id";
            Ws.Cells[row, 2] = "Вопрос";
            Ws.Cells[row, 3] = "Всего";
            Ws.Cells[row, 4] = "Ответов Да";
            Ws.Cells[row, 5] = "____%____";
            Ws.Cells[row, 6] = "Ответов Нет";
            Ws.Cells[row, 7] = "____%____";
            row = 2;
            foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
            {
                Ws.Cells[row, 1] = (row - 1).ToString();
                Ws.Cells[row, 2] = QQ.Text;
                int FiveCount = GoodAnsw.Where(a => a == QQ.Id).Count();
                Ws.Cells[row, 3] = VCount.ToString();
                Ws.Cells[row, 4] = FiveCount.ToString();
                Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                Ws.Cells[row, 6] = (VCount - FiveCount).ToString();
                Ws.Cells[row, 7] = 100 - (FiveCount * 100) / VCount;
                row++;
            }

            row++;

            Ws.get_Range("A1:Z1").EntireColumn.AutoFit();

        }


        static void CreateAllPoorResult()
        {

            List<int> KitchenPos = new List<int>() { 2, 8, 4, 49, 121 };
            List<int> StoykaPos = new List<int>() { 5, 6, 12 };
            List<int> WPos = new List<int>() { 27, 3 };
            Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook Wb = app.Workbooks.Add(true);
            Worksheet Ws = (Microsoft.Office.Interop.Excel.Worksheet)Wb.ActiveSheet;
            app.Visible = true;

            QDataDataContext QDB = new QDataDataContext();
            IQueryable<int> Deps = QDB.QuestAnswers.Where(a => a.AnketaId == AnketaId).Select(a => a.EmpDepNum.Value).Distinct();

            foreach (int Dep in Deps)
            {
                StaffDataDataContext SDB = new StaffDataDataContext();
                string DName = SDB.StaffDepartments.Where(a => a.DepID == Dep).First().DepName;
                Ws.Name = DName.Substring(0, Math.Min(DName.Count(), 20));
                Dictionary<int, Tuple<int, int>> Res = new Dictionary<int, Tuple<int, int>>();
                IQueryable<QuestAnswers> AllAnwers = QDB.QuestAnswers.Where(a => a.EmpDepNum == Dep && a.AnketaId == AnketaId);
                IQueryable<int> GoodAnsw = AllAnwers.Where(a => a.Result == 1 && (a.AnketaId == AnketaId)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswK = AllAnwers.Where(a => a.Result == 1 && (a.AnketaId == AnketaId) && KitchenPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswS = AllAnwers.Where(a => a.Result == 1 && (a.AnketaId == AnketaId) && StoykaPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                IQueryable<int> GoodAnswW = AllAnwers.Where(a => a.Result == 1 && (a.AnketaId == AnketaId) && WPos.Contains(a.EmpDepPos.Value)).Select(c => c.QuestionId.Value);
                int QCount = QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId).Count();

                int VCount = AllAnwers.Where(a => a.AnketaId == AnketaId).Count() / QCount;
                int row = 1;
                Ws.Cells[row, 1] = "Id";
                Ws.Cells[row, 2] = "Вопрос";
                Ws.Cells[row, 3] = "Всего";
                Ws.Cells[row, 4] = "Оценок 1";
                Ws.Cells[row, 5] = "____%____";
                row = 2;
                foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                {
                    Ws.Cells[row, 1] = (row - 1).ToString();
                    Ws.Cells[row, 2] = QQ.Text;
                    int FiveCount = GoodAnsw.Where(a => a == QQ.Id).Count();
                    Ws.Cells[row, 3] = VCount.ToString();
                    Ws.Cells[row, 4] = FiveCount.ToString();
                    Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                    row++;
                }

                row++;
                if (!AllPosOnly)
                {
                    VCount = AllAnwers.Where(a => KitchenPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    Ws.Cells[row, 2] = "Кухня";
                    row++;
                    if (VCount > 0)
                    {
                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswK.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCount.ToString();
                            Ws.Cells[row, 4] = FiveCount.ToString();
                            Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                            row++;
                        }
                    }
                    row++;
                    VCount = AllAnwers.Where(a => StoykaPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    if (VCount > 0)
                    {
                        Ws.Cells[row, 2] = "Стойка";
                        row++;

                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswS.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCount.ToString();
                            Ws.Cells[row, 4] = FiveCount.ToString();
                            Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                            row++;
                        }
                    }
                    row++;
                    VCount = AllAnwers.Where(a => WPos.Contains(a.EmpDepPos.Value) && a.AnketaId == AnketaId).Count() / QCount;
                    if (VCount > 0)
                    {
                        Ws.Cells[row, 2] = "Официанты";
                        row++;
                        foreach (QuestQuestions QQ in QDB.QuestQuestions.Where(a => a.AnketaId == AnketaId))
                        {
                            Ws.Cells[row, 1] = (row - 1).ToString();
                            Ws.Cells[row, 2] = QQ.Text;
                            int FiveCount = GoodAnswW.Where(a => a == QQ.Id).Count();
                            Ws.Cells[row, 3] = VCount.ToString();
                            Ws.Cells[row, 4] = FiveCount.ToString();
                            Ws.Cells[row, 5] = (FiveCount * 100) / VCount;
                            row++;
                        }
                    }
                }
                Ws.get_Range("A1:Z1").EntireColumn.AutoFit();
                Ws = Wb.Sheets.Add();


            }


        }
    }

}
