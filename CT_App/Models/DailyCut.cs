using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class DailyCut
    {
        public string C_ID { get; set; }
        public DateTime C_Date { get; set; }
        public float C_Amount { get; set; }
        public string C_Insrt_Person { get; set; }
        public string C_Updt_Person { get; set; }
        public string C_Del_Person { get; set; }
    }
}
