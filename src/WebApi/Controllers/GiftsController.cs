using Application.DTOs;
using Application.Gifts.Commands;
using Application.Gifts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GiftsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GiftsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GiftDto>>> GetGifts(
        [FromQuery] string? search = null,
        [FromQuery] Guid? categoryId = null)
    {
        var query = new GetGiftsQuery(search, categoryId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GiftDto>> CreateGift([FromBody] CreateGiftCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetGifts), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
