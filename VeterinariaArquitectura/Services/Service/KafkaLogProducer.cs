using System.Text.Json;
using Confluent.Kafka;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Services.IService;

namespace VeterinariaArquitectura.Services.Service
{
    public class KafkaLogProducer : ILogProducer, IDisposable
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;
        private readonly ILogger<KafkaLogProducer> _logger;

        public KafkaLogProducer(IConfiguration config, ILogger<KafkaLogProducer> logger)
        {
            _logger = logger;
            _topic = config["Kafka:Topic"]
                ?? throw new InvalidOperationException("Kafka:Topic no está configurado.");
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                MessageTimeoutMs = 5000
            };
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        // Encola el mensaje sin esperar la respuesta de Kafka, para que la operación
        // del usuario no se retrase ni falle si Kafka no está disponible.
        public void Publicar(LogEvento evento)
        {
            try
            {
                var json = JsonSerializer.Serialize(evento);
                _producer.Produce(_topic, new Message<Null, string> { Value = json }, reporte =>
                {
                    if (reporte.Error.IsError)
                    {
                        _logger.LogError("No se pudo entregar el log a Kafka: {Motivo}", reporte.Error.Reason);
                    }
                });
            }
            catch (KafkaException ex)
            {
                _logger.LogError(ex, "No se pudo publicar el log en Kafka");
            }
        }

        public void Dispose()
        {
            _producer.Flush(TimeSpan.FromSeconds(5));
            _producer.Dispose();
        }
    }
}
