namespace Tesouraria.Domain.Dtos.Responses.Tenants;

public record ResponseTenantsJson(IEnumerable<ResponseTenantJson> Tenants);

public record ResponseTenantJson(Guid Id, string Name);