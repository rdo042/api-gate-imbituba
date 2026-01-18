using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarPorId
{
    internal class BuscarPorIdUsuarioHandler(
        IUsuarioRepository UsuarioRepositorio
        ) : ICommandHandler<BuscarPorIdUsuarioQuery, Result<Usuario>>
    {
        private readonly IUsuarioRepository _usuarioRepository = UsuarioRepositorio;

        public async Task<Result<Usuario>> HandleAsync(BuscarPorIdUsuarioQuery command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _usuarioRepository.GetByIdAsync(command.Id)
                    ?? throw new Exception("Usuário não encontrado pelo id +" + command.Id);

                return Result<Usuario>.Success(result);

            }
            catch (Exception ex)
            {
                return Result<Usuario>.Failure("Erro ao realizar login - " + ex.Message);
            }
        }
    }
