using Application.Errors;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Infrastructure.Filters;

public class HttpExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            context.Result = context.Exception switch
            {
                EntityNotFoundByIdException<int, Workspace> ex => new ObjectResult(ex) {StatusCode = 404},
                _ => context.Result
            };

            // context.ExceptionHandled = true;
        }
    }
}