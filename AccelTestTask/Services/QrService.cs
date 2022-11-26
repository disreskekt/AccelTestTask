using System;
using AccelTestTask.Helpers;
using AccelTestTask.Services.Interfaces;
using IronBarCode;

namespace AccelTestTask.Services;

public class QrService : IQrService
{
    public Guid GenerateQr(string shortLink)
    {
        GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(shortLink, BarcodeEncoding.QRCode);

        Guid guid = Guid.NewGuid();

        barcode.SaveAsJpeg(ConstantHelper.QrPath + guid);

        return guid;
    }
}