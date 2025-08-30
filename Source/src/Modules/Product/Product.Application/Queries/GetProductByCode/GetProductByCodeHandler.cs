using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Domain.Repositories;
using Product.Application.DTOs;

namespace Product.Application.Queries.GetProductByCode
{
    public class GetProductByCodeHandler : IRequestHandler<GetProductByCodeQuery, ProductDto>
    {
        private readonly IProductRepository _repository;

        public GetProductByCodeHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByCodeAsync(request.ProductCode, cancellationToken);

            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                ProductCode = product.ProductCode,
                Name = product.Name,
                Weight = product.Weight,
                IsActive = product.IsActive
            };
        }
    }
}