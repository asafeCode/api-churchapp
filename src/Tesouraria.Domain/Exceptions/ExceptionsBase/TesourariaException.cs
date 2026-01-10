using System.Net;

namespace Tesouraria.Domain.Exceptions.ExceptionsBase;

public abstract class TesourariaException : SystemException
{
    protected TesourariaException(string messages) : base(messages) {}

    public abstract HttpStatusCode GetStatusCode();
    public abstract IList<string> GetErrorMessages();

}
