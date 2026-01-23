using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class TipoAvariaMapper : IMapper<TipoAvaria, TipoAvariaModel>
    {
        public TipoAvaria ToDomain(TipoAvariaModel model)
        {
            return TipoAvaria.Load(
                model.Id,
                model.Tipo,
                model.Descricao,
                model.Status
            );
        }

        public TipoAvariaModel ToModel(TipoAvaria entity)
        {
            var model = new TipoAvariaModel();

            model.Id = entity.Id;

            if (!string.IsNullOrWhiteSpace(entity.Tipo)) model.Tipo = entity.Tipo;
            if (!string.IsNullOrWhiteSpace(entity.Descricao)) model.Descricao = entity.Descricao;
            if (entity.Status != null) model.Status = (StatusEnum)entity.Status;

            return model;
        }
    }
}