namespace GateAPI.Infra.Mappers
{
    public interface IMapper<TDomain, TModel>
    {
        TDomain ToDomain(TModel model);
        TModel ToModel(TDomain entidade);
    }
}
