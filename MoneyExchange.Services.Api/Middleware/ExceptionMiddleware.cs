namespace MoneyExchange.Service.Api.Middleware
{
    using System;
    //using Sentry;
    using System.Net;
    using Transversal.Common;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using Microsoft.Extensions.Primitives;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor of exception middleware
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// InvokeAsync 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //SentrySdk.ConfigureScope(scope => {
            //    scope.Level = SentryLevel.Fatal;
            //    scope.SetTag("aplication_name", Constant.DefaultAplicationName);
            //    scope.SetTag("user_name", username);

            //    scope.User.Other.Add(new KeyValuePair<string, string>("ApplicationName", Constant.DefaultAplicationName));
            //    scope.User.Username = username;
            //    scope.User.IpAddress = ipRequest;
            //});

            //var identifier = SentrySdk.CaptureException(exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            var response = new Response<string>
            {
                IsSuccess = false,
                IsWarning = true,
                Message = string.Format(Message.ErrorInesperado, new Guid().ToString())
            };

            return context.Response.WriteAsync(response.Serialize());
        }
    }
}
