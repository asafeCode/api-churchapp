using System.Net;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public class RefreshTokenExpiredException : TesourariaException
{
    public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION){}
    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
}