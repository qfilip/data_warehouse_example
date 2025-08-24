namespace DwHouse.Identity.Models;
public sealed record AppIdentity(
    Guid Id,
    string Name,
    string Email
);
