using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.PerfilUC.Criar
{
    public class CriarPerfilHandler(IPerfilRepository perfil) : ICommandHandler<CriarPerfilCommand, Result<Perfil>>
    {
        private readonly IPerfilRepository _perfilRepository = perfil;

        public async Task<Result<Perfil>> HandleAsync(CriarPerfilCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var permissoes = command.Permissoes.Select(item => Permissao.Load(item.Id, item.Nome)).ToList();

                var obj = new Perfil(command.Nome, command.Descricao, permissoes);
                var result = await _perfilRepository.AddAsync(obj);

                return Result<Perfil>.Success(result);

            }
            catch (Exception ex)
            {
                return Result<Perfil>.Failure("Erro ao criar Perfil - " + ex.Message);
            }
        }
    }
}
