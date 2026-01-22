using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class TipoAvariaMapper:IMapper<TipoAvaria, TipoAvariaModel>
    {
        public TipoAvaria ToDomain(TipoAvariaModel model)
        {
            return TipoAvaria.Load(
                model.Id,
                model.Tipo,
                model.Descricao ?? "",
                model.Status
            );
        }

        public TipoAvariaModel ToModel(TipoAvaria entity)
        {
            return new TipoAvariaModel { 
                Id = entity.Id,
                Tipo = entity.Tipo,
                Descricao = entity.Descricao,
                Status = entity.Status
            };
        }
    }
}
