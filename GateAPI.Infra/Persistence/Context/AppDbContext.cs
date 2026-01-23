using GateAPI.Domain.Entities;
using GateAPI.Infra.Models;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Profile;
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
        public DbSet<LocalAvariaModel> LocalAvaria { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Global Query Filters para Soft Delete

            ApplySoftDeleteQueryFilter(builder);

            /** Para desabilitar global query filters aplique IgnoreQueryFilters na query desejada. (context.Entities.IgnoreQueryFilters())
            * Exemplo: _context.Perfil.IgnoreQueryFilters()
            */


            #endregion  Global Query Filters para Soft Delete

            // Manter configurações de mapeamento de campos de tabelas em arquivos individuais na pasta Profile
            // Segue exemplo de aplicação:
            builder.ApplyConfiguration(new UsuarioModelConfiguration());
            builder.ApplyConfiguration(new TipoLacreModelConfiguration());
            builder.ApplyConfiguration(new TipoAvariaModelConfiguration());
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
                        entry.State = EntityState.Modified;
                        entry.Entity.DeletedAt = now;
                        entry.Entity.DeletedBy = userId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Global Query Filters para Soft Delete
        /// </summary>
        /// <param name="builder"></param>
        private static void ApplySoftDeleteQueryFilter(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes()
                     .Where(t => typeof(BaseModel).IsAssignableFrom(t.ClrType)))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var deletedAtProperty = Expression.Call(
                    typeof(EF),
                    nameof(EF.Property),
                    new[] { typeof(DateTime?) },
                    parameter,
                    Expression.Constant(nameof(BaseModel.DeletedAt))
                );

                var condition = Expression.Equal(
                    deletedAtProperty,
                    Expression.Constant(null, typeof(DateTime?))
                );

                var lambda = Expression.Lambda(
                    typeof(Func<,>).MakeGenericType(entityType.ClrType, typeof(bool)),
                    condition,
                    parameter
                );

                builder.Entity(entityType.ClrType)
                       .HasQueryFilter(lambda);
            }
        }

    }
}
