using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GiftRepository : IGiftRepository
{
    private readonly ApplicationDbContext _context;

    public GiftRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Gift?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Gifts
            .Include(g => g.Category)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<List<Gift>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Gifts
            .Include(g => g.Category)
            .OrderByDescending(g => g.DateGiven)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Gift>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Gifts
            .Include(g => g.Category)
            .Where(g => g.CategoryId == categoryId)
            .OrderByDescending(g => g.DateGiven)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Gift>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _context.Gifts
            .Include(g => g.Category)
            .Where(g => g.Name.Contains(searchTerm) || 
                       g.Description.Contains(searchTerm) || 
                       g.Recipient.Contains(searchTerm))
            .OrderByDescending(g => g.DateGiven)
            .ToListAsync(cancellationToken);
    }

    public async Task<Gift> AddAsync(Gift gift, CancellationToken cancellationToken = default)
    {
        await _context.Gifts.AddAsync(gift, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return gift;
    }

    public async Task UpdateAsync(Gift gift, CancellationToken cancellationToken = default)
    {
        _context.Gifts.Update(gift);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Gift gift, CancellationToken cancellationToken = default)
    {
        _context.Gifts.Remove(gift);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
