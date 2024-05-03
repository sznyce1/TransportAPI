using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace TransportAPI.Middleware
{
    public class ErrorHandllingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                var message = $"";
                if (e.InnerException is SqlException)
                {
                    message = $"Database error.";
                } 
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Something went wrong. {message}");
            }
        }
    }
}
