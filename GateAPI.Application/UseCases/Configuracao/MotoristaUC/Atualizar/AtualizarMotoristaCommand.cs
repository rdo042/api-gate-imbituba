using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar
{
    public record AtualizarMotoristaCommand(Guid Id,
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    string Nome,
    DateOnly? DataNascimento,
    string RG,
    [MinLength(11, ErrorMessage = "CPF deve ter no mínimo 11 caracteres")]
    string CPF,
    string CNH,
    DateOnly? ValidadeCnh,
    string? Telefone,
    string? Foto,
    StatusEnum Status = StatusEnum.ATIVO) : IRequest<Result<Guid>>;
}