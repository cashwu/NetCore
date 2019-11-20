using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace testScopeContext.Models
{
    public class MyDbContext : DbContext, IMyDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customers> Customers { get; set; }
    }

    public interface IMyDbContext
    {
        DbSet<Customers> Customers { get; set; }

        int SaveChanges();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}