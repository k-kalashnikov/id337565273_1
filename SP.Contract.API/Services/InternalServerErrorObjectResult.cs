using Microsoft.AspNetCore.Mvc;

namespace SP.Contract.API.Services
{
    [Microsoft.AspNetCore.Mvc.Infrastructure.DefaultStatusCode(500)]
    public class InternalServerErrorObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = 500;

        public InternalServerErrorObjectResult(object value)
          : base(value)
        {
            this.StatusCode = new int?(DefaultStatusCode);
        }
    }
}
