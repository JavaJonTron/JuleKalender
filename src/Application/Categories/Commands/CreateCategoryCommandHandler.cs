using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Categories.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name, request.Description);
        
        await _categoryRepository.AddAsync(category, cancellationToken);

        return new CategoryDto(
            category.Id,
            category.Name,
            category.Description,
            category.LastUsedDate,
            category.Gifts.Count,
            category.CreatedAt
        );
    }
}
