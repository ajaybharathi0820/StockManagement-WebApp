using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Application.DTOs
{
    public class PolisherAssignmentDto
    {
        public Guid Id { get; set; }
        public Guid PolisherId { get; set; }
        public string PolisherName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        public List<PolisherAssignmentItemDto> Items { get; set; } = new();
    }
}