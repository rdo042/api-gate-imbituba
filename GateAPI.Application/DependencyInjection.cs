using FluentValidation;
using GateAPI.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GateAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // MediatR with Pipeline Behaviors
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // FluentValidation
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

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

            // Services

            return services;
        }
    }
}
