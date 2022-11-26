using System;

namespace AccelTestTask.Services.Interfaces;

public interface IQrService
{
    public Guid GenerateQr(string shortLink);
}