using System.Threading.Tasks;
using AccelTestTask.Models;

namespace AccelTestTask.Services.Interfaces;

public interface IShortLinkService
{
    public void ValidateUri(string uri);
    public Task<string> GenerateToken(string uri);
    public Task<ShortLink> GoTo(string token);
}