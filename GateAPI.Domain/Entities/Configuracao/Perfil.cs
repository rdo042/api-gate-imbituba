using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class Perfil : BaseEntity
    {
        public string? Nome { get; set; }
        public StatusEnum Status { get; set; }

        public static Perfil Load(Guid id, string nome, StatusEnum status)
        {
            var entidade = new Perfil
            {
                Nome = nome,
                Status = status
            };

            entidade.SetId(id);
            //entidade.SetAudit();
            return entidade;
        }
    }
}
