using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestioningAdmin.Models;
using System.Data.Odbc;
using System.Data;


namespace QuestioningAdmin
{
    public class ToBase
    {
        public ToBase()
        {
            //_StaffBaseDataContext = new StaffBaseDataContext();
            _VoitingBaseDataContext = new VoitingBaseDataContext();
        }
        private VoitingBaseDataContext _VoitingBaseDataContext;


        internal List<CEmployee> GetNotVoitEmployeesByDep(int DepId)
        {
            return null;
        }

        public int AnketaId
        {
            get
            {
                return _VoitingBaseDataContext.QuestSettings.Where(a => a.Name == "CurentAnketaId").Single().ValueInt.Value;
            }
        }

        public List<int> GetAllVoiting()
        {
            return _VoitingBaseDataContext.QuestAnswers.Where(a=>a.AnketaId.Value == AnketaId).Select(c => (c.EmpId.Value)).Distinct().ToList();
        }

        string StaffConnStr = "Driver={Progress OpenEdge 10.1B driver};HOST=web;DB=staff;UID=sysprogress;PWD=progress;PORT=2520;";
        internal List<CEmployee> GetAllEmployees()
        {
            List<CEmployee> Tmp = new List<CEmployee>();

            OdbcConnection Conn = new OdbcConnection(StaffConnStr);
            Conn.Open();
            string CommandStr = "SELECT   PUB.EMPLOYEE.EMPLOYEE_ID, PUB.EMPLOYEE.LAST_NAME, PUB.EMPLOYEE.FIRST_NAME, PUB.EMPLOYEE.MIDDLE_NAME, PUB.EMPLOYEE.PHONE_EMAIL, " +
                         "PUB.EMPLOYEE.UPDATE_DATE, PUB.EMPLOYEE.ENTRY_DATE, PUB.EMPLOYEE.DISMISSAL_DATE, PUB.EMPLOYEE.DISMISSAL_REASON, " +
                         "PUB.EMPLOYEE.PAYROLL_TYPE_ID, PUB.EMPLOYEE.CODE, PUB.EMPLOYEE.ENROL_DATE, PUB.EMPLOYEE.SALARY, PUB.EMPLOYEE.POSITION_ID, " +
                         "PUB.EMPLOYEE.SUBDIVISION_ID, PUB.EMPLOYEE.SALARY_FROM_DATE, PUB.EMPLOYEE.BIRTHDAY, PUB.EMPLOYEE.Size_id, PUB.EMPLOYEE.Sex " +

"FROM            PUB.EMPLOYEE " +
"WHERE   dismissal_date is null  and  PUB.EMPLOYEE.POSITION_ID<>17 ";


            OdbcCommand Comm = new OdbcCommand(CommandStr, Conn);

            OdbcDataReader OdR = Comm.ExecuteReader();

            

            while (OdR.Read())
            { 
            try
            {

                    CEmployee Emp = new CEmployee();
                    Emp.Id = OdR.GetInt32(0);


                Emp.DepId = OdR.GetInt32(14);

                Emp.FirstName = OdR.GetString(2);
                Emp.LastName = OdR.GetString(1);
                Emp.MiddleName = OdR.GetString(3);

                Emp.PosId = OdR.GetInt32(13);
                    Tmp.Add(Emp);
            }
            catch
            {
            }

        }

            Conn.Close();
            return Tmp;

        }

    }
}