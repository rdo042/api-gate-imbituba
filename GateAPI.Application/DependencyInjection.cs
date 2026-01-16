using Microsoft.Extensions.DependencyInjection;
using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // UseCases
            services.AddScoped<BuscarPorIdTipoLacreHandler>();
            services.AddScoped<CriarTipoLacreHandler>();
            services.AddScoped<AtualizarTipoLacreHandler>();
            services.AddScoped<DeletarTipoLacreHandler>();

            // Services

            return services;
        }
    }
}
