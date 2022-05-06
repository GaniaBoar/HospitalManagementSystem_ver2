using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class Bed
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int NumberOfBed { get; set; }
        public string Description { get; set; }
        public double Charges { get; set; }
        public bool Status { get; set; }

    }
}
