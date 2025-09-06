using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Production.Application.DTOs;
using Production.Domain.Repositories;

namespace Production.Application.Queries.SearchPolisherAssignments
{
    public class SearchPolisherAssignmentsQueryHandler : IRequestHandler<SearchPolisherAssignmentsQuery, List<PolisherAssignmentDto>>
    {
        private readonly IPolisherAssignmentRepository _repository;

        public SearchPolisherAssignmentsQueryHandler(IPolisherAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PolisherAssignmentDto>> Handle(
            SearchPolisherAssignmentsQuery request,
            CancellationToken cancellationToken)
        {
            var polisherAssignmentSearch = new Domain.ValueObjects.PolisherAssignmentSearch
            {
                PolisherId = request.Criteria.PolisherId,
                ProductId = request.Criteria.ProductId,
                FromDate = request.Criteria.FromDate,
                ToDate = request.Criteria.ToDate
            };
            var searchResults = await _repository.SearchAsync(polisherAssignmentSearch, cancellationToken);

            return searchResults.Select(assignment => new PolisherAssignmentDto
            {
                Id = assignment.Id,
                PolisherId = assignment.PolisherId,
                PolisherName = assignment.PolisherName,
                CreatedDate = assignment.CreatedDate,
                CreatedBy = assignment.CreatedBy,

                Items = assignment.Items.Select(item => new PolisherAssignmentItemDto
                {
                    Id = item.Id,
                    AssignmentId = item.AssignmentId,
                    ProductId = item.ProductId,
                    ProductCode = item.ProductCode ?? string.Empty,
                    ProductName = item.ProductName ?? string.Empty,
                    BagTypeId = item.BagTypeId,
                    BagTypeName = item.BagTypeName ?? string.Empty,
                    BagWeight = item.BagWeight,
                    Dozens = item.Dozens,
                    TotalWeight = item.TotalWeight,
                    NetWeight = item.NetWeight,
                    AvgWeight = item.AvgWeight,
                    ProductAvgWeight = item.ProductAvgWeight,
                    ToleranceDiff = item.ToleranceDiff
                }).ToList()
            }).ToList();
        }

    }
}