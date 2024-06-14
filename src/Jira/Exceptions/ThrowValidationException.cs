using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Jira.Exceptions
{
    public class ThrowValidationException : ApplicationException
    {
        public NameValueCollection Errors { get; private set; }

        public ThrowValidationException(string message) : base(message) { Errors = new NameValueCollection(); }
        public ThrowValidationException(string message, NameValueCollection errors) : base(message) { Errors = errors; }
        public ThrowValidationException(string message, List<ValidationFailure> validationErrors) : base(message)
        {
            var errors = new NameValueCollection();
            foreach (var validationError in validationErrors)
            {
                errors.Add(validationError.ErrorMessage, validationError.PropertyName);
            }
            Errors = errors;
        }

        public ThrowValidationException(string message, Exception innerException) : base(message, innerException) { Errors = new NameValueCollection(); }
    }
}