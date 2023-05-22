using BuildingHealth.DAL;
using Microsoft.EntityFrameworkCore;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace BuildingHealth.Mqtt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BuildingHealthDBContext>(options =>
                options.UseSqlServer("Data Source=PRUDIUSVLADPC\\DEV;Initial Catalog=BuildingHealthDB;Integrated Security=True"),
                ServiceLifetime.Singleton);

            builder.Services.AddSingleton<IManagedMqttClient>(i =>
                new MqttFactory().CreateManagedMqttClient());

            builder.Services.AddSingleton<ManagedMqttClientOptions>(i =>
            {
                MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                .WithClientId("Mqtt-client-service")
                .WithTcpServer("test.mosquitto.org");

                return new ManagedMqttClientOptionsBuilder()
                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                    .WithClientOptions(builder.Build())
                    .Build();
            });

            builder.Services.AddHostedService<MqttBackgroundTask>();

            // Add services to the container.

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.Run();
        }
    }
}