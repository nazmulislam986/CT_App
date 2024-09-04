using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class Saving
    {
        public string InSaving { get; set; }
        public float Saving_Amount { get; set; }
        public string Saving_To { get; set; }
        public string ThroughBy_Saving { get; set; }
        public DateTime Saving_Date { get; set; }
        public string Remarks_Saving { get; set; }
        public string SDT_V { get; set; }
        public DateTime SDT_V_Date { get; set; }
        public DateTime DDT_V_Date { get; set; }
        public string Saving_Bank { get; set; }
        public string S_Insrt_Person { get; set; }
        public string S_Updt_Person { get; set; }
        public string S_Del_Person { get; set; }

        public float Was_Saving_UD { get; set; }
        public float Now_Saving_UD { get; set; }
        public float Saving_Amount_UD { get; set; }
        public string Saving_To_UD { get; set; }
        public DateTime SDT_V_Date_UD { get; set; }
    }
}
