using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divo.Errors
{
    public class APiException(int statusCode, string? message = null, string? details = null) : ApiErrorResponse(statusCode, message)
    {
        public string? Details { get; set; } = details;
    }
}