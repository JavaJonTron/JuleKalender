using Domain.Entities;

namespace Domain.Interfaces;

public interface IGiftRepository
{
    Task<Gift?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Gift>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Gift>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task<List<Gift>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<Gift> AddAsync(Gift gift, CancellationToken cancellationToken = default);
    Task UpdateAsync(Gift gift, CancellationToken cancellationToken = default);
    Task DeleteAsync(Gift gift, CancellationToken cancellationToken = default);
}
