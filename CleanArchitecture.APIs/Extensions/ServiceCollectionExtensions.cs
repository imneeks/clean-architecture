using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static void RegisterCommandsAndHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        // Scan all types in the given assemblies
        foreach (var assembly in assemblies)
        {
            // Get all types that implement IRequestHandler<TRequest, TResponse>
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            // Register each handler with its respective interface
            foreach (var handlerType in handlerTypes)
            {
                foreach (var interfaceType in handlerType.GetInterfaces())
                {
                    if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    {
                        // Register the handler with transient lifetime
                        services.AddTransient(interfaceType, handlerType);
                    }
                }
            }
        }

        
    }
}
