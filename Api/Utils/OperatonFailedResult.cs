using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Utils
{

    /// <summary>
    /// Returns Bad Request and reasons.
    /// </summary>
    public class OperationFailedResult : ObjectResult
    {
        public OperationFailedResult(ApiErrorResponse error) : base(error)
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
