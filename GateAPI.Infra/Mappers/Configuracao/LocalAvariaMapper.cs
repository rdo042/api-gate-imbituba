using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class LocalAvariaMapper : IMapper<LocalAvaria, LocalAvariaModel>
    {
        public LocalAvaria ToDomain(LocalAvariaModel model)
        {
            return LocalAvaria.Load(
                model.Id,
                model.Local,
                model.Descricao,
                model.Status
            );
        }
        public LocalAvariaModel ToModel(LocalAvaria entity)
        {
            return new LocalAvariaModel() { 
                Id = entity.Id,
                Local = entity.Local,
                Descricao = entity.Descricao,
                Status = entity.Status
            };
        }
    }
}
