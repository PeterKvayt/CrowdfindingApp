using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers.Errors;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Middlewares
{
    public class ExceptionInterceptor
    {
        private readonly RequestDelegate _next;

        public ExceptionInterceptor(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await WriteErrorResponseAsync(httpContext, ex);
            }
        }

        private async Task WriteErrorResponseAsync(HttpContext httpContext, Exception ex)
        {
            //var (statusCode, message) = GetErrorDetailsByExceptionType(ex);
            var errors = new List<ErrorInfo>
            {
                new ErrorInfo(ex.Message),
                new ErrorInfo(ex.StackTrace)
            };

            var reply = new ReplyMessageBase { Errors = errors };
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var result = new JsonResult(reply);

            await httpContext.ExecuteResultAsync(result);
        }

        //private Tuple<int, string> GetErrorDetailsByExceptionType(Exception exception)
        //{
        //    return new Tuple<int, string>(StatusCodes.Status500InternalServerError, _resourceProvider.GetErrorMessageByCode(StatusCodes.Status500InternalServerError));
        //}
    }
}
