using System.Net;
using Tesouraria.Domain.Extensions;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public class InvalidLoginException : TesourariaException
{
    public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID){}
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    public override IList<string> GetErrorMessages() => [Message];
}