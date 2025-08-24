using Microsoft.Extensions.Hosting;
using DwHouse.Messaging.Abstractions;

namespace DwHouse.Messaging.Channels;


public abstract class ChannelConsumerHostingServiceBase<TMessage, TRepository> : BackgroundService
{
    protected readonly IMessageReader<TMessage> _consumer;
    protected readonly IServiceProvider _serviceProvider;

    public ChannelConsumerHostingServiceBase(
        IMessageReader<TMessage> consumer,
        IServiceProvider serviceProvider)
    {
        _consumer = consumer;
        _serviceProvider = serviceProvider;
    }

    protected abstract Task HandleEvent(TMessage message, TRepository repository);
}