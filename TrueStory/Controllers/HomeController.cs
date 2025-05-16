using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrueStory.Handlers;
using TrueStory.Models;

namespace TrueStory.Controllers;

[ApiController]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("[controller]/objects")]
    public async Task<IActionResult> List([FromQuery] string? search, [FromQuery] int? count, [FromQuery] int? page)
    {
        var pagedResult = await _mediator.Send(new GetListRequest(search, page ?? 1, count ?? 10));
        _logger.LogDebug("Request {Request} was handled", nameof(GetListRequest));
        return Ok(pagedResult);
    }

    [HttpPost("[controller]/objects")]
    public async Task<IActionResult> Add([FromBody] ItemModel item)
    {
        var content = await _mediator.Send(new AddItemRequest(item));
        _logger.LogDebug("Request {Request} was handled", nameof(AddItemRequest));
        return Ok(content);
        //supposed to be Created();
    }

    [HttpDelete("[controller]/objects/{id}")]
    public async Task<IActionResult> Remove([FromRoute] string id)
    {
        var content = await _mediator.Send(new RemoveRequest(id));
        _logger.LogDebug("Request {Request} was handled", nameof(RemoveRequest));
        return Ok(content);
        //supposed to be NoContent();
    }
}