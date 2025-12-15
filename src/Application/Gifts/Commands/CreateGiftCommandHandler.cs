using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Gifts.Commands;

public class CreateGiftCommandHandler : IRequestHandler<CreateGiftCommand, GiftDto>
{
    private readonly IGiftRepository _giftRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateGiftCommandHandler(IGiftRepository giftRepository, ICategoryRepository categoryRepository)
    {
        _giftRepository = giftRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<GiftDto> Handle(CreateGiftCommand request, CancellationToken cancellationToken)
    {
        // Verify category exists
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
            throw new ArgumentException($"Category with ID {request.CategoryId} not found");

        var gift = new Gift(
            request.Name,
            request.Description,
            request.DateGiven,
            request.Recipient,
            request.CategoryId,
            request.ImageUrl
        );

        await _giftRepository.AddAsync(gift, cancellationToken);
        
        // Update category's last used date
        category.AddGift(gift);
        await _categoryRepository.UpdateAsync(category, cancellationToken);

        return new GiftDto(
            gift.Id,
            gift.Name,
            gift.Description,
            gift.DateGiven,
            gift.Recipient,
            gift.CategoryId,
            category.Name,
            gift.ImageUrl,
            gift.CreatedAt
        );
    }
}
