using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleCustomExceptionResponseAsync(context, ex);
        }
    }

    private async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception ex)
    {
       var problemDetails = new ProblemDetails
       {
           Status = (int)HttpStatusCode.InternalServerError,
           Title = "Internal Server Error",
           Type = "https://httpstatuses.com/500", 
       };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}