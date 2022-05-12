using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public PatientRegistration PatientRegistration { get; set; }
        public Medicines Medicines { get; set; }
        //public Bed Bed { get; set; }
        public int Services { get; set; }
        public string Charges { get; set; }
        public bool Status { get; set; }
        public int Total { get; set; }
    }
}
