using Microsoft.EntityFrameworkCore;

namespace LensUp.BackOfficeService.Infrastructure.Database;

internal sealed class BackOfficeApplicationDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
