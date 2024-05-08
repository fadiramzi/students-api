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

        public async Task Invoke(HttpContext context)
        {
            // Intercept the response
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next(context);

                    // Reset the stream position
                    responseBody.Seek(0, SeekOrigin.Begin);

                    // Read the response
                    var responseBodyString = await new StreamReader(responseBody).ReadToEndAsync();

                    // Deserialize original response
                    object originalResponse = null;
                    try
                    {
                        originalResponse = JsonConvert.DeserializeObject(responseBodyString);
                    }
                    catch (JsonException)
                    {
                        // Response is not a valid JSON, maybe a string or primitive type
                        originalResponse = responseBodyString;
                    }

                    // Format response
                    var formattedResponse = new ApiResponse<object>();

                    if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                    {
                        formattedResponse.IsSuccess = true;
                        // Check if the response content type is JSON
                        if (context.Response.ContentType?.ToLower().Contains("application/json") == true)
                        {
                            formattedResponse.Result = JsonConvert.DeserializeObject(responseBodyString);
                        }
                        else
                        {
                            // Include response string as is
                            formattedResponse.Result = responseBodyString;
                        }
                    }
                    else
                    {
                        formattedResponse.IsSuccess = false;
                        formattedResponse.Error = new ApiError
                        {
                            Code = context.Response.StatusCode,
                            Description = "An error occurred"
                        };
                    }

                    // Convert response to JSON
                    var jsonResponse = JsonConvert.SerializeObject(formattedResponse);

                    // Write the formatted response
                    context.Response.ContentType = "application/json";
                    context.Response.ContentLength = Encoding.UTF8.GetByteCount(jsonResponse);
                    await context.Response.WriteAsync(jsonResponse);

                    // Restore the original body stream
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    context.Response.StatusCode = 500;
                    var errorResponse = new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Error = new ApiError
                        {
                            Code = 500,
                            Description = "An internal server error occurred"
                        }
                    };
                    var jsonResponse = JsonConvert.SerializeObject(errorResponse);
                    context.Response.ContentType = "application/json";
                    context.Response.ContentLength = Encoding.UTF8.GetByteCount(jsonResponse);
                    await context.Response.WriteAsync(jsonResponse);
                }
            }
        }
    }
}
