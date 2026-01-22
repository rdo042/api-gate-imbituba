using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class UsuarioRepository(
        AppDbContext context
        ) : IUsuarioRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            var model = await _context.Usuario
                .Include(u => u.Perfil)
                .SingleOrDefaultAsync(u => u.Email == email);

            return model == null ? null : UsuarioMapper.ToDomain(model);
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            var model = await _context.Usuario
                .Include(u => u.Perfil)
                .SingleOrDefaultAsync(u => u.Id == id);

            return model == null ? null : UsuarioMapper.ToDomain(model);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var query = await _context.Usuario
                .AsNoTracking()
                .ToListAsync();

            return query.Select(UsuarioMapper.ToDomain);
        }

        public async Task<Usuario> AddAsync(Usuario entidade)
        {
            var model = UsuarioMapper.ToModel(entidade);

            _context.Usuario.Add(model);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task UpdateAsync(Usuario entidade)
        {
            var model = UsuarioMapper.ToModel(entidade);

            _context.Usuario.Update(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await _context.Usuario.FindAsync(id) ?? throw new KeyNotFoundException($"Usuário com Id {id} não encontrado.");

            _context.Usuario.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Usuario>, int)> GetAllPaginatedAsync(int page, int pageSize, string? sortColumn, string sortDirection, string? nome)
        {
            var query = _context.Usuario.AsNoTracking();

            if (nome is not null)
            {
                var nomeNormalizado = nome.ToLower();
                query = query.Where(x =>
                    EF.Functions.Like(x.Nome, $"%{nomeNormalizado}%")
                );
            }

            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = sortDirection == "asc"
                    ? query.OrderBy(p => EF.Property<object>(p, sortColumn))
                    : query.OrderByDescending(p => EF.Property<object>(p, sortColumn));
            }
            else
            {
                query = query.OrderBy(p => p.CreatedAt);
            }

            int totalCount = await query.CountAsync();

            var entidades = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (entidades.Select(UsuarioMapper.ToDomain), totalCount);
        }
    }
}
