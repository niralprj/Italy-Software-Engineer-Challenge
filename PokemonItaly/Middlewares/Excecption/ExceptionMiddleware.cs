using Microsoft.AspNetCore.Http;
using PokemonItaly.API.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PokemonItaly.API.Middlewares.Excecption
{

    /// <summary>
    /// Custom middleware class to handle exception globally
    /// </summary>
    public class ExceptionMiddleware
    {

        #region Declaration
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }
        #endregion

        #region Invoker
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        #endregion

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            return httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message =  ex.Message
            }.ToString());

        }
    }
}
