using MassTransit;
using MasstransitRabbitMQ.Consumer.API.DependencyInjection.Options;
using MasstransitRabbitMQ.Consumer.API.MessageBus.Consumers.Events;
using System.Reflection;

namespace MasstransitRabbitMQ.Consumer.API.DependencyInjection.Extentions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfigurationMasstransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var masstransitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

            services.AddMassTransit(options =>
            {
                options.AddConsumers(Assembly.GetExecutingAssembly());
                options.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, config =>
                    {
                        config.Username(masstransitConfiguration.UserName);
                        config.Password(masstransitConfiguration.Password);
                    });
                    bus.ConfigureEndpoints(context);
                });
            });
            return services;
        }

        public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
            return services;
        }

    }
}
