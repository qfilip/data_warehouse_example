namespace DwHouse.Messaging.Abstractions;

public interface IMessageSender<TMessage>
{
    Task SendAsync(TMessage message);
}
