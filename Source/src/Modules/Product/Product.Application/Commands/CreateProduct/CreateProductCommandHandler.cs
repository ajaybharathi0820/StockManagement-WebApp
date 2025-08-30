using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Domain.Repositories;

namespace Product.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product.Domain.Entities.Product
            {
                ProductCode = request.Product.ProductCode,
                Name = request.Product.Name,
                Weight = request.Product.Weight,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _productRepository.AddAsync(product, cancellationToken);

            return product.Id;

        }
    }
}