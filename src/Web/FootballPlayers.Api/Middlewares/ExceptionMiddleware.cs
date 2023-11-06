using System.Text.Json;
using FootballPlayers.Common.Exeptions;
using FootballPlayers.Common.Extensions;
using FootballPlayers.Common.Responses;

namespace Api.Middlewares;

/// <summary>
/// 
/// </summary>
public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse response = null;
        try
        {
            await _next.Invoke(context);
        }
        catch (ProcessException pe)
        {
            response = pe.ToErrorResponse();
        }
        catch (Exception pe)
        {
            response = pe.ToErrorResponse();
        }
        finally
        {
            if (response is not null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
            }
        }
    }
}