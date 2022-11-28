using System.Threading.Tasks;
using AccelTestTask.Models;

namespace AccelTestTask.Services.Interfaces;

public interface IShortLinkService
{
    public Task<string> GenerateToken(string uri);
    public Task<ShortLink> GoTo(string token);
    public Task<bool> Exists(string token);
}