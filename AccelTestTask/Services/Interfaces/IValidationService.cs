namespace AccelTestTask.Services.Interfaces;

public interface IValidationService
{
    public void ValidateUri(string uri);
    public void ValidateToken(string token);
}