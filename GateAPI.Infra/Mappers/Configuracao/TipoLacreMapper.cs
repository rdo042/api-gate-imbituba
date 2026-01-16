using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public static class TipoLacreMapper
    {
        public static TipoLacre ToDomain(TipoLacreModel model)
        {
            return TipoLacre.Load(
                model.Id,
                model.Tipo,
                model.Descricao,
                model.Status
            );
        }
        public static TipoLacreModel ToModel(TipoLacre entity)
        {
            return new TipoLacreModel() { 
                Id = entity.Id,
                Tipo = entity.Tipo,
                Descricao = entity.Descricao,
                Status = entity.Status
            };
        }
    }
}
