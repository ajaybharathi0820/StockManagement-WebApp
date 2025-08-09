namespace Polisher.Domain.Repositories;

public interface IPolisherRepository
{
    //Task<List<Entities.Polisher>> GetAllAsync();
    // Task<Entities.Polisher> GetByIdAsync(Guid id);
    Task AddAsync(Entities.Polisher polisher, CancellationToken cancellationToken);
    // Task UpdateAsync(Entities.Polisher polisher);
    // Task DeleteAsync(Guid id);
}
