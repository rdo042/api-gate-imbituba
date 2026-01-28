namespace GateAPI.Domain.ValueObjects
{
    public sealed class RG : IEquatable<RG>
    {
        public string Value { get; }

        public RG(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("RG é obrigatório");

            var rg = value.Trim().Replace(".", "").Replace("-", "");

            if (rg.Length < 5 || rg.Length > 14)
                throw new ArgumentException("RG inválido");

            if (!rg.All(char.IsLetterOrDigit))
                throw new ArgumentException("RG inválido");

            Value = rg.ToUpperInvariant();
        }

        public override string ToString() => Value;

        public bool Equals(RG? other) =>
            other is not null && Value == other.Value;

        public override bool Equals(object? obj) =>
            Equals(obj as RG);

        public override int GetHashCode() =>
            Value.GetHashCode();
    }

}
