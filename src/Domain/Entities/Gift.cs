using Domain.Common;

namespace Domain.Entities;

public class Gift : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime DateGiven { get; private set; }
    public string Recipient { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    // For EF Core
    private Gift() { }

    public Gift(string name, string description, DateTime dateGiven, string recipient, Guid categoryId, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Gift name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(recipient))
            throw new ArgumentException("Recipient cannot be empty", nameof(recipient));

        Name = name;
        Description = description ?? string.Empty;
        DateGiven = dateGiven;
        Recipient = recipient;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
    }

    public void UpdateDetails(string name, string description, DateTime dateGiven, string recipient, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Gift name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(recipient))
            throw new ArgumentException("Recipient cannot be empty", nameof(recipient));

        Name = name;
        Description = description ?? string.Empty;
        DateGiven = dateGiven;
        Recipient = recipient;
        ImageUrl = imageUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeCategory(Guid newCategoryId)
    {
        CategoryId = newCategoryId;
        UpdatedAt = DateTime.UtcNow;
    }
}
