namespace Polisher.Domain.Repositories;

public interface IPolisherRepository
{
    Task<List<Entities.Polisher>> GetAllAsync(CancellationToken cancellationToken);
    Task<Entities.Polisher> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsContactNumberExistsAsync(string contactNumber, Guid? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> IsNameCombinationExistsAsync(string firstName, string lastName, Guid? excludeId = null, CancellationToken cancellationToken = default);
    Task AddAsync(Entities.Polisher polisher, CancellationToken cancellationToken);
    Task UpdateAsync(Entities.Polisher polisher, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
