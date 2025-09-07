using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Production.Application.DTOs;
using Production.Domain.Repositories;
using Production.Domain.Entities;

namespace Production.Application.Commands.CreatePolisherAssignment
{
    public class CreatePolisherAssignmentCommandHandler : IRequestHandler<CreatePolisherAssignmentCommand, Guid>
    {
        private readonly IPolisherAssignmentRepository _repository;

        public CreatePolisherAssignmentCommandHandler(IPolisherAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreatePolisherAssignmentCommand request, CancellationToken cancellationToken)
        {
            // Create assignment root
            var createdBy = !string.IsNullOrWhiteSpace(request.CurrentUserId)
                ? request.CurrentUserId!
                : request.polisherAssignment.CreatedBy;
            var assignment = new PolisherAssignment(
                request.polisherAssignment.PolisherId,
                request.polisherAssignment.PolisherName,
                createdBy
            );
            foreach (var item in request.polisherAssignment.Items)
            {
                assignment.AddItem(
                    item.ProductId,
                    assignment.Id,
                    item.ProductCode,
                    item.ProductName,
                    item.BagTypeId,
                    item.BagTypeName,
                    item.BagWeight,
                    item.Dozens,
                    item.TotalWeight,
                    item.ProductAvgWeight,
                    item.ToleranceDiff   
                );
            }

            await _repository.AddAsync(assignment, cancellationToken);

            return assignment.Id;
        }

    }
}