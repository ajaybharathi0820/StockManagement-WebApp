using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Production.Application.DTOs;

namespace Production.Application.Queries.SearchPolisherAssignments
{
    public class SearchPolisherAssignmentsQuery : IRequest<List<PolisherAssignmentDto>>
    {
        public PolisherAssignmentSearchDto Criteria { get; set; }
    }
}