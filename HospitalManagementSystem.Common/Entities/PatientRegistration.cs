using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class PatientRegistration
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public DateTime dob { get; set; }
        public string bloodgroup { get; set; }
        public string maritalstatus { get; set; }
        public Int64 phoneno { get; set; }
        public string address { get; set; }
        public string diagnosis { get; set; }
        public string complaints { get; set; }
    }
}
