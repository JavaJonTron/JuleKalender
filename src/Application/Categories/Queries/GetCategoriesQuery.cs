using Application.DTOs;
using MediatR;

namespace Application.Categories.Queries;

public record GetCategoriesQuery(string? SearchTerm = null) : IRequest<List<CategoryDto>>;
