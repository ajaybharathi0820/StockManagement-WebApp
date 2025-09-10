using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Product.Domain.Repositories;

namespace Product.Application.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Product.ProductCode)
                .NotEmpty().MaximumLength(50)
                .MustAsync(async (code, cancellation) => !await _productRepository.IsCodeExistsAsync(code, null, cancellation))
                .WithMessage("Product code already exists. Please choose a different code.");

            RuleFor(x => x.Product.Name)
                .NotEmpty().MaximumLength(100)
                .MustAsync(async (name, cancellation) => !await _productRepository.IsNameExistsAsync(name, null, cancellation))
                .WithMessage("Product name already exists. Please choose a different name.");

            RuleFor(x => x.Product.Weight).GreaterThan(0);
        }
    }
}