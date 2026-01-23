using GateAPI.Application.Common.DTOs;
using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.PerfilRequest
{
    public record AtualizarPerfilRequest
    (
        string Nome,
        string? Descricao,
        StatusEnum StatusEnum,
        ICollection<PermissaoItem> Permissoes
    )
    {
        public ICollection<PermissaoItem> Permissoes { get; init; } = Permissoes ?? [];
    };    
}
