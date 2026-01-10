using System.Net;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : TesourariaException
{
    private readonly IList<string> _errorMessages;

    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;


    public override IList<string> GetErrorMessages() => _errorMessages;
}
