using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using System;

namespace GateAPI.Tests.Entities.Configuracao.Stubs
{
    public static class MotoristaStub
    {
        public static Motorista Valid()
        {
            return Motorista.Create(
                nome: "João da Silva",
                dataNascimento: new DateOnly(1985, 5, 20),
                rg: "426812177",
                cpf: "11805302094",
                cnh: "87105792980",
                validadeCNH: new DateOnly(2026, 12, 31),
                telefone: "31999999999",
                foto: "https://example.com/foto.jpg",
                status: StatusEnum.ATIVO
            );
        }

        public static Motorista Valid01()
        {
            return Motorista.Create(
            nome: "Jane Silva",
            dataNascimento: new DateOnly(1990, 3, 15),
            rg: "351258772",
            cpf: "80333302010",
            cnh: "54509817637",
            validadeCNH: new DateOnly(2027, 6, 30),
            telefone: "31988888888",
            foto: "https://example.com/foto2.jpg",
            status: StatusEnum.ATIVO);
        }
        

        public static Motorista Invalid()
        {
            return Motorista.Create(
                nome: "AB",
                dataNascimento: null,
                rg: "000",
                cpf: "000",
                cnh: "000",
               validadeCNH: null,
                telefone: "",
                foto: null,
                status: StatusEnum.INATIVO
            );
        }

        public static Motorista WithCustomData(
            string nome = "João da Silva",
            DateOnly? dataNascimento = null,
            string rg = "218131094",
            string cpf = "55231890065",
            string cnh = "12722978818",
            DateOnly? validadeCnh = null,
            string? telefone = null,
            string? foto = null,
            StatusEnum status = StatusEnum.ATIVO)
        {
            return Motorista.Create(
                nome: nome,
                dataNascimento: dataNascimento,
                rg: rg,
                cpf: cpf,
                cnh: cnh,
                validadeCNH: validadeCnh,
                telefone: telefone,
                foto: foto,
                status: status
            );
        }
    }
}