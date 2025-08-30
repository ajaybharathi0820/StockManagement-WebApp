using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagType.Domain.Entities
{
    public class BagType
    {
        public int Id { get; set; }          // Primary Key
        public string Name { get; set; } = string.Empty;
        public decimal Weight { get; set; }  // Weight per bag type
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}