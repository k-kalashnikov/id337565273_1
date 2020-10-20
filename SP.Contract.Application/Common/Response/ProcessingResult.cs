using System;

namespace SP.Contract.Application.Common.Response
{
    public class ProcessingResult<T>
    {
        public ProcessingResult(T result, string[] errors = null)
        {
            Result = result;
            Errors = errors ?? Array.Empty<string>();
        }

        public T Result { get; }

        public string[] Errors { get; }
    }
}
