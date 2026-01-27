using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.Base
{
    public class BuscarPorIdHandler<TEntity>
    : IRequestHandler<BuscarPorIdQuery<TEntity>, Result<TEntity?>>
    where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BuscarPorIdHandler(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Result<TEntity?>> Handle(
            BuscarPorIdQuery<TEntity> request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity is null)
                return Result<TEntity?>.Failure($"{typeof(TEntity).Name} não encontrado ({request.Id})");

            return Result<TEntity?>.Success(entity);
        }
    }
}
