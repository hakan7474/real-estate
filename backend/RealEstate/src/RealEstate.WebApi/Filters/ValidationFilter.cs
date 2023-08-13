using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Core;
using System.Net;

namespace RealEstate.WebApi.Filters;

public class ValidationFilter : IResultFilter
{

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var errorsInModelState = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

        var errorResponse = new Response<dynamic>()
        {
            Status = ServiceResponseStatuses.Error
        };

        foreach (var error in errorsInModelState)
        {
            foreach (var subError in error.Value)
            {
                errorResponse.Error(error.Key, subError, (int)HttpStatusCode.BadRequest);
            }
        }

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = errorResponse.StatusCode
        };
        return;
    }
}