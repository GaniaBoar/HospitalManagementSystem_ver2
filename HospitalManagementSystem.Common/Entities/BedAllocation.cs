using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class BedAllocation
    {
        public int Id { get; set; }

        public BedConfiguration BedConfiguration { get; set; }
        public PatientRegistration PatientRegistration { get; set; }
        public DateTime AllocatedOn { get; set; }
        public DateTime AllocatedTill { get; set; }
        public bool Status { get; set; }
        
    }
}
