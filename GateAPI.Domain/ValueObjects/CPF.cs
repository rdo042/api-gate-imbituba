namespace GateAPI.Domain.ValueObjects
{
    public sealed class CPF : IEquatable<CPF>
    {
        public string Value { get; }

        public CPF(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CPF é obrigatório");

            var cpf = SomenteNumeros(value);

            if (cpf.Length != 11)
                throw new ArgumentException("CPF inválido");

            if (!Validar(cpf))
                throw new ArgumentException("CPF inválido");

            Value = cpf;
        }

        private static string SomenteNumeros(string input) =>
            new string(input.Where(char.IsDigit).ToArray());

        private static bool Validar(string cpf)
        {
            if (cpf.Distinct().Count() == 1)
                return false;

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var temp = cpf[..9];
            var soma = temp.Select((t, i) => (t - '0') * mult1[i]).Sum();

            var resto = soma % 11;
            var dig1 = resto < 2 ? 0 : 11 - resto;

            temp += dig1;
            soma = temp.Select((t, i) => (t - '0') * mult2[i]).Sum();

            resto = soma % 11;
            var dig2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith($"{dig1}{dig2}");
        }

        public override string ToString() =>
            Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");

        public bool Equals(CPF? other) =>
            other is not null && Value == other.Value;

        public override bool Equals(object? obj) =>
            Equals(obj as CPF);

        public override int GetHashCode() =>
            Value.GetHashCode();
    }

}
