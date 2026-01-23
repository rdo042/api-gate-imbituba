namespace GateAPI.Configuration
{
    using Microsoft.AspNetCore.Routing;
    using System.Text.RegularExpressions;

    public class KebabCaseTransformer : IOutboundParameterTransformer
    {
        // Adicionar convenção para kebab case nas controller (endpoint)
        public string TransformOutbound(object value)
        {
            if (value == null) return null;

            return Regex
                .Replace(value.ToString(), "(?<!^)([A-Z])", "-$1")
                .ToLower();
        }
    }

}
