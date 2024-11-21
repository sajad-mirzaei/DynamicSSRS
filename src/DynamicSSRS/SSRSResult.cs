
namespace DynamicSSRS
{
    public class SSRSResult
    {
        public ResultEnum Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int? ErrorCode { get; set; } // unKnown errors
    }

    public enum ResultEnum
    {
        Success = 1, // successful operation
        Error = 2, // general error
        NotFound = 3, // Item not found
        ValidationError = 4, // Validation error
        Unauthorized = 5, // No access or authentication failed
        Conflict = 6, // conflict in the data (such as the existence of a similar item)
        Timeout = 7, // End of waiting time for operation
        ServerError = 8 // Internal server error
    }

}