using System;
using AccelTestTask.Exceptions;
using AccelTestTask.Services.Interfaces;

namespace AccelTestTask.Services;

public class ValidationService : IValidationService
{
    public void ValidateUri(string uri)
    {
        if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
        {
            throw new UriFormatException("Wrong uri format");
        }
    }

    public void ValidateToken(string token)
    {
        if(!Int32.TryParse(token,
               System.Globalization.NumberStyles.HexNumber,
               System.Globalization.CultureInfo.InvariantCulture,
               out int _))
        {

            throw new ValidationException("Wrong token format");
        }
    }
}