namespace Application.DTOs;

public record GiftDto(
    Guid Id,
    string Name,
    string Description,
    DateTime DateGiven,
    string Recipient,
    Guid CategoryId,
    string CategoryName,
    string? ImageUrl,
    DateTime CreatedAt
);
