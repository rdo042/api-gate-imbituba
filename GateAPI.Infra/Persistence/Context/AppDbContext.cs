using GateAPI.Domain.Entities;
using GateAPI.Infra.Models;
using GateAPI.Infra.Models.Configuracao;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace GateAPI.Infra.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<PerfilModel> Perfil { get; set; }
        public DbSet<PermissaoModel> Permissao { get; set; }
        public DbSet<TipoLacreModel> TipoLacre { get; set; }
        public DbSet<TipoAvariaModel> TipoAvaria { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Global Query Filters para Soft Delete
            
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                    continue;

                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var deletedAtProperty = Expression.Property(
                    parameter,
                    nameof(BaseEntity.DeletedAt));

                var nullConstant = Expression.Constant(null, typeof(DateTime?));

                var filter = Expression.Equal(deletedAtProperty, nullConstant);

                var lambda = Expression.Lambda(filter, parameter);

                builder.Entity(entityType.ClrType)
                       .HasQueryFilter(lambda);
            }

            /** Para desabilitar global query filters aplique IgnoreQueryFilters na query desejada. (context.Entities.IgnoreQueryFilters())
            * Exemplo: _context.Perfil.IgnoreQueryFilters()
            */


            #endregion  Global Query Filters para Soft Delete

            //TODO: Adicionar via profile.
            
            builder.Entity<UsuarioModel>(entity =>
            {
                entity.HasIndex(x => x.Email).IsUnique();
            });

            builder.Entity<TipoLacreModel>(entity =>
            {
                entity.Property(x => x.Status)
                .HasConversion<string>();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseModel>();

            var httpContext = _httpContextAccessor.HttpContext;
            var userId = "System";

            if (httpContext != null && httpContext.Items.TryGetValue("UserId", out var userObj))
            {
                userId = userObj?.ToString() ?? "System";
            }

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
                    case EntityState.Deleted:
                        entry.Entity.DeletedAt = now;
                        entry.Entity.DeletedBy = userId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
