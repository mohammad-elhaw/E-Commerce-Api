using Shared.ErrorModels;

namespace E_Commerce_Api.Middlewares
{
    public static class CustomStatusCodePagesExtensions
    {
        public static void ConfigureStatusCodePages(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                response.ContentType = "application/json";

                var result = new ErrorDetails
                {
                    StatusCode = response.StatusCode,
                    Message = response.StatusCode switch
                    {
                        StatusCodes.Status400BadRequest => "Bad Request, Please Check your request",
                        StatusCodes.Status401Unauthorized => "Unauthorized Access",
                        StatusCodes.Status403Forbidden => "Access is Forbidden",
                        StatusCodes.Status404NotFound => "The Requested endpoint is not exist",
                        StatusCodes.Status500InternalServerError => "Internal Server Error",
                        _ => "Unexpected Error"
                    }
                }.ToString();

                await response.WriteAsync(result);
            });
        }
    }
}
