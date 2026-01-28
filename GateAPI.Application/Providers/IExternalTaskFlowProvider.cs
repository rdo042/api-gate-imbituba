using GateAPI.Domain.Entities.Integracao;

namespace GateAPI.Application.Providers
{
    public interface IExternalTaskFlowProvider
    {
        Task<TaskFlowPlateResponse> GetTasksByLicensePlateAsync(
            string licensePlate,
            CancellationToken cancellationToken);
    }
}
