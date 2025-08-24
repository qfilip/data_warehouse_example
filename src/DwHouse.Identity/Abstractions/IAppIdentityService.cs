using System.Security.Claims;

namespace DwHouse.Identity.Abstractions;

public interface IAppIdentityService<T>
{
    void Set(T identity);
    void Set(IEnumerable<Claim> claims);
    T Get();
}
