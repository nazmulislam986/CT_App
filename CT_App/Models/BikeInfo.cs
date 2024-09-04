using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT_App.Models
{
    public class BikeInfo
    {
        public string B_ID { get; set; }
        public DateTime B_Chng_Date { get; set; }
        public float B_KM_ODO { get; set; }
        public float B_Mobile_Go { get; set; }
        public float B_Next_ODO { get; set; }
        public string B_Insrt_Person { get; set; }
        public string B_Updt_Person { get; set; }
    }
}
