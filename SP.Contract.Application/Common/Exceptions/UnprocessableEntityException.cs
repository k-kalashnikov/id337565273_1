﻿using System;

namespace SP.Contract.Application.Common.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string name, object key)
            : base($"Entity {name} with key {key} is invalid")
        {
        }

        public UnprocessableEntityException(string message)
            : base(message)
        {
        }
    }
}
