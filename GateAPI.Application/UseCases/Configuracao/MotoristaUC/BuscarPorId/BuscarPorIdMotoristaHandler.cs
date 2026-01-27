//using GateAPI.Application.Common.Models;
//using GateAPI.Domain.Entities.Configuracao;
//using GateAPI.Domain.Repositories.Configuracao;
//using MediatR;

//namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC
//{
//    public class BuscarPorIdMotoristaHandler : IRequestHandler<BuscarPorIdMotoristaQuery, Result<Motorista?>>
//    {
//        private readonly IMotoristaRepository _motoristaRepository;
//        public BuscarPorIdMotoristaHandler(IMotoristaRepository motoristaRepository)
//        {
//            _motoristaRepository = motoristaRepository;
//        }

//        public async Task<Result<Motorista?>> Handle(BuscarPorIdMotoristaQuery request, CancellationToken cancellationToken)
//        {
//            var motorista = await _motoristaRepository.GetByIdAsync(request.Id);

//            if (motorista is null)
//                return Result<Motorista?>.Failure($"Motorista não encontrado {request.Id}");

//            return Result<Motorista?>.Success(motorista);
//        }
//    }
//}
