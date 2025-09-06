using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Production.Application.DTOs;
using Production.Domain.Repositories;

namespace Production.Application.Queries.GetPolisherAssignmentById
{
    public class GetPolisherAssignmentByIdQueryHandler
        : IRequestHandler<GetPolisherAssignmentByIdQuery, PolisherAssignmentDto?>
    {
        private readonly IPolisherAssignmentRepository _repository;

        public GetPolisherAssignmentByIdQueryHandler(IPolisherAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PolisherAssignmentDto?> Handle(
            GetPolisherAssignmentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var assignment = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (assignment == null)
                return null;

            return new PolisherAssignmentDto
            {
                Id = assignment.Id,
                PolisherId = assignment.PolisherId,
                PolisherName = assignment.PolisherName,
                CreatedDate = assignment.CreatedDate,
                CreatedBy = assignment.CreatedBy,
                Items = assignment.Items.Select(i => new PolisherAssignmentItemDto
                {
                    Id = i.Id,
                    AssignmentId = i.AssignmentId,
                    ProductCode = i.ProductCode,
                    ProductName = i.ProductName,
                    BagTypeName = i.BagTypeName,
                    BagWeight = i.BagWeight,
                    Dozens = i.Dozens,
                    TotalWeight = i.TotalWeight,
                    NetWeight = i.NetWeight,
                    AvgWeight = i.AvgWeight,
                    ProductAvgWeight = i.ProductAvgWeight,
                    ToleranceDiff=i.ToleranceDiff
                }).ToList()
            };
        }
    }
}