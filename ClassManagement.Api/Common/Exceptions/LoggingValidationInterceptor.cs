using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement.Api.Common.Exceptions
{
    class LoggingValidationInterceptor(ILogger<LoggingValidationInterceptor> logger) : IValidatorInterceptor
    {
        private ILogger<LoggingValidationInterceptor> _logger = logger;    

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            _logger.LogInformation("Starting validation for model: {ModelName}", commonContext.InstanceToValidate.GetType().Name);

            return commonContext;
        }

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogWarning("Validation failed for {PropertyName}: {ErrorMessage}", error.PropertyName, error.ErrorMessage);
                }
            }
            return result;
        }
    }
}
