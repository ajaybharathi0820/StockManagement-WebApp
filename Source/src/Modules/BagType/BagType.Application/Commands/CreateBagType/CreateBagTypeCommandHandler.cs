using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagType.Domain.Repositories;
using MediatR;

namespace BagType.Application.Commands.CreateBagType
{
    public class CreateBagTypeCommandHandler : IRequestHandler<CreateBagTypeCommand, int>
    {
        private readonly IBagTypeRepository _repository;

        public CreateBagTypeCommandHandler(IBagTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateBagTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new BagType.Domain.Entities.BagType
            {
                Id = Guid.NewGuid(),
                Name = request.BagType.Name,
                Weight = request.BagType.Weight
            };

            await _repository.AddAsync(entity,cancellationToken);

            return 1; // Success indicator since BagType uses Guid ID
        }
        
    }
}