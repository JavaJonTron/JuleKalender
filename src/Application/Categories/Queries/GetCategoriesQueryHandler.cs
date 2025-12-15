using Application.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Categories.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? await _categoryRepository.GetAllAsync(cancellationToken)
            : await _categoryRepository.SearchAsync(request.SearchTerm, cancellationToken);

        return categories.Select(c => new CategoryDto(
            c.Id,
            c.Name,
            c.Description,
            c.LastUsedDate,
            c.Gifts.Count,
            c.CreatedAt
        )).ToList();
    }
}
