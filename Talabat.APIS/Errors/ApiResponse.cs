
namespace Talabat.APIS.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? messaga = null)
        {
            StatusCode = statusCode;
            Message = messaga?? GetDefaultMessageForStatusCode(StatusCode);

        }

        private string? GetDefaultMessageForStatusCode(int? statusCode)
        {
            //500=> "Internal Server";
            //400 => "Bad Request";
            //401 => "Unauthorized";
            //404 => "Not Found";
            return statusCode switch
            {
                500 => "Internal Server",
                400 => "Bad Request",
                401 => "You Are Not Authorized",
                404 => "Resource Not Found",
                _ => null

            };
        }
    }
}
