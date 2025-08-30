using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }                // Primary Key
        public string ProductCode { get; set; }    // Unique product code
        public string Name { get; set; }           // Product name
        public decimal Weight { get; set; }        // Product weight
        public DateTime CreatedAt { get; set; }    // Audit field
        public string CreatedBy { get; set; }      // Audit field
        public bool IsActive { get; set; } = true; // Soft delete / Active status
    }
}
