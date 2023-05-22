using BuildingHealth.Core.Models;
using BuildingHealth.DAL;
using BuildingHealth.Mqtt.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Text.Json;

namespace BuildingHealth.Mqtt
{
    public class MqttBackgroundTask : BackgroundService
    {
        private readonly IManagedMqttClient _mqttClient;
        private readonly ManagedMqttClientOptions _options;
        private readonly BuildingHealthDBContext _dbContext;

        public MqttBackgroundTask(
            IManagedMqttClient mqttClient, 
            ManagedMqttClientOptions options, 
            BuildingHealthDBContext dbContext)
        {
            _mqttClient = mqttClient;
            _options = options;
            _dbContext = dbContext;

            _mqttClient.ConnectedAsync += OnConnectedAsync;
            _mqttClient.DisconnectedAsync += OnDisconnectedAsync;
            _mqttClient.ConnectingFailedAsync += OnConnectingFailedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += OnApplicationMessageRecievedAsync;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectToBrocker();
        }

        private async Task ConnectToBrocker()
        {
            await _mqttClient.StartAsync(_options);
            await _mqttClient.SubscribeAsync("constructionsMeasurings");
        }

        private Task OnConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine("Service connected to the brocker");
            return Task.CompletedTask;
        }

        private Task OnDisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine("Service disconnected from the brocker");
            return Task.CompletedTask;
        }

        private Task OnConnectingFailedAsync(ConnectingFailedEventArgs arg)
        {
            Console.WriteLine("Connection failed check network or broker!");
            return Task.CompletedTask;
        }

        private async Task OnApplicationMessageRecievedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("##################################");
            Console.WriteLine($"Recieved message from brocker. Trying to save...");
            var str = arg.ApplicationMessage.ConvertPayloadToString();
            Console.WriteLine(str);

            JObject jObject = JObject.Parse(str);
            var response = jObject.ToObject<ResponseModel>();

            await SaveResponseAsync(response);
            Console.WriteLine("##################################");
        }

        private async Task SaveResponseAsync(ResponseModel response)
        {
            try
            {
                _dbContext.SensorsResponses.Add(response.GetDbModel());
                await _dbContext.SaveChangesAsync();
                Console.WriteLine($"Message saved from brocker");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Can't properly save response, see inner error:");
                Console.WriteLine(ex.Message);

                // Check for inner exceptions
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine("Inner Exception:");
                    Console.WriteLine(innerException.Message);
                    innerException = innerException.InnerException;
                }
            }
        }
    }
}
