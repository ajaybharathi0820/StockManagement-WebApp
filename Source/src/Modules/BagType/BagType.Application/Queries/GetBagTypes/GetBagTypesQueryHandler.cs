using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using BagType.Application.DTOs;
using BagType.Domain.Repositories;

namespace BagType.Application.Queries.GetBagTypes
{
    public class GetBagTypesQueryHandler : IRequestHandler<GetBagTypesQuery, List<BagTypeDto>>
    {
        private readonly IBagTypeRepository _repository;

        public GetBagTypesQueryHandler(IBagTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BagTypeDto>> Handle(GetBagTypesQuery request, CancellationToken cancellationToken)
        {
            var bagTypes = await _repository.GetAllAsync(cancellationToken);

            return bagTypes.Select(bt => new BagTypeDto
            {
                Id = bt.Id,
                Name = bt.Name,
                Weight = bt.Weight,
                CreatedAt = bt.CreatedAt,
                UpdatedAt = bt.UpdatedAt
            }).ToList();
        }
    }
}