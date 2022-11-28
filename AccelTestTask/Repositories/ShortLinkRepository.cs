using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AccelTestTask.Models;
using AccelTestTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccelTestTask.Repositories;

public class ShortLinkRepository : IShortLinkRepository
{
    private readonly DataContext _context;

    public ShortLinkRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ShortLink>> GetAllAsync()
    {
        return await _context.Tokens.ToListAsync();
    }

    public async Task<ShortLink?> GetByIdAsync(int id)
    {
        return await _context.Tokens.FindAsync(id);
    }

    public async Task<ShortLink?> FirstOrDefaultAsync(Expression<Func<ShortLink, bool>> predicate)
    {
        return await _context.Tokens.FirstOrDefaultAsync(predicate);
    }
    
    public void Add(ShortLink todoItem)
    {
        _context.Tokens.Add(todoItem);
    }

    public void Remove(ShortLink todoItem)
    {
        _context.Tokens.Remove(todoItem);
    }

    public async Task<bool> AnyAsync(Expression<Func<ShortLink, bool>> predicate)
    {
        return await _context.Tokens.AnyAsync(predicate);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}