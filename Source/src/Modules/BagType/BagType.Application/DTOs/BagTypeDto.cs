using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagType.Application.DTOs
{
    public class BagTypeDto
    {
    public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}