using Application.Interface;
using Confluent.Kafka;
using Domain.Event;
using Infrastructure.Configuration.Kafka;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Outgoing.Messaging.Pacing;

public class ProductCreatedKafkaPublisher : KafkaBaseProducer<Ignore, ProductCreatedEvent>, IProductCreatedEventPublisher
{
    public ProductCreatedKafkaPublisher(
        IKafkaContext context,
        ILogger<ProductCreatedKafkaPublisher> logger) : base(context, logger, "infoeste.api-product.product.created")
    {
    }
}