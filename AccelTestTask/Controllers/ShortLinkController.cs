using System;
using System.Threading.Tasks;
using AccelTestTask.Exceptions;
using AccelTestTask.Models;
using AccelTestTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccelTestTask.Controllers;

[ApiController]
public class ShortLinkController : ControllerBase
{
    private readonly IShortLinkService _shortLinkService;

    public ShortLinkController(IShortLinkService shortLinkService)
    {
        _shortLinkService = shortLinkService;
    }

    [HttpPost]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GenerateToken([FromQuery] string url)
    {
        try
        {
            //url validation

            string token = await _shortLinkService.GenerateToken(url);

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("/{token}")]
    public async Task<IActionResult> GoTo([FromRoute] string token)
    {
        try
        {
            ShortLink shortLink = await _shortLinkService.GoTo(token);
            
            return Redirect(shortLink.Link);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}