using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class DailyAnt
    {
        public string DA_ID { get; set; }
        public DateTime DA_Date { get; set; }
        public float DA_FPAmount { get; set; }
        public float DA_SPAmount { get; set; }
        public float NotTaken { get; set; }
        public string DA_Data { get; set; }
        public DateTime TakenDate { get; set; }
        public string DA_Insrt_Person { get; set; }
        public string DA_Updt_Person { get; set; }
        public string DA_Del_Person { get; set; }
    }
}
