namespace Ecom.API.Helper
{
    public class ResponseApi
    {
        public ResponseApi(int statusCode, string? statusMessage=null)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage?? GetMessageFromStatusCode(StatusCode);
        }
        private string GetMessageFromStatusCode(int statuscode)
        {
            return statuscode switch
            {
                200=>"Done",
                400=>"Bad Request",
                401=>"Un Autherized",
                500=>"Server Error",
                _=>null,
            };
        }
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }
    }
}
