using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Api.Utils
{
    public class ApiErrorResponse
    {
        public string Message { get; set; }

        public List<ApiErrorPayLoad> Errors { get; set; }

        public ApiErrorResponse(ModelStateDictionary modelState)
        {
            Message = string.Empty;
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ApiErrorPayLoad(key, x.ErrorMessage)))
                .ToList();
        }

        public ApiErrorResponse(string message, IEnumerable<string> errors)
        {
            Message = message;
            Errors = errors.Select(x => new ApiErrorPayLoad(string.Empty, x)).ToList();
        }

        public ApiErrorResponse(IEnumerable<string> errors)
        {
            Message = "Invalid API operation.";
            Errors = errors.Select(x => new ApiErrorPayLoad(string.Empty, x)).ToList();
        }

        public ApiErrorResponse() { }
    }

    public class ApiErrorPayLoad
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ApiErrorPayLoad(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
