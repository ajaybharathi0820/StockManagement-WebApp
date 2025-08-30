using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using BagType.Application.DTOs;
using BagType.Domain.Repositories;

namespace BagType.Application.Queries.GetBagTypeById
{
    public class GetBagTypeByIdQueryHandler : IRequestHandler<GetBagTypeByIdQuery, BagTypeDto>
    {
        private readonly IBagTypeRepository _repository;

        public GetBagTypeByIdQueryHandler(IBagTypeRepository repository)
        {
            _repository = repository;
        }
        public async Task<BagTypeDto> Handle(GetBagTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var bagType = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (bagType == null)
                throw new KeyNotFoundException($"BagType with Id {request.Id} not found.");

            return new BagTypeDto
            {
                Id = bagType.Id,
                Name = bagType.Name,
                Weight = bagType.Weight,
                CreatedAt = bagType.CreatedAt,
                UpdatedAt = bagType.UpdatedAt
            };
        }

    }
}