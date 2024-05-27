using MassTransit;
using MasstransitRabbitMQ.Producer.API.DependencyInjection.Options;

namespace MasstransitRabbitMQ.Producer.API.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfigurationMasstransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var masstransitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

            services.AddMassTransit(options =>
            {
                options.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, config =>
                    {
                        config.Username(masstransitConfiguration.UserName);
                        config.Password(masstransitConfiguration.Password);
                    });
                });
            });
            return services;
        }
    }
}
