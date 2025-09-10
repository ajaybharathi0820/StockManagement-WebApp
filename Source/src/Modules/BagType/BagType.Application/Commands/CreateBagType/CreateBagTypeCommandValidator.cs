using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BagType.Domain.Repositories;

namespace BagType.Application.Commands.CreateBagType
{
    public class CreateBagTypeCommandValidator : AbstractValidator<CreateBagTypeCommand>
    {
        private readonly IBagTypeRepository _bagTypeRepository;

        public CreateBagTypeCommandValidator(IBagTypeRepository bagTypeRepository)
        {
            _bagTypeRepository = bagTypeRepository;

            RuleFor(x => x.BagType.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .MustAsync(async (name, cancellation) => !await _bagTypeRepository.IsNameExistsAsync(name, null, cancellation))
                .WithMessage("Bag type name already exists. Please choose a different name.");

            RuleFor(x => x.BagType.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0");
        }
    }
}