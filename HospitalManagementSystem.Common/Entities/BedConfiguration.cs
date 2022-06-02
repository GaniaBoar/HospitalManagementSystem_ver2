using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class BedConfiguration
    {
  
        public int Id { get; set; }
        public int Number { get; set; }

        public BedType BedType { get; set; }
        public string Description { get; set; }
        
    }
}
