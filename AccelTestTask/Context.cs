using Microsoft.EntityFrameworkCore;

namespace AccelTestTask;

public class DataContext : DbContext
{
    public DbSet<ShortLink> Tokens { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}