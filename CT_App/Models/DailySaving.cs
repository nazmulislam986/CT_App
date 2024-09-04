using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class DailySaving
    {
        public string DS_ID { get; set; }
        public DateTime DS_Date { get; set; }
        public float DS_FPAmount { get; set; }
        public float DS_SPAmount { get; set; }
        public float DS_TPAmount { get; set; }
        public float NotTaken { get; set; }
        public string DS_Data { get; set; }
        public DateTime DS_InBankDate { get; set; }
        public string DS_Insrt_Person { get; set; }
        public string DS_Updt_Person { get; set; }
        public string DS_Del_Person { get; set; }
    }
}
