using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;
using Talabat.APIS.Errors;

namespace Talabat.APIS.Middlware
{
    public class ExeptionMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddelware> _logger;
        private readonly IHostEnvironment _evn;

        public ExeptionMiddelware(RequestDelegate Next , ILogger<ExeptionMiddelware> logger,IHostEnvironment evn )
        {
            _next = Next;
            _logger = logger;
            _evn = evn;

        }

        //InvokeAsync

        public async Task InvokAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex ,ex.Message);
                //Production => Log Exeption In Database

                context.Response.ContentType = "application/json";
                context.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                //if (_evn.IsDevelopment())
                //{
                //    var Response = new ApiExeptionResponse((int)HttpStatusCode.InternalServerError, ex.Message,ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var Response = new ApiExeptionResponse((int)HttpStatusCode.InternalServerError);
                //}
                
                
                var Response = _evn.IsDevelopment() ? new ApiExeptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()): new ApiExeptionResponse((int)HttpStatusCode.InternalServerError); 
                var Options = new JsonSerializerOptions() 
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                     
                };
                var JasonResponse = JsonSerializer.Serialize(Response, Options);
                await context.Response.WriteAsync(JasonResponse);
            }
        }

    }
}
