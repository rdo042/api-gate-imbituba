namespace GateAPI.Domain.Entities.Integracao
{
    public class Lpr(string placa, string inicioProcessamento, string fimProcessamento)
    {
        public string Placa { get; private set; } = placa;
        public string DataInicioProcessamento { get; private set; } = inicioProcessamento;
        public string DataFimProcessamento { get; private set; } = fimProcessamento;
    }
}
