using System.Net;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public class ExpiredTokenException : TesourariaException
{
    public ExpiredTokenException() : base(ResourceMessagesException.VERIFICATION_TOKEN_EXPIRED)
    {
    }
    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;

}