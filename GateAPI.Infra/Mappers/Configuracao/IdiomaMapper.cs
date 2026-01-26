using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class IdiomaMapper : IMapper<Idioma, IdiomaModel>
    {
        public Idioma ToDomain(IdiomaModel model)
        {
            return Idioma.Load(
                model.Id,
                model.Codigo,
                model.Nome,
                model.Descricao,
                model.Status,
                (CanalEnum)model.Canal,
                model.EhPadrao
            );
        }

        public IdiomaModel ToModel(Idioma entity)
        {
            return new IdiomaModel
            {
                Id = entity.Id,
                Codigo = entity.Codigo,
                Nome = entity.Nome,
                Descricao = entity.Descricao,
                Status = entity.Status,
                Canal = (int)entity.Canal,
                EhPadrao = entity.EhPadrao
            };
        }
    }
}
