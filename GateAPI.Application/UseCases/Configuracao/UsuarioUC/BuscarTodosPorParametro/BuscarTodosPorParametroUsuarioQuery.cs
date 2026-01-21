namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarTodosPorParametro
{
    public record BuscarTodosPorParametroUsuarioQuery
        (int Page, int PageSize, string? SortColumn, string SortDirection, string? Nome);
}
