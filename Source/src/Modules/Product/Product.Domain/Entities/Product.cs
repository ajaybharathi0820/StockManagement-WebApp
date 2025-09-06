using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    using Common.Models;

    public class Product : AuditableEntity
    {
    public Guid Id { get; set; }                // Primary Key
        public string ProductCode { get; set; }    // Unique product code
        public string Name { get; set; }           // Product name
        public decimal Weight { get; set; }        // Product weight
        public DateTime CreatedAt { get; set; }    // Audit field
    // ...existing code...
    }
}
