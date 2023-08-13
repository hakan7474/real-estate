using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Core;
using RealEstate.Core.Exceptions;
using System.Net;

namespace RealEstate.WebApi.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private const string ErrorKey = "Error";
    public void OnException(ExceptionContext context)
    {
        var error = new Response<dynamic>()
        {
            Status = ServiceResponseStatuses.Error
        };

        if (context.Exception is BusinessException businessException)
        {
            error.Error(businessException.ErrorKey, businessException.Message, (int)HttpStatusCode.InternalServerError);
        }
        else
        {
            error.Error(ErrorKey, context.Exception.Message, (int)HttpStatusCode.InternalServerError);
        }
        
        context.Result = new ObjectResult(error)
        {
            StatusCode = error.StatusCode
        };
        return;
    }
}