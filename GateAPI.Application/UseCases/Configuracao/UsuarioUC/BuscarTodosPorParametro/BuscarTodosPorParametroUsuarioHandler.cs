using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarTodosPorParametro
{
    public class BuscarTodosPorParametroUsuarioHandler(
        IUsuarioRepository UsuarioRepositorio
        ) : ICommandHandler<BuscarTodosPorParametroUsuarioQuery, Result<ListaPaginada>>
    {
        private readonly IUsuarioRepository _usuarioRepository = UsuarioRepositorio;

        public async Task<Result<ListaPaginada>> HandleAsync(BuscarTodosPorParametroUsuarioQuery command, CancellationToken cancellationToken = default)
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

                return Result<ListaPaginada>.Success(new ListaPaginada(result.Item1, result.Item2));

            }
            catch (Exception ex)
            {
                return Result<ListaPaginada>.Failure("Erro ao realizar login - " + ex.Message);
            }
        }
    }
}
