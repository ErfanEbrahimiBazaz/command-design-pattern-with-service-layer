using Command1.Business.Invoker;
using Command1.Business.Receivers;
using Command1.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Command1;

public static class DepedencyExtensions
{
    public static IServiceCollection AddCommandPatternDI(this IServiceCollection services)
    {
        // Register the receiver, TextEditor as a singleton
        services.AddScoped<TextEditor>();

        // Don't register the commands themselves as services
        // commands need constructor parameters (TextEditor, string text, int count).
        //The DI container doesn’t know what "Hello, " or 5 should be.
        //services.AddTransient<Business.Commands.ICommand, Business.Commands.WriteTextCommand>();
        //services.AddTransient<Business.Commands.ICommand, Business.Commands.DeleteLastNCharactersCommand>();

        services.AddScoped<CommandManager>();
        services.AddScoped<EditorCommandService>();
        return services;
    }
}
