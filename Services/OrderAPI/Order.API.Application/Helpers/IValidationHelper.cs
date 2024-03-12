using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Application.Helpers
{
    public interface IValidationHelper
    {
        Task<string> ValidateAsync(dynamic dto);
    }
}
