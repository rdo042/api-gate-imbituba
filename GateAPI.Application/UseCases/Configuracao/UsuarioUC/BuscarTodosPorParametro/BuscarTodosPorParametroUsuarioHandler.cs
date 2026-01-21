using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarTodosPorParametro
{
    public class BuscarTodosPorParametroUsuarioHandler(
        IUsuarioRepository UsuarioRepositorio
        ) : ICommandHandler<BuscarTodosPorParametroUsuarioQuery, Result<(IEnumerable<Usuario>, int)>>
    {
        private readonly IUsuarioRepository _usuarioRepository = UsuarioRepositorio;

        public async Task<Result<(IEnumerable<Usuario>, int)>> HandleAsync(BuscarTodosPorParametroUsuarioQuery command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _usuarioRepository.GetAllPaginatedAsync(
                    command.Page,
                    command.PageSize,
                    command.SortColumn,
                    command.SortDirection,
                    command.Nome
                    );

                return Result<(IEnumerable<Usuario>, int)>.Success(result);

            }
            catch (Exception ex)
            {
                return Result<(IEnumerable<Usuario>, int)>.Failure("Erro ao realizar login - " + ex.Message);
            }
        }
    }
}
