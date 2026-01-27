using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.BuscarTodos;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC
{
    public class BuscarTodosMotoristaHandler : IRequestHandler<BuscarTodosMotoristaQuery, Result<PaginatedResultDto<Motorista?>>>
    {
        private readonly IMotoristaRepository _motoristaRepository;
        public BuscarTodosMotoristaHandler(IMotoristaRepository motoristaRepository)
        {
            _motoristaRepository = motoristaRepository;
        }

        public async Task<Result<PaginatedResultDto<Motorista?>>> Handle(BuscarTodosMotoristaQuery request, CancellationToken cancellationToken)
        {
            var motorista = await _motoristaRepository.GetAllAsync(cancellationToken);

            if (motorista is null)
                return Result<PaginatedResultDto<Motorista?>>.Failure($"Motorista não encontrado {request.PageNumber}");
           
            
            var paginatedResult = new PaginatedResultDto<Motorista?> {
                Data = motorista,
                TotalCount= motorista.Count(),
                PageSize= request.PageSize,
                PageNumber= request.PageNumber
            };

            return Result<PaginatedResultDto<Motorista?>>.Success(paginatedResult);
        }
    }
}
