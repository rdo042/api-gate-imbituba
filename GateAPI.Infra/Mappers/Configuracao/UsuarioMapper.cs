using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public static class UsuarioMapper
    {
        public static Usuario ToDomain(UsuarioModel model)
        {
            ICollection<Permissao> permissoes = [];
            if (model.Perfil?.Permissoes.Count != 0)
                permissoes = (model.Perfil?.Permissoes.Select(item => Permissao.Load(item.Id, item.Nome)).ToList() ?? []);

            Perfil? perfil = null;
            if (model.Perfil != null)
                perfil = Perfil.Load(model.Perfil.Id, model.Perfil.Nome, model.Perfil.Descricao, model.Status, permissoes);

            return Usuario.Load(
                model.Id,
                model.Nome,
                model.Email,
                model.SenhaHash,
                perfil,
                model.Status
            );
        }
        public static UsuarioModel ToModel(Usuario entity)
        {
            return new UsuarioModel()
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Email = entity.Email,
                SenhaHash = entity.SenhaHash,
                PerfilId = entity.Perfil?.Id,
                Status = entity.Status
            };
        }
    }
}
