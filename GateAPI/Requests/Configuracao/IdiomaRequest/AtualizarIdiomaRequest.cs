using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.IdiomaRequest
{
    public record AtualizarIdiomaRequest(
        string Codigo,
        string Nome,
        string? Descricao,
        StatusEnum Status,
        CanalEnum Canal
    );
}
