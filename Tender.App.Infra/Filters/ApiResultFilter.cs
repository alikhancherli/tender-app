using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tender.App.Domain.Shared;

namespace Tender.App.Infra.Filters;

public class ApiResultFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = ResultHandler<object>.Failure(context.Exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.HttpContext.Response.ContentType = "application/json";
        context.Result = new ObjectResult(result);
    }
}
