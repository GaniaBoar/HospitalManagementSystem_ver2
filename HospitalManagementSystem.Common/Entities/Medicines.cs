using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
   public class Medicines
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Composition { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string BatchId { get; set; }
        public DateTime Mfddate { get; set; }
        public DateTime Expdate { get; set; }
        public bool Availability { get; set; }
        public string CompanyName { get; set; }
    }
}
