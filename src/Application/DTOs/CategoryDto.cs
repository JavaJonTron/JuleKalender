namespace Application.DTOs;

public record CategoryDto(
    Guid Id,
    string Name,
    string Description,
    DateTime? LastUsedDate,
    int GiftCount,
    DateTime CreatedAt
);

public record CategoryDetailDto(
    Guid Id,
    string Name,
    string Description,
    DateTime? LastUsedDate,
    List<GiftDto> Gifts,
    DateTime CreatedAt
);
