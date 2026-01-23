//using GateAPI.Application.Common.Models;
//using GateAPI.Application.Providers;
//using GateAPI.Domain.Entities.Integracao;
//using MediatR;

//namespace GateAPI.Application.UseCases.Integracao.ReconhecerPlacaUC
//{
//    internal class ReconhecerPlacaHandler(ILprProvider lprProvider) : IRequestHandler<ReconhecerPlacaCommand, Result<Lpr>>
//    {
//        private readonly ILprProvider _lprProvider = lprProvider;

//        public async Task<Result<Lpr>> Handle(ReconhecerPlacaCommand command, CancellationToken ct)
//        {
//            var result = await _lprProvider.RecognizeAsync(command.ImageBase64, ct);

//            if (result is null)
//                return Result<Lpr>.Failure("Erro ao reconhecer placa");

//            return Result<Lpr>.Success(result);
//        }
//    }
//}
