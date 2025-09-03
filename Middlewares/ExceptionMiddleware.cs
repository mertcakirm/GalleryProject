using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace GalleryProject.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, dbEx.Message);

            string message = dbEx.Message;

            if (dbEx.InnerException != null)
            {
                var innerMessage = dbEx.InnerException.Message;
                if (innerMessage.Contains("Duplicate") || innerMessage.Contains("unique") || innerMessage.Contains("UNIQUE"))
                {
                    message = "Bu değer zaten mevcut.";
                }
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                success = false,
                error = message
            }));
        }
        catch (KeyNotFoundException knfEx)
        {
            _logger.LogWarning(knfEx, "Entity not found");

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                success = false,
                error = "Aranan kayıt bulunamadı",
                details = knfEx.Message
            }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                success = false,
                error = "Beklenmeyen bir hata oluştu",
                details = ex.Message
            }));
        }
    }
}