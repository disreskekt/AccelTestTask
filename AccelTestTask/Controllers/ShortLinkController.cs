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
    private readonly IValidationService _validationService;

    public ShortLinkController(IShortLinkService shortLinkService, IValidationService validationService)
    {
        _shortLinkService = shortLinkService;
        _validationService = validationService;
    }

    [HttpPost]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GenerateToken([FromQuery] string uri)
    {
        try
        {
            _validationService.ValidateUri(uri);

            string token = await _shortLinkService.GenerateToken(uri);

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
            _validationService.ValidateToken(token);
            
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