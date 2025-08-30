using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagType.Domain.Repositories;
using MediatR;

namespace BagType.Application.Commands.UpdateBagType
{
    public class UpdateBagTypeCommandHandler : IRequestHandler<UpdateBagTypeCommand, bool>
    {
        private readonly IBagTypeRepository _repository;

        public UpdateBagTypeCommandHandler(IBagTypeRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(UpdateBagTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.BagType.Id,cancellationToken);
            if (entity == null)
                return false;

            entity.Name = request.BagType.Name;
            entity.Weight = request.BagType.Weight;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(entity,cancellationToken);
            return true;
        }
    }
}