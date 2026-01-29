using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.MotoristaUC.Atualizar
{
    public record AtualizarMotoristaCommand(Guid Id,
    string Nome,
    DateOnly? DataNascimento,
    string RG,
    string CPF,
    string CNH,
    DateOnly? ValidadeCnh,
    string? Telefone,
    string? Foto,
    StatusEnum Status = StatusEnum.ATIVO) : IRequest<Result<Guid>>;
}