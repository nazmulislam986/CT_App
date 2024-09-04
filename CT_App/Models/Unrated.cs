using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class Unrated
    {
        public string InUnrated { get; set; }
        public float Unrated_Amount { get; set; }
        public string Unrated_To { get; set; }
        public string ThroughBy_Unrated { get; set; }
        public DateTime Unrated_Date { get; set; }
        public string Remarks_Unrated { get; set; }
        public string UDT_V { get; set; }
        public DateTime UDT_V_Date { get; set; }
        public DateTime DDT_V_Date { get; set; }
        public string U_Insrt_Person { get; set; }
        public string U_Updt_Person { get; set; }
        public string U_Del_Person { get; set; }

        public float Was_Unrated_UD { get; set; }
        public float Now_Unrated_UD { get; set; }
        public float Unrated_Amount_UD { get; set; }
        public string Unrated_To_UD { get; set; }
        public DateTime UDT_V_Date_UD { get; set; }
    }
}
