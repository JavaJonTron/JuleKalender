using Application.DTOs;
using MediatR;

namespace Application.Gifts.Queries;

public record GetGiftsQuery(string? SearchTerm = null, Guid? CategoryId = null) : IRequest<List<GiftDto>>;
