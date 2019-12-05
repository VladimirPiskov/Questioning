using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;


namespace Questioning.Models
{
    public class SetEmployeeModel
    {
        public SetEmployeeModel()
        { }
       

       
        public int Num { set; get; }

        public string  Name { set; get; }

        public int DepId { set; get; }

        public int PosId { set; get; }

        public string ErrorMsg { set; get; }

    }


    public class CEmployee
    {
        
        public int Id { set; get; }

        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public int DepId { set; get; }

        public int PosId { set; get; }

    }

    public class CAnswer
    {
        public bool? Answ1 { set; get; }
        public bool? Answ2 { set; get; }
        public bool? Answ3 { set; get; }
        public bool? Answ4 { set; get; }
        public bool? Answ5 { set; get; }


    }

    public class CQuestion
    {
        public int NumberInAnketa { set; get; }
        public int AnketaId { set; get; }
        public int QId { set; get; }
        public string QText { set; get; }
        public CAnswer Answer { set; get; }
       
        public int Result { set; get; }
    }

    public class QuestionsModel
    {
        //public int AnketaId = 2;
        public List<CQuestion> Questions { set; get; }
        public string Greet1 { set; get; }
        public string Greet2 { set; get; }
        public string Greet3 { set; get; }
        public CEmployee Empl { set; get; }
        public bool NotRight { set; get; }
        public string ErrorMessage { set; get; }
        public string IpAddress { set; get; }
        public string CompName { set; get; }
        public string UserAgent { set; get; }
        public int AnswerTypes { set; get; }
    }



}