using Application.DTOs;
using MediatR;

namespace Application.Gifts.Commands;

public record CreateGiftCommand(
    string Name,
    string Description,
    DateTime DateGiven,
    string Recipient,
    Guid CategoryId,
    string? ImageUrl = null
) : IRequest<GiftDto>;
