using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Application.Providers;

namespace GateAPI.Application.UseCases.Integracao.ReconhecerPlacaUC
{
    internal class ReconhecerPlacaHandler(
        ILprProvider lprProvider) : ICommandHandler<ReconhecerPlacaCommand, Result<string>>
    {
        private readonly ILprProvider _lprProvider = lprProvider;

        //public async Task<Result<string>> Handle(
        //    ReconhecerPlacaCommand command,
        //    CancellationToken ct)
        //{
        //    var result = await _lprProvider.RecognizeAsync(
        //        command.ImageBase64,
        //        ct);

        //    if (result is null)
        //        return ReconhecerPlacaResult.Manual();

        //    if (result.Confidence < _settings.MinConfidence)
        //        return ReconhecerPlacaResult.ConfirmacaoManual(result.Plate);

        //    return ReconhecerPlacaResult.Automatico(result.Plate);
        //}

        public Task<Result<string>> HandleAsync(ReconhecerPlacaCommand command, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
