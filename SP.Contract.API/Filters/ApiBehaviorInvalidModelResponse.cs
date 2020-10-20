using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Contract.Common.Extensions;
using SP.Market.Identity.Common.Responses;

namespace SP.Contract.API.Filters
{
    public static class ApiBehaviorInvalidModelResponse
    {
        public static IActionResult Response(ActionContext context)
        {
            var errors = context.ModelState
                .Where(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .ToArray();

            var count = errors.Count();
            var arrayInvalidParams = new string[count];
            for (int i = 0; i < count; i++)
            {
                arrayInvalidParams[i] = errors[i].Key;
            }

            var invalidParamsString = string.Join(",", arrayInvalidParams);

            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status400BadRequest,
                Type = "validation-error",
                Detail = $"Please refer to the errors property for additional details: {invalidParamsString}"
            };

            return new BadRequestObjectResult(HttpCustomResponse.BadRequest(problemDetails.ToJson()));
        }
    }
}
