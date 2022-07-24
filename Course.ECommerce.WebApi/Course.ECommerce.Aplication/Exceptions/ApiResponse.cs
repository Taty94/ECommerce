using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.Exceptions
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageByStatusCode(statusCode);
        }       

        private string? GetMessageByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You have made, a bad request",
                401 => "You are not authorized",
                404 => "Resource need it, was not found",
                500 => "Your request have result in a server error",
                _ => null
            };
        }
    }
}
