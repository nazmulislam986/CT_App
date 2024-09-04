using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class Teken
    {
        public string InTake { get; set; }
        public float Total_Take { get; set; }
        public string Take_To { get; set; }
        public string ThroughBy_Take { get; set; }
        public DateTime Take_Date { get; set; }
        public string Remarks_Take { get; set; }
        public string TDT_V { get; set; }
        public DateTime TDT_V_Date { get; set; }
        public DateTime DDT_V_Date { get; set; }
        public string T_Insrt_Person { get; set; }
        public string T_Updt_Person { get; set; }
        public string T_Del_Person { get; set; }

        public float Was_Take_UD { get; set; }
        public float Now_Take_UD { get; set; }
        public float Total_Take_UD { get; set; }
        public string Take_To_UD { get; set; }
        public DateTime TDT_V_Date_UD { get; set; }
    }
}
