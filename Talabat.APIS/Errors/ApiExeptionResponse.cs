﻿namespace Talabat.APIS.Errors
{
    public class ApiExeptionResponse:ApiResponse
    {
        public string? Details { get; set; }
        public ApiExeptionResponse(int statusCode , string? message=null , string? details=null):base(statusCode, message)
        {
            Details = details ;
        }
    }
}
