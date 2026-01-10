using System.Net;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public class RefreshTokenNotFoundException : TesourariaException
{
    public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION){}
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    public override IList<string> GetErrorMessages() => [Message];
}