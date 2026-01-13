using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Repositories.Tenant;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.UseCases.Queries.VerifyInviteCode;

public record VerifyInviteCodeQuery(string Code) : IQuery;
public class VerifyInviteCodeHandler : IQueryHandler<VerifyInviteCodeQuery, VerifyInviteCodeResponseDto>
{
    private readonly IInviteCodeValidator _codeValidator;
    private readonly ITenantRepository _tenantRepository;
    public VerifyInviteCodeHandler(IInviteCodeValidator codeValidator, ITenantRepository tenantRepository)
    {
        _codeValidator = codeValidator;
        _tenantRepository = tenantRepository;
    }

    public async Task<VerifyInviteCodeResponseDto> HandleAsync(VerifyInviteCodeQuery query, CancellationToken ct = default)
    {
        var inviteCode = await _codeValidator.ValidateAndGetCode(query.Code, ct);
        var tenant = await _tenantRepository.GetTenantById(inviteCode.TenantId, ct);
        
        if (tenant is null) return new VerifyInviteCodeResponseDto { IsValid = false };
        
        return new VerifyInviteCodeResponseDto
        {
            IsValid = true,
            TenantName = tenant.Name,
            Code = inviteCode.Value
        };
    }
}
public record VerifyInviteCodeResponseDto
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = "-";
    public string TenantName { get; set; } = "-";
    public string Code { get; set; } = "-";
}