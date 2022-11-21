using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccelTestTask.Controllers;

[ApiController]
public class ShortLinkController : ControllerBase
{
    private readonly DataContext _db;

    public ShortLinkController(DataContext dataContext)
    {
        _db = dataContext;
    }

    [HttpPost]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GenerateToken([FromQuery] string url)
    {
        try
        {
            //url validation
            
            ShortLink? shortLink = await _db.Tokens.FirstOrDefaultAsync(tkn => tkn.Link == url);
            
            if (shortLink is not null)
            {
                return Ok(shortLink.Token);
            }
            
            string newToken = $"{url.GetHashCode():X}";
            
            while (true)
            {
                shortLink = await _db.Tokens.FirstOrDefaultAsync(tkn => tkn.Token == newToken);

                if (shortLink is null)
                {
                    break;
                }
                
                newToken = $"{(url + DateTime.Now.ToString(CultureInfo.InvariantCulture)).GetHashCode():X}";
            }
            
            _db.Tokens.Add(new ShortLink()
            {
                Link = url,
                Token = newToken
            });
            await _db.SaveChangesAsync();
            
            return Ok(newToken);
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
            ShortLink? shortLink = await _db.Tokens.FirstOrDefaultAsync(tkn => tkn.Token == token);

            if (shortLink is null)
            {
                return NotFound();
            }

            return Redirect(shortLink.Link);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}