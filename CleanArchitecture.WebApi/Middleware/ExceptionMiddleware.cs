using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using FluentValidation;
using Microsoft.IdentityModel.Abstractions;

namespace CleanArchitecture.WebApi.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly AppDbContext _context;

    public ExceptionMiddleware(AppDbContext context)
    {
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context); // uygulamada hata yok ise bir sonrak imiddleware'e devam et.
        }
        catch (Exception ex)
        {
            await LogExceptionToDatabaseAsync(ex, context.Request); //loglama yapıyoruz
            await HandleExceptionAsync(context, ex); // user'ın anlayabileceği detayda exception hatası üretiyoruz
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json"; //API isteği yapa nyere cevap veriken JSON formatta cevap beklendiğindne, hata mesajlarını da JSON formatına çeviriyoruz öncelikle.
        context.Response.StatusCode = 500;

        if (ex.GetType() == typeof(ValidationException))
        {
            return context.Response.WriteAsync(new ValidationErrorDetails
            {
                Errors = ((ValidationException)ex).Errors.Select(s => s.PropertyName),
                StatusCode = 403 // validasyon hatasına 403 Forbidden dönüyoruz, yani buraya geçemezsin diyoruz.
            }.ToString());
        }

        return context.Response.WriteAsync(new ErrorResult
        {
            Message = ex.Message,
            StatusCode = context.Response.StatusCode
        }.ToString());
    }

    private async Task LogExceptionToDatabaseAsync(Exception ex, HttpRequest request)
    {
        ErrorLog errorLog = new()
        {
            ErrorMessage = ex.Message,
            StackTrace = ex.StackTrace,
            RequestPath = request.Path,
            RequestMethod = request.Method,
            Timestamp = DateTime.Now,
        };

        await _context.Set<ErrorLog>().AddAsync(errorLog, default); // default'u cancellation token yerine yazdık
        await _context.SaveChangesAsync(default);
    }
}
