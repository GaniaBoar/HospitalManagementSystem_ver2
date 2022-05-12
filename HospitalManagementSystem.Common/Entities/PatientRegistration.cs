using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class PatientRegistration
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Bloodgroup { get; set; }
        public string Maritalstatus { get; set; }
        public Int64 Phoneno { get; set; }
        public string Address { get; set; }
        public string Diagnosis { get; set; }
        public string Complaints { get; set; }
    }
}
