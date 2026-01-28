using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.MotoristaUC.Criar
{
    public record CriarMotoristaCommand(
        string Nome,
        DateOnly? DataNascimento,
        string RG,
        string CPF,
        string CNH,
        DateOnly? ValidadeCnh,
        string? Telefone,
        string? Foto,
        StatusEnum Status = StatusEnum.ATIVO) : IRequest<Result<Motorista>>;
}