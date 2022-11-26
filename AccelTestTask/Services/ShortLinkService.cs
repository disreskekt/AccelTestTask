using System;
using System.Threading.Tasks;
using AccelTestTask.Exceptions;
using AccelTestTask.Models;
using AccelTestTask.Repositories.Interfaces;
using AccelTestTask.Services.Interfaces;

namespace AccelTestTask.Services;

public class ShortLinkService : IShortLinkService
{
    private readonly IShortLinkRepository _repository;

    public ShortLinkService(IShortLinkRepository repository)
    {
        _repository = repository;
    }

    public void ValidateUri(string uri)
    {
        if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
        {
            throw new UriFormatException("Wrong uri format");
        }
    }
    
    public async Task<string> GenerateToken(string uri)
    {
        ShortLink? shortLink = await _repository.FirstOrDefaultAsync(tkn => tkn.Link == uri);
            
        if (shortLink is not null)
        {
            return shortLink.Token;
        }
            
        string newToken = $"{uri.GetHashCode():X}";
            
        while (true)
        {
            shortLink = await _repository.FirstOrDefaultAsync(tkn => tkn.Token == newToken);

            if (shortLink is null)
            {
                break;
            }
                
            newToken = $"{(uri + Guid.NewGuid()).GetHashCode():X}";
        }
            
        _repository.Add(new ShortLink()
        {
            Link = uri,
            Token = newToken
        });
        await _repository.SaveChangesAsync();

        return newToken;
    }
    
    public async Task<ShortLink> GoTo(string token)
    {
        ShortLink? shortLink = await _repository.FirstOrDefaultAsync(tkn => tkn.Token == token);

        if (shortLink is null)
        {
            throw new NotFoundException();
        }

        return shortLink;
    }
}