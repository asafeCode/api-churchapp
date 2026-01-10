using System.Net;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public class NotFoundException : TesourariaException
{
    public NotFoundException(string message) : base(message){}
    
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;

    public override IList<string> GetErrorMessages() => [Message];
}