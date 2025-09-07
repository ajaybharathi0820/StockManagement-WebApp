using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Production.Application.DTOs;

namespace Production.Application.Commands.CreatePolisherAssignment
{
        public class CreatePolisherAssignmentCommand : IRequest<Guid>
        {
            public PolisherAssignmentDto polisherAssignment { get; set; }
            public string? CurrentUserId { get; set; }
        }
}