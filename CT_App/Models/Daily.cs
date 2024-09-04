using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class Daily
    {
        public string D_ID { get; set; }
        public DateTime D_Date { get; set; }
        public float D_FPAmount { get; set; }
        public float D_SPAmount { get; set; }
        public float NotTaken { get; set; }
        public string D_Data { get; set; }
        public DateTime TakenDate { get; set; }
        public string D_Insrt_Person { get; set; }
        public string D_Updt_Person { get; set; }
        public string D_Del_Person { get; set; }
    }
}
