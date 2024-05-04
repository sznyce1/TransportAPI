using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using TransportAPI.Exceptions;

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
            catch(NotFoundException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync($"{e.Message}");
            }
            catch (InvalidDrivingLicenceException e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Invalid driving licence");
            }
            catch (SqlException e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Database error");
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Something went wrong {e.Message}");
            }
            
        }
    }
}
