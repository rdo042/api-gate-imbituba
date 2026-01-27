using GateAPI.Domain.Enums;
using GateAPI.Domain.Exceptions;
using GateAPI.Domain.ValueObjects;
using System.Runtime.CompilerServices;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class Motorista : BaseEntity
    {
        public string Nome { get; private set; }
        public DateOnly? DataNascimento { get; private set; }
        public RG RG { get; private set; }
        public CPF CPF { get; private set; }
        public CNH CNH { get; private set; }
        public DateOnly? ValidadeCNH { get; private set; }
        public string? Telefone { get; private set; }
        public string? Foto { get; private set; }
        public StatusEnum Status { get; private set; }

        private Motorista(string nome, DateOnly? dataNascimento, string rg, string cpf, string cnh, DateOnly? validadeCNH, string? telefone, string? foto, StatusEnum status)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            RG = new RG(rg);
            CPF = new CPF(cpf); 
            CNH = new CNH(cnh);
            ValidadeCNH = validadeCNH;
            Telefone = telefone;
            Foto = foto;        
            Status = status;

        }

        public static Motorista Create(string nome, DateOnly? dataNascimento, string rg, string cpf, string cnh, DateOnly? validadeCNH, string? telefone, string? foto, StatusEnum status)
        {
            //if (string.IsNullOrWhiteSpace(nome))
            //    throw new DomainRulesException("Nome inválido");

            //if (cpf.Length != 11)
            //    throw new DomainRulesException("CPF inválido");

            var motorista = new Motorista(nome, dataNascimento, rg, cpf, cnh, validadeCNH, telefone, foto, status);
            motorista.CreateId();

            return motorista;
        }

        public static Motorista Load(Guid id, string nome, DateOnly? dataNascimento, string rg, string cpf, string cnh, DateOnly? validadeCNH, string? telefone, string? foto, StatusEnum status)
        {
            var entity = new Motorista(nome, dataNascimento, rg, cpf, cnh, validadeCNH, telefone, foto, status);
            entity.SetId(id);
            return entity;

        }

        public void UpdateEntity(string nome)
        {
            Nome = nome;
        }

        public void UpdateEntity( string nome, DateOnly? dataNascimento)
        {
            UpdateEntity(nome);
            DataNascimento = dataNascimento;
        }

        public void UpdateEntity(string nome, DateOnly? dataNascimento, string rg, string cpf)
        {
            UpdateEntity(nome, dataNascimento);
            RG = new RG(rg);
            CPF = new CPF(cpf);
        }

        public void UpdateEntity(string nome, DateOnly? dataNascimento, string rg, string cpf, string cnh)
        {
            UpdateEntity(nome, dataNascimento, rg, cpf);
            CNH = new CNH(cnh);
        }

        public void UpdateEntity(string nome, DateOnly? dataNascimento, string rg, string cpf, string cnh, string telefone, string foto, StatusEnum status)
        {
            UpdateEntity(nome, dataNascimento, rg, cpf, cnh);
            Telefone = telefone;
            Foto = foto;
            Status = status;
        }

        public void UpdateIfNullEntity(string? nome=null, DateOnly? dataNascimento=null, string? rg=null, string? cpf=null, string? cnh=null, string? telefone= null, string? foto = null, StatusEnum? status = null)
        {
            if (!string.IsNullOrWhiteSpace(nome)) Nome = nome;
            if (dataNascimento != null) DataNascimento = dataNascimento;
            if (!string.IsNullOrWhiteSpace(rg)) RG = new RG(rg);
            if (!string.IsNullOrWhiteSpace(cpf)) CPF = new CPF(cpf);
            if (!string.IsNullOrWhiteSpace(cnh)) CNH = new CNH(cnh);
            if (!string.IsNullOrWhiteSpace(telefone)) Telefone = telefone;
            if (!string.IsNullOrWhiteSpace(foto)) Foto = foto;
            if (status != null) Status = (StatusEnum)status;
        }


    }

    
}