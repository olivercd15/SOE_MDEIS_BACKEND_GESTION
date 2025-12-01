namespace SOE_MDEIS_BACKEND_GESTION.DTOs
{
    public class ApiResponse<T>
    {
        public string? Message { get; set; }
        public string? TechnicalMessage { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }

        public ApiResponse(string? message, string? technicalMessage, T? data, int statusCode, bool success)
        {
            Message = message;
            TechnicalMessage = technicalMessage;
            Data = data;
            StatusCode = statusCode;
            Success = success;
        }

        public static ApiResponse<T> SuccessResponse(T data, string? message = null, int statusCode = 200)
        {
            string finalMessage = string.IsNullOrWhiteSpace(message) ? "Operación exitosa" : message;
            return new ApiResponse<T>(finalMessage, null, data, statusCode, true);
        }

        public static ApiResponse<T> ErrorResponse(string message, string? technicalMessage, int statusCode)
        {
            return new ApiResponse<T>(message, technicalMessage, default, statusCode, false);
        }
    }
}
