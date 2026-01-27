namespace GateAPI.Domain.Validators
{
    public interface IValidator<T>
    {        void Validar(T entidade);
    }
}
