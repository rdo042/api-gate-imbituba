using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class PerfilRepository(AppDbContext context) : IPerfilRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Perfil> AddAsync(Perfil entidade)
        {
            var model = PerfilMapper.ToModel(entidade);

            _context.Perfil.Add(model);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await _context.Perfil.FindAsync(id);
            if (model is null) return;

            _context.Perfil.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Perfil>> GetAllAsync()
        {
            var model = await _context.Perfil
                .AsNoTracking()
                .Include(u => u.Permissoes).ToListAsync();

            return model.Select(PerfilMapper.ToDomain);
        }

        public async Task<Perfil?> GetByIdAsync(Guid id)
        {
            var model = await _context.Perfil
                .Include(u => u.Permissoes)
                .SingleOrDefaultAsync(u => u.Id == id);

            return model == null ? null : PerfilMapper.ToDomain(model);
        }

        public async Task UpdateAsync(Perfil entidade)
        {
            var modelNoBanco = await _context.Perfil
            .Include(p => p.Permissoes)
            .FirstOrDefaultAsync(x => x.Id == entidade.Id);

            if (modelNoBanco == null) return;

            modelNoBanco.Nome = entidade.Nome;
            modelNoBanco.Status = entidade.Status;

            var idsNovasPermissoes = entidade.Permissoes.Select(p => p.Id).ToList();

            var novasPermissoesModel = await _context.Permissao
                .Where(p => idsNovasPermissoes.Contains(p.Id))
                .ToListAsync();

            modelNoBanco.Permissoes = novasPermissoesModel;

            await _context.SaveChangesAsync(); ;
        }
    }
}
