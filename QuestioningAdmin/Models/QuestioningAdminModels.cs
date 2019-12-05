using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestioningAdmin.Models
{

    public class CEmployee
    {

        public int Id { set; get; }

        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public int DepId { set; get; }

        public int PosId { set; get; }

    }

    public class EmplsNotVoidModel
    {
        public List<CEmployee> Empls = new List<CEmployee>();
        public  string DepName { set; get; }

    }


    public class QuestioningAdminModel
    {
        public List<QuestioningResultModel> Res = new List<QuestioningResultModel>();

    }
    public class QuestioningResultModel
    {
        public int DepId { set; get; }
        public string DepName { set; get; }
        public int PeoplesCount { set; get; }
        public int Voided { set; get; }
        public double PecentVoided {
            get
            {
                if (PeoplesCount == 0) return 0;
                return Voided * 100 / PeoplesCount;
            }
                
                 }



    }
}