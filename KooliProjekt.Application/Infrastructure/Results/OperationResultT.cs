namespace KooliProjekt.Application.Infrastructure.Results
{
    public class OperationResult<T>
    {
        public bool Succeeded { get; set; }
        public bool HasErrors => !Succeeded;

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
            return new OperationResult<T> { Succeeded = false, Message = message };
        }

        public static OperationResult<T> NotFound(string message = "Not found")
        {
            return new OperationResult<T> { Succeeded = false, Message = message };
        }
    }
}