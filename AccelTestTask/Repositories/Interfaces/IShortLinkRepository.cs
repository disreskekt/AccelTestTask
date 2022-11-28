using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AccelTestTask.Models;

namespace AccelTestTask.Repositories.Interfaces;

public interface IShortLinkRepository
{
    public Task<IEnumerable<ShortLink>> GetAllAsync();
    public Task<ShortLink?> GetByIdAsync(int id);
    public Task<ShortLink?> FirstOrDefaultAsync(Expression<Func<ShortLink, bool>> predicate);
    public void Add(ShortLink todoItem);
    public void Remove(ShortLink todoItem);
    public Task<bool> AnyAsync(Expression<Func<ShortLink, bool>> predicate);
    public Task SaveChangesAsync();
}