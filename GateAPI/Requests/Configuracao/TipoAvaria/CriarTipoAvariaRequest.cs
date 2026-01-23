using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;
public record CriarTipoAvariaRequest(string Tipo, string? Descricao, StatusEnum Status);

