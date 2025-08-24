using MassTransit;

namespace DwHouse.Messaging;

public record MassTransitConsumersRegistryFunctions(
    Action<IBusRegistrationConfigurator> AddConsumers,
    Action<IReceiveEndpointConfigurator, IBusRegistrationContext> ConfigureConsumers,
    Action<IInMemoryBusFactoryConfigurator> ConfigureCommands,
    Action MapCommandEndpoints
);