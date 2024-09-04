using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class Installment
    {
        public string I_ID { get; set; }
        public DateTime I_Date { get; set; }
        public float Take_Total { get; set; }
        public float Take_Anot { get; set; }
        public float Take_Mine { get; set; }
        public string Take_Data { get; set; }
        public float InsPerMonth { get; set; }
        public float PerMonthPay { get; set; }
        public float InsPay { get; set; }
        public DateTime InsPay_Date { get; set; }
        public string I_Insrt_Person { get; set; }
        public string I_Updt_Person { get; set; }
        public string I_Del_Person { get; set; }
    }
}
