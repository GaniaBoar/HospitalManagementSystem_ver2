using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class BedType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
