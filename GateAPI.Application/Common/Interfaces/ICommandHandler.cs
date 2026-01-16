namespace GateAPI.Application.Common.Interfaces
{
    namespace IrisApi.Application.Common.Interfaces
    {
        public interface ICommandHandler<TCommand, TResult>
        {
            Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
        }
    }
}
