using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.Deletar
{
    public class DeletarUsuarioHandler(
        IUsuarioRepository UsuarioRepositorio
        ) : ICommandHandler<DeletarUsuarioCommand, Result<object?>>
    {
        private readonly IUsuarioRepository _usuarioRepository = UsuarioRepositorio;

        public async Task<Result<object?>> HandleAsync(DeletarUsuarioCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _usuarioRepository.DeleteAsync(command.Id);

            return result ? Result<object?>.Success(null) : Result<object?>.Failure("Usuário não encontrado pelo id - " + command.Id);
        }
    }
}
