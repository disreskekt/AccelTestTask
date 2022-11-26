using System.Threading.Tasks;
using AccelTestTask.Models;

namespace AccelTestTask.Services.Interfaces;

public interface IShortLinkService
{
    public Task<string> GenerateToken(string url);
    public Task<ShortLink> GoTo(string token);
}