using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
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

        public Task<IEnumerable<Usuario>> GetAllAsync()
        {
            throw new NotImplementedException();
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

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
