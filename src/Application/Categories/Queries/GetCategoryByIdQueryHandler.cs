using Application.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Categories.Queries;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDetailDto?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDetailDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (category == null)
            return null;

        var gifts = category.Gifts.Select(g => new GiftDto(
            g.Id,
            g.Name,
            g.Description,
            g.DateGiven,
            g.Recipient,
            g.CategoryId,
            category.Name,
            g.ImageUrl,
            g.CreatedAt
        )).ToList();

        return new CategoryDetailDto(
            category.Id,
            category.Name,
            category.Description,
            category.LastUsedDate,
            gifts,
            category.CreatedAt
        );
    }
}
