namespace Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public IEnumerable<string>? Errors { get; init; }

        public static ApiResponse<T> SuccessResponse(T data) =>
            new() { Success = true, Data = data };

        public static ApiResponse<T> FailResponse(IEnumerable<string> errors) =>
            new() { Success = false, Errors = errors };

        public static ApiResponse<T> FailResponse(string error) =>
            new() { Success = false, Errors = new[] { error } };
    }
}
