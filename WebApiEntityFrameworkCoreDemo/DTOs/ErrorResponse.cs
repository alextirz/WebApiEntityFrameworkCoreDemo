namespace WebApiEntityFrameworkCoreDemo.DTOs
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid? Id { get; set; }

        public ErrorResponse(bool success, string message = null, Guid? id = null)
        {
            Success = success;
            Message = message;
            Id = id;
        }

        public static ErrorResponse Ok(string message = null) => new(true, message);
        public static ErrorResponse Ok(Guid id) => new(true, null, id);
        public static ErrorResponse Fail(string message) => new(false, message);
    }
}
