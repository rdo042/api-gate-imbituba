using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Requests
{
    public class ChangeBaseRequest<T>
    {
        [FromRoute] public Guid id { get; set; }
        [FromBody] public T Data { get; set; }
    }
}
