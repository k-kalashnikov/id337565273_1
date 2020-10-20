using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SP.Contract.API.Services;
using SP.Contract.Application.Common.Exceptions;
using SP.Market.Identity.Common.Responses;

namespace SP.Contract.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private IHostingEnvironmentService _serviceEnvironment;

        public override void OnException(ExceptionContext context)
        {
            _serviceEnvironment = GetService(context, typeof(IHostingEnvironmentService));

            if (context.Exception is ValidationException exception)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = ResultBadRequestFluentValidation(exception.Failures);
                return;
            }

            Log.Error(context.Exception, "An unhandled exception has occurred");

            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            if (context.Exception is UnauthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
            }

            if (context.Exception is UnprocessableEntityException)
            {
                code = HttpStatusCode.UnprocessableEntity;
            }

            if (context.Exception is ConflictException)
            {
                code = HttpStatusCode.Conflict;
            }

            if (context.Exception is AuthorizationException)
            {
                code = HttpStatusCode.Forbidden;
            }

            if (context.Exception is InvalidOperationException)
            {
                code = HttpStatusCode.InternalServerError;
            }

            if (context.Exception is BadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = ResultException(context.Exception.Message, context.Exception.StackTrace, code);
        }

        private IHostingEnvironmentService GetService(ExceptionContext context, Type service)
        {
            try
            {
                return (IHostingEnvironmentService)context.HttpContext.RequestServices.GetService(service);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.ToString());
            }
        }

        private IActionResult ResultBadRequestFluentValidation(IDictionary<string, string[]> exceptions)
        {
            var ex = new FluentValidationExceptionResponseBody()
                { Status = StatusCodes.Status400BadRequest };

            foreach (var (key, value) in exceptions)
            {
                ex.Errors.Add(Utility.ToCamelCase(key), value);
            }

            return new JsonResult(ex);
        }

        private IActionResult ResultBadRequest(IDictionary<string, string[]> exceptions)
        {
            var responseErrors = new List<string>();

            foreach (var exp in exceptions)
            {
                foreach (var value in exp.Value)
                {
                    responseErrors.Add(value);
                }
            }

            return new JsonResult(HttpCustomResponse.BadRequest(string.Join(",", responseErrors)));
        }

        private IActionResult ResultException(string message, string stackTrace, HttpStatusCode code)
        {
            var responseErrors = new List<string>();
            Log.Information($"{nameof(CustomExceptionFilterAttribute)} ResultException message: {message}");
            responseErrors.Add(message);
            var isEnvironmentProduction = _serviceEnvironment.GetEnvironment();
            Log.Information($"{nameof(CustomExceptionFilterAttribute)} isEnvironmentProduction: {isEnvironmentProduction}");
            if (isEnvironmentProduction != true)
            {
                Log.Information($"{nameof(CustomExceptionFilterAttribute)} write stackTrace: {stackTrace}");
                responseErrors.Add(stackTrace);
            }

            return code switch
            {
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(
                    HttpCustomResponse.Unauthorized()),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(
                    HttpCustomResponse.BadRequest(string.Join(";", responseErrors))),
                HttpStatusCode.Conflict => new ConflictObjectResult(
                    HttpCustomResponse.Conflict(string.Join(";", responseErrors))),
                HttpStatusCode.NotFound => new NotFoundObjectResult(
                    HttpCustomResponse.NotFound(string.Join(";", responseErrors))),
                HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(
                    HttpCustomResponse.UnprocessableEntity(string.Join(";", responseErrors))),
                HttpStatusCode.InternalServerError => new InternalServerErrorObjectResult(
                    HttpCustomResponse.InternalServerError(string.Join(";", responseErrors))),
                _ => new InternalServerErrorObjectResult(HttpCustomResponse.UnknownServerError(
                    string.Join(";", responseErrors))),
            };
        }
    }
}
