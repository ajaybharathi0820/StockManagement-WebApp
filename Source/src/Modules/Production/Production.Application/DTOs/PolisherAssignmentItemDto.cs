using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Application.DTOs
{
    public class PolisherAssignmentItemDto
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;

        public Guid BagTypeId { get; set; }
        public string BagTypeName { get; set; } = string.Empty;
        public decimal BagWeight { get; set; }

        public int Dozens { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal NetWeight { get; set; }
        public decimal AvgWeight { get; set; }
        public decimal ProductAvgWeight { get; set; }
        public decimal ToleranceDiff { get; set; }
    }
}