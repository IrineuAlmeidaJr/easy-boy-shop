using Application.Interface;
using Confluent.Kafka;
using Domain.Event;
using Domain.Interface;
using Infrastructure.Configuration.Kafka;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Incoming.Messaging;

public class StockRegisteredKafkaListener : KafkaConsumerBase<Ignore, ProductCreatedEvent>
{
    private readonly ILogger<StockRegisteredKafkaListener> _logger;
    private readonly IStockServices _stockService;

    public StockRegisteredKafkaListener(
        IStockServices stockService,
        IKafkaContext kafkaContext,
        ILogger<StockRegisteredKafkaListener> logger) : base(kafkaContext, logger, "infoeste.api-product.product.created")
    {
        _logger = logger;
        _stockService = stockService;
    }

    protected override async Task HandleMessage(Message<Ignore, ProductCreatedEvent> message)
    {
        _logger.LogDebug("Teste");


        if (message.Value != null)
        {
            var stock = await _stockService.Create(message.Value);
        }            


        throw new NotImplementedException();
    }
}
