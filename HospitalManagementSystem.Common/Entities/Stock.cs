using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Common.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Status { get; set; }
        
        
    }
}
