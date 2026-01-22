using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using Microsoft.AspNetCore.Mvc;

public class AlterarTipoAvariaRequest
{
    [FromRoute] public Guid Id { get; set; }
    [FromBody] public AtualizarTipoLacreCommand TipoLacre { get; set; }
}
