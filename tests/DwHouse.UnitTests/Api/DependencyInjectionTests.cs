using DwHouse.Api.Static;

namespace DwHouse.UnitTests.Api;

public class DependencyInjectionTests
{
    [Fact]
    public void CanResolveServices()
    {
        var _ = Webapi.Build(Array.Empty<string>());
    }
}
