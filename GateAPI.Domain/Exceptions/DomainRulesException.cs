namespace GateAPI.Domain.Exceptions
{
    public class DomainRulesException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public DomainRulesException(string error)
            : base(error)
        {
            Errors = new[] { error };
        }

        public DomainRulesException(IEnumerable<string> errors)
            : base("Erro de validação de domínio")
        {
            Errors = errors.ToList().AsReadOnly();
        }
    }

}
