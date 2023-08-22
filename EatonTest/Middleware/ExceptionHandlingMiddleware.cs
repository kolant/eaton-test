using System.Net;
using System.Text;
using EatonTest.Services.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EatonTest.Middleware
{
    public class ExceptionHandlingMiddleware : IDisposable
    {
        private const string ResponseContentType = "application/json";

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly RequestDelegate _next;
        private readonly IServiceScope _serviceScope;

        public ExceptionHandlingMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _serviceScope = scopeFactory.CreateScope();
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentNullException argNullException) when (argNullException.Message.Contains(
                "Cannot pass null model to Validate"))
            {
                await WriteToResponseAsync(
                    context,
                    new BadRequestExceptionResponse("Incoming request has invalid parameters"));
            }
            catch (ApplicationException applicationException)
            {
                await WriteToResponseAsync(context, new BadRequestExceptionResponse(applicationException.Message));
            }
            catch (Exception)
            {
                var errorId = Guid.NewGuid().ToString();

                await WriteToResponseAsync(context, null, HttpStatusCode.InternalServerError);
            }
        }

        public void Dispose()
        {
            _serviceScope?.Dispose();
        }

        private static async Task WriteToResponseAsync(HttpContext context, object data, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = ResponseContentType;

            var result = JsonConvert.SerializeObject(data, SerializerSettings);
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(result);

            await context.Response.WriteAsync(result);
        }
    }
}