using System.Net;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public class ConflictException : TesourariaException
{
    public ConflictException(string messages) : base(messages){}

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;

    public override IList<string> GetErrorMessages() => [Message];
}