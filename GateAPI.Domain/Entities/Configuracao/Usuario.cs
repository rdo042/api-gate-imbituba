using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string SenhaHash { get; private set; } = string.Empty;
        public bool EmailConfirmado { get; private set; }
        public string? LinkFoto { get; private set; } 
        public Perfil? Perfil { get; private set; }
        public StatusEnum Status { get; private set; }

        public Usuario(string nome, string email, string senhaHash, string? linkFoto, Perfil? perfil)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Perfil = perfil;
            Status = StatusEnum.ATIVO;
            EmailConfirmado = false;
            LinkFoto = linkFoto;

            Validation();
        }

        private Usuario() { }

        public void UpdateEntity(string nome, string email, string senhaHash, string? linkFoto, Perfil? perfil, StatusEnum status)
        {
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Perfil = perfil;
            Status = status;
            LinkFoto = linkFoto;

            Validation();
        }

        public void UpdateStatus(StatusEnum status)
        {
            Status = status;
        }

        public static Usuario Load(
            Guid id, string nome, string email, string senhaHash, bool emailConfirmado, string? linkFoto, Perfil? perfil, StatusEnum status
            )
        {
            var entidade = new Usuario
            {
                Nome = nome,
                Email = email,
                SenhaHash = senhaHash,
                EmailConfirmado = emailConfirmado,
                Perfil = perfil,
                Status = status,
                LinkFoto = linkFoto,
            };

            entidade.SetId(id);
            return entidade;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(this.Nome))
                throw new ArgumentException("O Nome do usuario é obrigatório.");
            if (this.Nome.Length > 100)
                throw new ArgumentException("O tamanho do nome do usuario não é suportado - 100");
            if (string.IsNullOrWhiteSpace(this.Email))
                throw new ArgumentException("O Email do usuario é obrigatório.");
            if (this.Email.Length > 150)
                throw new ArgumentException("O tamanho do email do usuario não é suportado - 150");
            if (string.IsNullOrWhiteSpace(this.SenhaHash))
                throw new ArgumentException("A Senha do usuario é obrigatória.");
            if (this.SenhaHash.Length > 255)
                throw new ArgumentException("O tamanho da senha do usuario não é suportado - 255");
            if (this.LinkFoto?.Length > 255)
                throw new ArgumentException("O tamanho do link de foto do usuario não é suportado - 255");
        }
    }
}
