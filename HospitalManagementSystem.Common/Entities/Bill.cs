using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class Bill
    {
       
        public int Id { get; set; }
        public PatientRegistration PatientRegistration { get; set; }
        public Medicines Medicines { get; set; }
        public BedAllocation BedAllocation { get; set; }
        public DateTime BillDate { get; set; }
        
        public string TestName { get; set; }
        public Double TestCharge { get; set; }
        public bool Status { get; set; }

        public Double Total { get; set; }
    }
}
