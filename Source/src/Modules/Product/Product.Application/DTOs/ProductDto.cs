using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.DTOs
{
    public class ProductDto
    {
    public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public bool IsActive { get; set; }
    }
}