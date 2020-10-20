using System;

namespace SP.Contract.Application.Common.Exceptions
{
    public static class IntegrationWebExceptionByCode
    {
        public static Exception Create(int statusCode, string api, string message)
        {
            var verb = $"На внешнем источнике: {api} ошибка: {message}";
            switch (statusCode)
            {
                case 401:
                    return new UnauthorizedException(verb);
                case 403:
                    return new UnauthorizedException(verb);
                case 409:
                    return new ConflictException(verb);
                case 500:
                    return new Exception(verb);
                case 404:
                    return new NotFoundException(verb);
                case 400:
                    return new BadRequestException(verb);
            }

            return new Exception();
        }
    }
}
