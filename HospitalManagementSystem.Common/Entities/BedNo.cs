using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class BedNo
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public BedType BedType { get; set; }
        public float Price { get; set; }
    }
}
