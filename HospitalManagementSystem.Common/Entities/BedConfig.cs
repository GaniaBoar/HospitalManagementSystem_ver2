using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class BedConfig
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public BedNo BedNo { get; set; }
    }
}
