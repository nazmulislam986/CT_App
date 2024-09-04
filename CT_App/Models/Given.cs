using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class Given
    {
        public string InGiven { get; set; }
        public float Total_Given { get; set; }
        public string Given_To { get; set; }
        public string ThroughBy_Given { get; set; }
        public DateTime Given_Date { get; set; }
        public string Remarks_Given { get; set; }
        public string GDT_V { get; set; }
        public DateTime GDT_V_Date { get; set; }
        public DateTime DDT_V_Date { get; set; }
        public string G_Insrt_Person { get; set; }
        public string G_Updt_Person { get; set; }
        public string G_Del_Person { get; set; }

        public float Was_Given_UD { get; set; }
        public float Now_Given_UD { get; set; }
        public float Total_Given_UD { get; set; }
        public string Given_To_UD { get; set; }
        public DateTime GDT_V_Date_UD { get; set; }
    }
}
