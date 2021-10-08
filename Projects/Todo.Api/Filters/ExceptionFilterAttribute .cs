using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Todo.Common.Exceptions;

namespace Todo.Api.Filters
{
    /// <summary>
    /// Represents attribute that provides a filter to catch exceptions.
    /// </summary>

    public class CatchExceptionFilterAttribute: ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, HttpStatusCode> _exceptionStatus = new Dictionary<Type, HttpStatusCode>
        {
            {typeof(DbContextAccessException), HttpStatusCode.InternalServerError},
            {typeof(ObjectNotFoundException), HttpStatusCode.NotFound}
        };

        /// <inheritdoc />
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
                return Task.CompletedTask;

            if (_exceptionStatus.TryGetValue(context.Exception.GetType(), out var httpStatus))
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = (int) httpStatus
                };
            }
            else
            {
                context.Result = new ObjectResult("An unknown exception has occurred.")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            return Task.CompletedTask;
        }

    }
}