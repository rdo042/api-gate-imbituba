using GateAPI.Domain.Enums;
public record CriarTipoAvariaRequest(string Tipo, string? Descricao, StatusEnum Status);
