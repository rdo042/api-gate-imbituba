using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.Deletar
{
    public class DeletarIdiomaHandler(IIdiomaRepository idiomaRepository) : IRequestHandler<DeletarIdiomaCommand, Result<string>>
    {
        private readonly IIdiomaRepository _idiomaRepository = idiomaRepository;

        public async Task<Result<string>> Handle(DeletarIdiomaCommand command, CancellationToken cancellationToken = default)
        {
            var resultado = await _idiomaRepository.DeleteAsync(command.Id, cancellationToken);

            if (!resultado)
                return Result<string>.Failure("Idioma não encontrado");

            return Result<string>.Success("Idioma deletado com sucesso");
        }
    }
}
