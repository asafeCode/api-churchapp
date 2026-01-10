using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Infrastructure.DataAccess;

namespace Tesouraria.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger) => _logger = logger;

    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case TesourariaException tesourariaException:
                HandleProjectException(tesourariaException, context);
                break;
            
            case FluentValidation.ValidationException validationException:
                HandleValidationException(validationException, context);
                break;
            
            default:
                ThrowUnknowException(context);
                break;
        }
    }

    private static void HandleProjectException(TesourariaException tesourariaException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)tesourariaException.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(tesourariaException.GetErrorMessages()));
    }
    private static void HandleValidationException(FluentValidation.ValidationException validationException, ExceptionContext context)
    { 
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        
        var errors = validationException.Errors.Any()
            ? validationException.Errors.Select(e => e.ErrorMessage).ToList()
            : [validationException.Message];
        
        context.Result = new ObjectResult(new ResponseErrorJson(errors));
    }
    private void ThrowUnknowException(ExceptionContext context)
    { 
        if (context.Exception is InvalidOperationException ioe &&
            ioe.Message.Contains("Sequence contains more than one element"))
        {
            _logger.LogError(ioe, "Erro de unicidade detectado ao usar SingleOrDefault");

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(
                "Erro interno: dados inconsistentes detectados. Entre em contato com o suporte."
            ));
            return;
        }

        _logger.LogError(context.Exception, "Erro desconhecido capturado no filtro de exceção");
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }



}
