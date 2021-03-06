using JPProject.Domain.Core.Events;
using JPProject.Domain.Core.Interfaces;
using JPProject.EntityFrameworkCore.EventSourcing;
using JPProject.EntityFrameworkCore.Interfaces;
using JPProject.EntityFrameworkCore.Repository;
using JPProject.Sso.EntityFramework.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JPProject.Sso.EntityFramework.Repository.Configuration
{
    public static class ContextConfiguration
    {
        public static IJpProjectConfigurationBuilder AddSsoContext<TContext, TEventStore>(this IJpProjectConfigurationBuilder services)
            where TContext : class, ISsoContext
            where TEventStore : class, IEventStoreContext

        {
            services.Services.TryAddScoped<IEventStore, SqlEventStore>();
            services.Services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.Services.TryAddScoped<IEventStoreContext, TEventStore>();
            services.Services.AddScoped<ISsoContext, TContext>();
            services.Services.TryAddScoped<IJpEntityFrameworkStore>(x => x.GetRequiredService<TContext>());
            services.Services.AddStores();
            return services;
        }
        public static IJpProjectConfigurationBuilder AddSsoContext<TContext>(this IJpProjectConfigurationBuilder services)
            where TContext : class, ISsoContext, IEventStoreContext

        {
            services.Services.TryAddScoped<IEventStore, SqlEventStore>();
            services.Services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.Services.AddScoped<ISsoContext, TContext>();
            services.Services.TryAddScoped<IEventStoreContext>(s => s.GetService<TContext>());
            services.Services.TryAddScoped<IJpEntityFrameworkStore>(x => x.GetRequiredService<TContext>());
            services.Services.AddStores();
            return services;
        }
    }
}
