using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Application.Common.Models
{
    public class PaginacaoRequest
    {
        [FromQuery] public int? PageNumber { get; set; }
        [FromQuery] public int? PageSize { get; set; } = 20;
    }
}
