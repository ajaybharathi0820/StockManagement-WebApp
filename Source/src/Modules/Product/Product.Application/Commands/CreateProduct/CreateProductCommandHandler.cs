using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Domain.Repositories;

namespace Product.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product.Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                ProductCode = request.Product.ProductCode,
                Name = request.Product.Name,
                Weight = request.Product.Weight,
                // set audit via helper below
            };
            product.MarkCreated(!string.IsNullOrWhiteSpace(request.CurrentUserId) ? request.CurrentUserId! : "System");

            await _productRepository.AddAsync(product, cancellationToken);

            return product.Id;

        }
    }
}