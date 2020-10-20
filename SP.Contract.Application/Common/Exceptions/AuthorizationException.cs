using System;

namespace SP.Contract.Application.Common.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(CrudOperationException operation)
             : base($"Access is denied of operation to {operation}")
        {
        }
    }

    public enum CrudOperationException
    {
        Create,
        Read,
        Update,
        Delete
    }
}
