namespace API.Middleware;
using API.Errors;
using System;
using System.Net;
using System.Text.Json;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context){
        try{
            await next(context);
        }
        catch(Exception ex){

            logger.LogError(ex, ex.Message);
            context.Response.ContentType="application/json";
            context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
            var response = env.IsDevelopment() ? new ApiExceptions(context.Response.StatusCode,ex.Message,ex.StackTrace)
            : new ApiExceptions(context.Response.StatusCode,ex.Message,"Internal Server Error");

#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances
            var json = JsonSerializer.Serialize(response,options);
            await context.Response.WriteAsync(json);
        }

    }
} 