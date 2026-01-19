namespace GateAPI.Application.UseCases.Integracao.ReconhecerPlacaUC
{
    public record ReconhecerPlacaCommand(
        string ImageBase64,
        string Origem,
        string Usuario,
        DateTime Data,
        int CheckpointId);
}
