using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Func.Isolated.Net7.With.AI
{
    public class MyFunctions
    {
        private readonly ILogger _logger;

        public MyFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MyFunctions>();
        }

        [Function(nameof(Function1))]
        public HttpResponseData Function1([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            DateTime date = DateTime.UtcNow;

            _logger.LogInformation("Some logging tests right now:");
            _logger.LogTrace("Custom message as Trace " + date.ToLongTimeString());
            _logger.LogDebug("Custom message as Debug " + date.ToLongTimeString());
            _logger.LogInformation("Custom message as Information " + date.ToLongTimeString());
            _logger.LogWarning("Custom message as Warning " + date.ToLongTimeString());
            _logger.LogError("Custom message as Error " + date.ToLongTimeString());
            _logger.LogCritical("Custom message as Critical " + date.ToLongTimeString());

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }

        [Function(nameof(Function2))]
        public HttpResponseData Function2([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            DateTime date = DateTime.UtcNow;

            _logger.LogInformation("Some logging tests right now:");
            _logger.LogTrace("Custom message as Trace " + date.ToLongTimeString());
            _logger.LogDebug("Custom message as Debug " + date.ToLongTimeString());
            _logger.LogInformation("Custom message as Information " + date.ToLongTimeString());
            _logger.LogWarning("Custom message as Warning " + date.ToLongTimeString());
            _logger.LogError("Custom message as Error " + date.ToLongTimeString());
            _logger.LogCritical("Custom message as Critical " + date.ToLongTimeString());

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
