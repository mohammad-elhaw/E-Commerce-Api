using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce_Api.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationSegment(ActionContext context)
        {
            // context ==> ModelState => Dictionary<string, ModelStateEntry>
            // string => key, name of param
            // ModelStateEntry => object => errors


            var errors = context.ModelState
                .Where(e => e.Value.Errors.Any())
                .Select(e => new ValidationError
                {
                    Field = e.Key,
                    Errors = e.Value.Errors
                    .Select(x => x.ErrorMessage)
                });

            var errorResponse = new ValidationErrorResponse
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                ErrorMessage = "Validation Segment Field",
                Errors = errors
            };
            return new UnprocessableEntityObjectResult(errorResponse);
        }
    }
}
