namespace RestauranteBackend.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? ErrorType { get; set; }
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }

        public ErrorResponse(int statusCode, string message, string? errorType = null, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            ErrorType = errorType;
            Details = details;
            Timestamp = DateTime.UtcNow;
        }
    }

}
