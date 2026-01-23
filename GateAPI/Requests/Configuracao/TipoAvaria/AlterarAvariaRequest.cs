using GateAPI.Domain.Enums;
public record AlterarAvariaRequest(string? Tipo, string? Descricao, StatusEnum Status = StatusEnum.ATIVO);
