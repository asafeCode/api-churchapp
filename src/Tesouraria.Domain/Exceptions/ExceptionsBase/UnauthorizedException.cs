using System.Net;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public class UnauthorizedException : TesourariaException
{
    public UnauthorizedException(string messages) : base(messages){}
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    public override IList<string> GetErrorMessages() => [Message];
}