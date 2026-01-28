using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.BuscarTodos
{
    public class BuscarTodosTipoAvariaHandler : IRequestHandler<BuscarTodosTipoAvariaQuery, Result<PaginatedResultDto<Domain.Entities.Configuracao.TipoAvaria?>>>
    {
        private readonly ITipoAvariaRepository _tipoAvariaRepository;
        public BuscarTodosTipoAvariaHandler(ITipoAvariaRepository tipoAvariaRepository)
        {
            _tipoAvariaRepository = tipoAvariaRepository;
        }

        public async Task<Result<PaginatedResultDto<Domain.Entities.Configuracao.TipoAvaria?>>> Handle(BuscarTodosTipoAvariaQuery request, CancellationToken cancellationToken)
        {
            var tipoAvaria = await _tipoAvariaRepository.GetAllAsync(cancellationToken);

            if (tipoAvaria is null)
                return Result<PaginatedResultDto<Domain.Entities.Configuracao.TipoAvaria?>>.Failure($"Tipo de avaria não encontrado {request.PageNumber}");
           
            
            var paginatedResult = new PaginatedResultDto<Domain.Entities.Configuracao.TipoAvaria?> {
                Data = tipoAvaria,
                TotalCount= tipoAvaria.Count(),
                PageSize= request.PageSize,
                PageNumber= request.PageNumber
            };

            return Result<PaginatedResultDto<Domain.Entities.Configuracao.TipoAvaria?>>.Success(paginatedResult);
        }
    }
}
