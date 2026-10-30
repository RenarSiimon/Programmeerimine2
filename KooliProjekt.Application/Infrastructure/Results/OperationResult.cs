using System.Collections.Generic;

namespace KooliProjekt.Application.Infrastructure.Results
{
    public class OperationResult
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; }

        public IDictionary<string, string> PropertyErrors { get; private set; }
        public IList<string> Errors { get; private set; }

        public bool HasErrors
        {
            get
            {
                return !Succeeded ||
                       PropertyErrors?.Count > 0 ||
                       Errors?.Count > 0;
            }
        }

        public bool ShouldSerializeHasErrors()
        {
            return HasErrors;
        }

        public OperationResult AddError(string error)
        {
            Succeeded = false;
            if (Errors == null)
            {
                Errors = new List<string>();
            }

            Errors.Add(error);

            return this;
        }

        public OperationResult AddPropertyError(string property, string error)
        {
            Succeeded = false;
            if (PropertyErrors == null)
            {
                PropertyErrors = new Dictionary<string, string>();
            }
            else if (PropertyErrors.ContainsKey(property))
            {
                return this;
            }

            PropertyErrors.Add(property, error);

            return this;
        }

        public static OperationResult Success()
        {
            return new OperationResult { Succeeded = true };
        }

        public static OperationResult Failure(string message)
        {
            var result = new OperationResult { Succeeded = false, Message = message };
            result.AddError(message);
            return result;
        }

        public static OperationResult NotFound(string message = "Not found")
        {
            var result = new OperationResult { Succeeded = false, Message = message };
            result.AddError(message);
            return result;
        }
    }
}