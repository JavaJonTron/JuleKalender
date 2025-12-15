using Domain.Common;

namespace Domain.Entities;

public class Category : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime? LastUsedDate { get; private set; }
    
    private readonly List<Gift> _gifts = new();
    public IReadOnlyCollection<Gift> Gifts => _gifts.AsReadOnly();

    // For EF Core
    private Category() { }

    public Category(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));

        Name = name;
        Description = description ?? string.Empty;
    }

    public void UpdateDetails(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));

        Name = name;
        Description = description ?? string.Empty;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddGift(Gift gift)
    {
        _gifts.Add(gift);
        LastUsedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveGift(Gift gift)
    {
        _gifts.Remove(gift);
        UpdatedAt = DateTime.UtcNow;
    }
}
