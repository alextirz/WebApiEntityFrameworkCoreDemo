namespace WebApiEntityFrameworkCoreDemo.DTOs
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid? Id { get; set; }

        public ApiResponse(bool success, string message = null, Guid? id = null)
        {
            Success = success;
            Message = message;
            Id = id;
        }

        public static ApiResponse Ok(string message = null) => new(true, message);
        public static ApiResponse Ok(Guid id) => new(true, null, id);
        public static ApiResponse Fail(string message) => new(false, message);
    }
}
