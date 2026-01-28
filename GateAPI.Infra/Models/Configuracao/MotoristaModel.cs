using GateAPI.Domain.Enums;

namespace GateAPI.Infra.Models.Configuracao
{
    public class MotoristaModel: BaseModel
    {
        public string Nome { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string CNH { get; set; }
        public DateOnly? ValidadeCNH { get; set; }
        public string Telefone { get; set; }
        public string Foto { get; set; }
        public StatusEnum Status { get; set; }
    }
}
