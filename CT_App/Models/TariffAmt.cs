using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class TariffAmt
    {
        public string InExpense { get; set; }
        public float Expense_Amount { get; set; }
        public string Expense_To { get; set; }
        public string ThroughBy_Expense { get; set; }
        public DateTime Expense_Date { get; set; }
        public string Remarks_Expense { get; set; }
        public string EDT_V { get; set; }
        public DateTime EDT_V_Date { get; set; }
        public DateTime DDT_V_Date { get; set; }
        public string E_Insrt_Person { get; set; }
        public string E_Updt_Person { get; set; }
        public string E_Del_Person { get; set; }

        public float Was_Expense_UD { get; set; }
        public float Now_Expense_UD { get; set; }
        public float Expense_Amount_UD { get; set; }
        public string Expense_To_UD { get; set; }
        public DateTime EDT_V_Date_UD { get; set; }
    }
}
