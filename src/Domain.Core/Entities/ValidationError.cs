﻿using System.Collections.Generic;

namespace Domain.Core
{
    public class ValidationError : IValidationError
    {
        private List<ValidationErrorItem> errorItemList = new List<ValidationErrorItem>();

        #region IValidationError Members

        public bool IsValid
        {
            get
            {
                return errorItemList.Count == 0;
            }
        }

        public IEnumerable<ValidationErrorItem> GetErrors()
        {
            return errorItemList;
        }

        public IValidationError AddError(string errorKey)
        {
            errorItemList.Add(new ValidationErrorItem { ErrorKey = errorKey });
            return this;
        }
        public IValidationError AddError(string errorKey, params object[] parameters)
        {
            errorItemList.Add(new ValidationErrorItem { ErrorKey = errorKey, Parameters = new List<object>(parameters) });
            return this;
        }
        public IValidationError AddError(string errorKey, IList<object> parameters)
        {
            errorItemList.Add(new ValidationErrorItem { ErrorKey = errorKey, Parameters = new List<object>(parameters) });
            return this;
        }

        #endregion
    }
}
