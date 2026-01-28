namespace GateAPI.Infra.Providers.Configuracao
{
    public class ExternalApiOptions
    {
        public const string SectionName = "AdapterApi";
        public string BaseUrl { get; set; } = string.Empty;
    }
}
