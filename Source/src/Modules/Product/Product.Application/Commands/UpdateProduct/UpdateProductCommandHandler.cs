using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Domain.Repositories;

namespace Product.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand,bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Product.Id, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Product with Id {request.Product.Id} not found");

            product.ProductCode = request.Product.ProductCode;
            product.Name = request.Product.Name;
            product.Weight = request.Product.Weight;
            product.IsActive = request.Product.IsActive;

            await _productRepository.UpdateAsync(product, cancellationToken);

            return true;
        }
    }
}