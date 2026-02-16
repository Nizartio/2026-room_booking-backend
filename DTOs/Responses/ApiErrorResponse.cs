using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.DTOs.Responses
{
    public class ApiErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, string[]> Errors { get; set; } = new();

        public static ApiErrorResponse FromMessage(string message)
        {
            return new ApiErrorResponse { Message = message };
        }

        public static ApiErrorResponse FromModelState(ModelStateDictionary modelState)
        {
            var errors = modelState
                .Where(kvp => kvp.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors
                        .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                            ? "Invalid value."
                            : e.ErrorMessage)
                        .ToArray() ?? new string[0]
                );

            return new ApiErrorResponse
            {
                Message = "Validation failed.",
                Errors = errors
            };
        }
    }
}
