using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using StudentsManagerMW.Models.APIResponse;
using System.Text;

namespace StudentsManagerMW.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Capture the original response body stream
            var originalBodyStream = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {
                    // Replace the original response body with the MemoryStream
                    context.Response.Body = responseBody;

                    // Call the next middleware in the pipeline
                    await _next(context);

                    // Rewind the MemoryStream to read its content
                    responseBody.Seek(0, SeekOrigin.Begin);

                    if (context.Response.StatusCode == StatusCodes.Status200OK)
                    {
                        // Success response
                        var responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();

                        var response = new ApiResponse<object>
                        {
                            IsSuccess = true,
                            Result = JsonConvert.DeserializeObject(responseBodyContent),
                            Error = null
                        };

                        // Serialize and write the response
                        var jsonResponse = JsonConvert.SerializeObject(response);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(jsonResponse);
                    }
                    else
                    {
                        // Error response
                        var response = new ApiResponse<object>
                        {
                            IsSuccess = false,
                            Result = null,
                            Error = new ApiError
                            {
                                Code = context.Response.StatusCode,
                                Description = context.Features.Get<IStatusCodeReExecuteFeature>()?.OriginalPath ?? context.Response.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error.ToString()
                            }
                        };

                        // Serialize and write the response
                        var jsonResponse = JsonConvert.SerializeObject(response);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(jsonResponse);
                    }

                    // Copy the captured response body back to the original response stream
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            finally
            {
                // Restore the original response body stream
                context.Response.Body = originalBodyStream;
            }
        }
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }

        private async Task WriteResponse(HttpResponse response, ApiResponse<object> responseObject)
        {
            var jsonResponse = JsonConvert.SerializeObject(responseObject);
            var buffer = Encoding.UTF8.GetBytes(jsonResponse);
            await response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
