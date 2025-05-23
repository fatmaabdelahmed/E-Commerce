namespace Ecom.API.Helper
{
    public class ApiExceptions : ResponseApi
    {

        public ApiExceptions(int statusCode, string? statusMessage = null, string details=null) : base(statusCode, statusMessage)
        {
           Details = details;
        }

        public string Details { get; set; }
    }
}
