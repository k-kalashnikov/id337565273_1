using System;

namespace SP.Contract.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }

        public static UnauthorizedException Response(string message)
            => new UnauthorizedException(message);
    }
}
