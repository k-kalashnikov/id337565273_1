using System;
using System.Collections.Generic;
using System.Linq;

namespace SP.Contract.Application.Common.Response
{
    public static class ResultHelper
    {
        public static ProcessingResult<T> Success<T>(T value) =>
            new ProcessingResult<T>(value);

        public static ProcessingResult<T> Warn<T>(T value, string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                throw new ArgumentNullException(nameof(error));
            }

            return new ProcessingResult<T>(value, new[] { error });
        }

        public static ProcessingResult<T> Warn<T>(T value, IEnumerable<string> errors)
        {
            if (errors is null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            return new ProcessingResult<T>(value, errors.ToArray());
        }

        public static ProcessingResult<T> Error<T>(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                throw new ArgumentNullException(nameof(error));
            }

            return new ProcessingResult<T>(default, new[] { error });
        }

        public static ProcessingResult<T> Error<T>(IEnumerable<string> errors)
        {
            if (errors is null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            return new ProcessingResult<T>(default, errors.ToArray());
        }
    }
}
