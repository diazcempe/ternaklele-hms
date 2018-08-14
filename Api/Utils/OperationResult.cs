using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Utils
{
    /// <summary>
    /// Generally, any API that validates a user and / or change the state of the entities in any way should return this object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationResult<T>
        where T : class
    {
        public OperationResult() { } // for JSON serialisation.

        public OperationResult(HttpStatusCode statusCode, T contextObject = null, params string[] errors)
        {
            ResponseStatusCode = statusCode;
            ContextObject = contextObject;
            Errors = new List<string>(errors);
        }

        public OperationResult(string error)
            : this(HttpStatusCode.BadRequest, null, error) { }

        public OperationResult(HttpStatusCode statusCode, T contextObject)
            : this(statusCode, contextObject, new string[0]) { }

        /// <summary>
        /// The response code which should be returned by the Controller
        /// </summary
        public HttpStatusCode ResponseStatusCode { get; set; }

        /// <summary>
        /// Errors to be shown to the frontends, either on Noty or validation summary.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Returns the contextual model/data for the operation.
        /// </summary>
        public T ContextObject { get; set; }
    }
}
