using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string SenhaHash { get; private set; } = string.Empty;
        public bool EmailConfirmado { get; private set; }
        public Perfil? Perfil { get; private set; }
        public StatusEnum Status { get; private set; }

        public Usuario(string nome, string email, string senhaHash, Perfil? perfil)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Perfil = perfil;
            Status = StatusEnum.ATIVO;
            EmailConfirmado = false;

            Validation();
        }

        private Usuario() { }

        public void UpdateEntity(string nome, string email, string senhaHash, Perfil? perfil, StatusEnum status)
        {
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Perfil = perfil;
            Status = status;

            Validation();
        }

        public static Usuario Load(Guid id, string nome, string email, string senhaHash, Perfil? perfil, StatusEnum status)
        {
            var entidade = new Usuario
            {
                Nome = nome,
                Email = email,
                SenhaHash = senhaHash,
                Perfil = perfil,
                Status = status
            };

            entidade.SetId(id);
            //entidade.SetAudit();
            return entidade;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(this.Nome))
                throw new ArgumentException("O Nome do usuario é obrigatório.");
            if (this.Nome.Length > 50)
                throw new ArgumentException("O tamanho do nome do usuario não é suportado");
            if (string.IsNullOrWhiteSpace(this.Email))
                throw new ArgumentException("O Email do usuario é obrigatório.");
            if (this.Nome.Length > 50)
                throw new ArgumentException("O tamanho do email do usuario não é suportado");
            if (string.IsNullOrWhiteSpace(this.SenhaHash))
                throw new ArgumentException("A Senha do usuario é obrigatória.");
        }
    }
}
