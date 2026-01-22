using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class TipoAvariaRepository : BaseRepository<Domain.Entities.Configuracao.TipoAvaria, TipoAvariaModel>, ITipoAvariaRepository
    {
        public TipoAvariaRepository(AppDbContext context, IMapper<Domain.Entities.Configuracao.TipoAvaria, TipoAvariaModel> mapper):base(context, mapper)
        {

        }
    }
}
