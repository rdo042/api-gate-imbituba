using GateAPI.Domain.Entities.Integracao;

namespace GateAPI.Application.Providers
{
    public interface ILprProvider
    {
        Task<Lpr?> RecognizeAsync(
            string request,
            CancellationToken ct);
    }
}
