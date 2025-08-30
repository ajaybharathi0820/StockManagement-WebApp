using MediatR;
using BagType.Domain.Repositories;

namespace BagType.Application.Commands.DeleteBagType
{
    public class DeleteBagTypeCommandHandler : IRequestHandler<DeleteBagTypeCommand, bool>
    {
        private readonly IBagTypeRepository _repository;

        public DeleteBagTypeCommandHandler(IBagTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBagTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id,cancellationToken);
            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity,cancellationToken);
            return true;
        }
    }
}
