using System.Text.Json;
using Confluent.Kafka;
using VeterinariaArquitectura.Data;
using VeterinariaArquitectura.Models;

namespace VeterinariaArquitectura.Services.Service
{
    public class KafkaLogConsumer : BackgroundService
    {
        private readonly IConfiguration _config;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<KafkaLogConsumer> _logger;

        public KafkaLogConsumer(IConfiguration config, IServiceScopeFactory scopeFactory,
            ILogger<KafkaLogConsumer> logger)
        {
            _config = config;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        // Consume() bloquea el hilo, por eso se ejecuta en Task.Run y no en el hilo de arranque.
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => Consumir(stoppingToken), stoppingToken);
        }

        private async Task Consumir(CancellationToken ct)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _config["Kafka:BootstrapServers"],
                GroupId = "petcare-logs-db",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            consumer.Subscribe(_config["Kafka:Topic"]!);

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    var resultado = consumer.Consume(ct);
                    if (resultado?.Message == null)
                    {
                        continue;
                    }

                    var evento = JsonSerializer.Deserialize<LogEvento>(resultado.Message.Value);
                    if (evento != null)
                    {
                        evento.LogEventoId = 0;
                        using var scope = _scopeFactory.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        db.LogEventos.Add(evento);
                        await db.SaveChangesAsync(ct);
                    }

                    // El mensaje se confirma solo después de guardarlo en la base de datos.
                    consumer.Commit(resultado);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error procesando un log de Kafka");
                    await Task.Delay(TimeSpan.FromSeconds(2), ct);
                }
            }

            consumer.Close();
        }
    }
}
