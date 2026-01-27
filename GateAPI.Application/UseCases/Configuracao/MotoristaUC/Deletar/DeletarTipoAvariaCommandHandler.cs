
using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Deletar
{
    public class DeletarMotoristaCommandHandler: IRequestHandler<DeletarMotoristaCommand, Result<Guid>>
    {
        private readonly IMotoristaRepository _repository;

        public DeletarMotoristaCommandHandler(IMotoristaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(DeletarMotoristaCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _repository.DeleteAsync(command.Id, cancellationToken);

            if (!result)
                return Result<Guid>.Failure("Motorista não encontrado para exclusão.");

            return Result<Guid>.Success(command.Id);

        }
    }
}
