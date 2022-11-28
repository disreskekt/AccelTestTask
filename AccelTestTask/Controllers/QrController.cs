using System;
using System.Threading.Tasks;
using AccelTestTask.Helpers;
using AccelTestTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccelTestTask.Controllers;

public class QrController : ControllerBase
{
    private readonly IQrService _qrService;
    private readonly IShortLinkService _shortLinkService;
    private readonly IValidationService _validationService;

    public QrController(IQrService qrService, IShortLinkService shortLinkService, IValidationService validationService)
    {
        _qrService = qrService;
        _shortLinkService = shortLinkService;
        _validationService = validationService;
    }
    
    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GenerateQr([FromQuery] string link)
    {
        Guid guid = Guid.Empty;
        try
        {
            _validationService.ValidateUri(link);
            
            if (!await _shortLinkService.Exists(link.RetrieveToken()))
            {
                return NoContent();
            }
            
            guid = _qrService.GenerateQr(link);
            
            byte[] qrBytes = await System.IO.File.ReadAllBytesAsync(ConstantHelper.QrPath + guid);
            
            return File(qrBytes, "image/jpeg");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        finally
        {
            if (System.IO.File.Exists(ConstantHelper.QrPath + guid))
            {
                System.IO.File.Delete(ConstantHelper.QrPath + guid);
            }
        }
    }
}