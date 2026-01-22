using Microsoft.Extensions.DependencyInjection;

namespace GateAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // UseCases
            services.AddScoped<UseCases.Configuracao.UsuarioUC.BuscarPorId.BuscarPorIdUsuarioHandler>();
            services.AddScoped<UseCases.Configuracao.UsuarioUC.BuscarTodosPorParametro.BuscarTodosPorParametroUsuarioHandler>();
            services.AddScoped<UseCases.Configuracao.UsuarioUC.Criar.CriarUsuarioHandler>();
            services.AddScoped<UseCases.Configuracao.UsuarioUC.Atualizar.AtualizarUsuarioHandler>();
            services.AddScoped<UseCases.Configuracao.UsuarioUC.Login.LoginUsuarioHandler>();
            services.AddScoped<UseCases.Configuracao.UsuarioUC.Deletar.DeletarUsuarioHandler>();
            services.AddScoped<UseCases.Configuracao.UsuarioUC.AlterarStatus.AlterarStatusUsuarioHandler>();

            services.AddScoped<UseCases.Configuracao.PerfilUC.BuscarPorId.BuscarPorIdPerfilHandler>();
            services.AddScoped<UseCases.Configuracao.PerfilUC.BuscarTodos.BuscarTodosPerfilHandler>();
            services.AddScoped<UseCases.Configuracao.PerfilUC.Criar.CriarPerfilHandler>();
            services.AddScoped<UseCases.Configuracao.PerfilUC.Atualizar.AtualizarPerfilHandler>();
            services.AddScoped<UseCases.Configuracao.PerfilUC.Deletar.DeletarPerfilHandler>();

            services.AddScoped<UseCases.Configuracao.TipoLacreUC.BuscarPorId.BuscarPorIdTipoLacreHandler>();
            services.AddScoped<UseCases.Configuracao.TipoLacreUC.Criar.CriarTipoLacreHandler>();
            services.AddScoped<UseCases.Configuracao.TipoLacreUC.Atualizar.AtualizarTipoLacreHandler>();
            services.AddScoped < UseCases.Configuracao.TipoLacreUC.Deletar.DeletarTipoLacreHandler>();

            // Services

            return services;
        }
    }
}
