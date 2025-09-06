using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Production.Application.DTOs;

namespace Production.Application.Queries.GetPolisherAssignmentById
{
    public record GetPolisherAssignmentByIdQuery(Guid Id) : IRequest<PolisherAssignmentDto>;
}