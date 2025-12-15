using Application.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Gifts.Queries;

public class GetGiftsQueryHandler : IRequestHandler<GetGiftsQuery, List<GiftDto>>
{
    private readonly IGiftRepository _giftRepository;

    public GetGiftsQueryHandler(IGiftRepository giftRepository)
    {
        _giftRepository = giftRepository;
    }

    public async Task<List<GiftDto>> Handle(GetGiftsQuery request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.Gift> gifts;

        if (request.CategoryId.HasValue)
        {
            gifts = await _giftRepository.GetByCategoryIdAsync(request.CategoryId.Value, cancellationToken);
        }
        else if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            gifts = await _giftRepository.SearchAsync(request.SearchTerm, cancellationToken);
        }
        else
        {
            gifts = await _giftRepository.GetAllAsync(cancellationToken);
        }

        return gifts.Select(g => new GiftDto(
            g.Id,
            g.Name,
            g.Description,
            g.DateGiven,
            g.Recipient,
            g.CategoryId,
            g.Category.Name,
            g.ImageUrl,
            g.CreatedAt
        )).ToList();
    }
}
