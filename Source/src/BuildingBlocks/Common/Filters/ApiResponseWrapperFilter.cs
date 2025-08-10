using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Filters
{
    public class ApiResponseWrapperFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null) return;

            if (context.Result is ObjectResult objectResult)
            {
                var value = objectResult.Value;
                if (value != null && value.GetType().IsGenericType &&
                    value.GetType().GetGenericTypeDefinition() == typeof(ApiResponse<>))
                    return; // Already wrapped

                var valueType = value?.GetType() ?? typeof(object);

                var wrappedResponse = typeof(ApiResponse<>).MakeGenericType(valueType)
                                        .GetMethod("SuccessResponse", new[] { valueType })
                                        .Invoke(null, new[] { value });

                context.Result = new ObjectResult(wrappedResponse)
                {
                    StatusCode = objectResult.StatusCode ?? 200
                };
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(ApiResponse<object>.SuccessResponse(null)) { StatusCode = 200 };
            }
            else if (context.Result is ContentResult contentResult)
            {
                context.Result = new ObjectResult(ApiResponse<string>.SuccessResponse(contentResult.Content ?? string.Empty))
                {
                    StatusCode = contentResult.StatusCode ?? 200
                };
            }
            else if (context.Result is StatusCodeResult statusCodeResult)
            {
                context.Result = new ObjectResult(ApiResponse<object>.SuccessResponse(null))
                {
                    StatusCode = statusCodeResult.StatusCode
                };
            }
        }
    }
}
