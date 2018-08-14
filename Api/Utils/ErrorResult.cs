using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Utils
{
    /// <summary>
    /// Returns Internal Server Error and reasons.
    /// </summary>
    public class ErrorResult : ObjectResult
    {
        public ErrorResult(ApiErrorResponse error) : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
