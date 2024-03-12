using Customer.API.Application.Features.Commands;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API.Application.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        private static readonly List<IValidator> Validators = new()
        {
            new CreateCustomerCommandValidator(),
            new DeleteCustomerCommandValidator(),
            new UpdateCustomerCommandValidator(),
        };

        public async Task<string> ValidateAsync(object obj)
        {
            var error = string.Empty;

            var validator = Validators.FirstOrDefault(v => v.CanValidateInstancesOfType(obj.GetType()));

            if (validator != null)
            {
                var context = new ValidationContext<object>(obj);

                var result = (ValidationResult)await validator.ValidateAsync(context);

                error = string.Concat(result.Errors.Select(e => e.ErrorMessage.Replace("'", "") + " ")).Trim();
            }

            return error;
        }
    }
}
