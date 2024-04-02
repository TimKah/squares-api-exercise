using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Squares.Database.Memory.Exceptions;
using ExceptionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute;

namespace Squares.API.Filters;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NoSuchUserException:
            case NullReferenceException:
                context.Result = new BadRequestObjectResult("No such user exists");
                break;
            case InvalidOperationException:
                context.Result = new BadRequestObjectResult("It is not possible to form the shape");
                break;
            default:
                context.Result = new BadRequestObjectResult("Something went wrong");
                break;
        }
    }
}