using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public abstract class ExtendedControllerBase : ControllerBase
    {
        protected IActionResult ProcessOperationResult<T>(OperationResult<T> opResult)
            where T : class
        {
            switch (opResult.ResponseStatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.Created:
                    if (opResult.ContextObject == null && !opResult.Errors.Any()) // no context to send back to the client.
                        return StatusCode(StatusCodes.Status201Created);
                    return StatusCode(StatusCodes.Status201Created, opResult);
                case HttpStatusCode.OK:
                    if (opResult.ContextObject == null && !opResult.Errors.Any()) // no context to send back to the client.
                        return Ok();
                    return Ok(opResult);
                case HttpStatusCode.BadRequest: // for any invalid business API ops, the server shall return bad request with error strings
                    return new OperationFailedResult(new ApiErrorResponse(opResult.Errors));
                case HttpStatusCode.Unauthorized: // for any unauthorised API ops, the server shall return bad request with unauthorised errors
                    return new OperationFailedResult(new ApiErrorResponse(new List<string>() { "User is not authorised to perform the operation." }));
                case HttpStatusCode.InternalServerError: // there are a few services which will return internal server error.
                    return new ErrorResult(new ApiErrorResponse(opResult.Errors));
                default:
                    return NotFound(); // default to not sending any data
            }
        }
    }
}
