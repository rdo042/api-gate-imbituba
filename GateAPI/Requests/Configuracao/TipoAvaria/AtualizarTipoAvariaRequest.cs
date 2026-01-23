using GateAPI.Domain.Enums;

public record AtualizarTipoAvariaRequest(string Tipo, string? Descricao, StatusEnum Status);

