using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SP.Contract.API.Filters
{
    public class FluentValidationExceptionResponse
    {
        public static IActionResult Response(ActionContext context)
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState);

            var exceptionBody = new FluentValidationExceptionResponseBody()
            {
                Status = StatusCodes.Status400BadRequest
            };

            foreach (var (key, value) in problemDetails.Errors)
            {
                exceptionBody.Errors.Add(Utility.ToCamelCase(key), value);
            }

            var result = new BadRequestObjectResult(exceptionBody);
            return result;
        }
    }

    public class FluentValidationExceptionResponseBody
    {
        public FluentValidationExceptionResponseBody()
        {
            Errors = new Dictionary<string, string[]>();
        }

        [JsonProperty("errors")]
        public Dictionary<string, string[]> Errors { get; set; }

        [JsonProperty("status")]
        public int? Status { get; set; }
    }

    public class Utility
    {
        public static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            var chars = s.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                var hasNext = i + 1 < chars.Length;
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }
    }
}



