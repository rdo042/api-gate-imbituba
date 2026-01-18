using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public static class PerfilMapper
    {
        public static Perfil ToDomain(PerfilModel model)
        {
            ICollection<Permissao> permissoes = [];
            if (model.Permissoes.Count != 0)
                permissoes = (model.Permissoes.Select(item => Permissao.Load(item.Id, item.Nome)).ToList() ?? []);

            return Perfil.Load(model.Id, model.Nome, model.Descricao, model.Status, permissoes);
        }
        public static PerfilModel ToModel(Perfil entity)
        {
            ICollection<PermissaoModel> permissoes = [];
            if (entity.Permissoes.Count != 0)
                permissoes = [.. entity.Permissoes.Select(item => new PermissaoModel { Id = item.Id, Nome = item.Nome})];

            return new PerfilModel()
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Descricao = entity.Descricao,
                Status = entity.Status,
                Permissoes = permissoes
            };
        }
    }
}
