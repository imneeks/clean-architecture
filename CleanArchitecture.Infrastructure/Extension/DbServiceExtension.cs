using CleanArchitecture.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Extension
{
    public static class DbServiceExtension
    {
        public static void AddInfractureServices(this IServiceCollection services) {
            // Load the assembly of both the Application Core and Infrastructure projects
            var coreAssembly = Assembly.GetAssembly(typeof(CoreMarker)); // This targets Application Core
            var infrastructureAssembly = Assembly.GetAssembly(typeof(InfrastructureMarker)); // This targets Infrastructure

            // Get all types from both assemblies
            var coreTypes = coreAssembly.GetTypes().Where(t => t.IsInterface).ToList();
            var infrastructureTypes = infrastructureAssembly.GetTypes().Where(t => !t.IsInterface).ToList();

            // Loop through all interfaces and find matching implementations in the Infrastructure project
            foreach (var interfaceType in coreTypes)
            {
                var implementationType = infrastructureTypes.FirstOrDefault(t => interfaceType.Name == "I" + t.Name);

                if (implementationType != null)
                {
                    // Register the implementation as scoped
                    services.AddScoped(interfaceType, implementationType);
                }
            }
        }
    }
}
