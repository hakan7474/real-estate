using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core;

namespace RealEstate.WebApi.Controllers;

public class BaseController : ControllerBase
{
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    private IMediator _mediator;

    public IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}

