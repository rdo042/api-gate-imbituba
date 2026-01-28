using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface IMotoristaRepository: IBaseRepository<Motorista>
    {
        Task<Motorista?> GetByDocumentoAsync(string documento, CancellationToken cancellationToken = default);
    }
}
