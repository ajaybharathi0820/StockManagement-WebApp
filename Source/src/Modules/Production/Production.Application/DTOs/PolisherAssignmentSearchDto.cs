using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Application.DTOs
{
    public class PolisherAssignmentSearchDto
    {
        public Guid? PolisherId { get; set; }
        public Guid? ProductId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}