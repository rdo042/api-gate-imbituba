using GateAPI.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TipoLacreModel> TipoLacre { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TipoLacreModel>(entity =>
            {
                entity.Property(x => x.Status)
                .HasConversion<string>();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseModel>();

            //var httpContext = _httpContextAccessor.HttpContext;
            var userId = "System";

            //if (httpContext != null && httpContext.Items.TryGetValue("UserId", out var userObj))
            //{
            //    userId = userObj?.ToString() ?? "System";
            //}

            foreach (var entry in entries)
            {
                var now = DateTime.Now;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        entry.Entity.CreatedBy = userId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        entry.Entity.UpdatedBy = userId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
