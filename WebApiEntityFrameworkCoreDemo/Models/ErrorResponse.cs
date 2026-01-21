namespace WebApiEntityFrameworkCoreDemo.Models
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ErrorResponse(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        public static ErrorResponse Ok(string message = null) => new(true, message);
        public static ErrorResponse Fail(string message) => new(false, message);
    }
}
