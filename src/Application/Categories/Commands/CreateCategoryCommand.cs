using Application.DTOs;
using MediatR;

namespace Application.Categories.Commands;

public record CreateCategoryCommand(string Name, string Description) : IRequest<CategoryDto>;
