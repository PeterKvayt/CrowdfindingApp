using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.DataTransfers.Errors;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CrowdfundingApp.Api.Middlewares
{
    public class ExceptionInterceptor
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public ExceptionInterceptor(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _environment = env;
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
            var errors = new List<ErrorInfo>();
            if(_environment.IsDevelopment())
            {
                errors.Add(new ErrorInfo(ex.Message));
                errors.Add(new ErrorInfo(ex.StackTrace));
            }
            else
            {
                errors.Add(new ErrorInfo("Oops, something went wrong!"));
            }

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
