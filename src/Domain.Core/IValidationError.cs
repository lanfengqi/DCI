using System.Collections.Generic;

namespace Domain.Core
{
    public interface IValidationError
    {
        IEnumerable<ValidationErrorItem> GetErrors();
        IValidationError AddError(string errorKey);
        IValidationError AddError(string errorKey, params object[] parameters);
        IValidationError AddError(string errorKey, IList<object> parameters);
        bool IsValid { get; }
    }

    public class ValidationErrorItem
    {
        public ValidationErrorItem() { Parameters = new List<object>(); }
        public string ErrorKey { get; set; }
        public List<object> Parameters { get; set; }
    }
}
