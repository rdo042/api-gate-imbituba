namespace GateAPI.Domain.ValueObjects
{
    public sealed class CNH : IEquatable<CNH>
    {
        public string Value { get; }

        public CNH(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CNH é obrigatória");

            var cnh = new string(value.Where(char.IsDigit).ToArray());

            if (cnh.Length != 11)
                throw new ArgumentException("CNH inválida");

            if (cnh.Distinct().Count() == 1)
                throw new ArgumentException("CNH inválida");

            if (!Validar(cnh))
                throw new ArgumentException("CNH inválida");

            Value = cnh;
        }

        private static bool Validar(string cnh)
        {
            int soma = 0;
            int peso = 9;

            for (int i = 0; i < 9; i++)
                soma += (cnh[i] - '0') * peso--;

            int digito1 = soma % 11;
            digito1 = digito1 >= 10 ? 0 : digito1;

            soma = 0;
            peso = 1;

            for (int i = 0; i < 9; i++)
                soma += (cnh[i] - '0') * peso++;

            int digito2 = soma % 11;
            digito2 = digito2 >= 10 ? 0 : digito2;

            return cnh.EndsWith($"{digito1}{digito2}");
        }

        public override string ToString() => Value;

        public bool Equals(CNH? other) =>
            other is not null && Value == other.Value;

        public override bool Equals(object? obj) =>
            Equals(obj as CNH);

        public override int GetHashCode() =>
            Value.GetHashCode();
    }

}
