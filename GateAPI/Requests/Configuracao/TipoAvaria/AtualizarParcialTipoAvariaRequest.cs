using GateAPI.Domain.Enums;

public record AtualizarParcialTipoAvariaRequest(string? Tipo = null, string? Descricao = null, StatusEnum? Status= null);

