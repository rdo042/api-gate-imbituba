using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarPorId
{
    public class BuscarPorIdUsuarioHandler(
        IUsuarioRepository UsuarioRepositorio
        ) : ICommandHandler<BuscarPorIdUsuarioQuery, Result<Usuario>>
    {
        private readonly IUsuarioRepository _usuarioRepository = UsuarioRepositorio;

        public async Task<Result<Usuario>> HandleAsync(BuscarPorIdUsuarioQuery command, CancellationToken cancellationToken = default)
        {
            var result = await _usuarioRepository.GetByIdAsync(command.Id);

            if(result == null)
                return Result<Usuario>.Failure("Usuário não encontrado pelo id " + command.Id);

            return Result<Usuario>.Success(result);
        }
    }
}
