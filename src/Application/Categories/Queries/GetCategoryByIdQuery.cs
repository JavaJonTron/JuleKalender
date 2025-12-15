using Application.DTOs;
using MediatR;

namespace Application.Categories.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDetailDto?>;
