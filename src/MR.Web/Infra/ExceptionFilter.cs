using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MR.Web.Features.Core;

namespace MR.Web.Infra
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            var result = CreateForException(context.Exception);
            if (result != null)
            {
                context.Result = result;
            }
        }

        private static IActionResult CreateForException(Exception exception)
        {
            return exception switch
            {
                NotExistsException _ => new NotFoundResult(),
                ValidationException validationException => new BadRequestObjectResult(validationException.Errors),
                _ => null,
            };
        }
    }
}