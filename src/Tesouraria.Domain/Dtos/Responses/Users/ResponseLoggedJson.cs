using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Responses.Users;

public record ResponseLoggedUserJson
{
    public string Name { get; set; } =  string.Empty;
    public UserRole Role { get; set; }
    public ResponseTokensJson Tokens { get; set; } =  default!;
}
public record ResponseLoggedOwnerJson
{
    public string Name { get; set; } =  string.Empty;
    public ResponseTokensJson Tokens { get; set; } =  default!;
}