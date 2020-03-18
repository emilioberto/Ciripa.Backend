using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ciripa.Business
{
    public static class BusinessConfiguration
    {
        public static IServiceCollection AddBusiness(this IServiceCollection source) =>
            source
                .AddMediatR(Assembly.GetExecutingAssembly());

        // .AddScoped(KidService)
        // .AddScoped(PresenceService);
    }
}