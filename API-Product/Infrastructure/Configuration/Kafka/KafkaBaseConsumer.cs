using Confluent.Kafka;
using Infrastructure.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Configuration.Kafka;

public abstract class KafkaConsumerBase<TKey, TValue> : BackgroundService
{
    private readonly IKafkaContext _context;
    private readonly ILogger<KafkaConsumerBase<TKey, TValue>> _logger;
    private readonly string _topic;

    private readonly IConsumer<TKey, TValue> _consumer;

    protected virtual IDeserializer<TKey> GetKeyDeserializer() => new KafkaJsonSerializer<TKey>();
    protected virtual IDeserializer<TValue> GetValueDeserializer() => new KafkaJsonSerializer<TValue>();
    protected KafkaConsumerBase(IKafkaContext context, ILogger<KafkaConsumerBase<TKey, TValue>> logger, string topic)
    {
        _context = context;
        _logger = logger;
        _topic = topic;

        _consumer = new ConsumerBuilder<TKey, TValue>(_context.GetConsumerConfig())
            .SetKeyDeserializer(GetKeyDeserializer())
            .SetValueDeserializer(GetValueDeserializer())
            .Build();
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Run(async () =>
        {
            _consumer.Subscribe(_topic);
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _consumer.Consume(cancellationToken);
                    await HandleMessage(cr.Message);
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException)
                        throw;

                    _logger.LogError(ex, "An unhandled exception was thrown in a kafka listener");
                }
            }

            _consumer.Close();
        }, cancellationToken);
    }

    protected abstract Task HandleMessage(Message<TKey, TValue> message);
}