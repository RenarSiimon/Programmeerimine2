namespace KooliProjekt.Application.Infrastructure.Results
{
    public class OperationResult<T> : OperationResult
    {
        private T _result;
        public T Result
        {
            get => _result;
            set => _result = value;
        }

        public T Value
        {
            get => _result;
            set => _result = value;
        }

        public string Message { get; set; }

        public static OperationResult<T> Success(T result)
        {
            return new OperationResult<T> { Succeeded = true, Result = result };
        }

        public static OperationResult<T> Failure(string message)
        {
            var result = new OperationResult<T> { Succeeded = false, Message = message };
            result.AddError(message);
            return result;
        }

        public static OperationResult<T> NotFound(string message = "Not found")
        {
            var result = new OperationResult<T> { Succeeded = false, Message = message };
            result.AddError(message);
            return result;
        }
    }
}