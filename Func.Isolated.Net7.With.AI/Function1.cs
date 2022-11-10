using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Func.Isolated.Net7.With.AI
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            DateTime date = DateTime.UtcNow;

            _logger.LogInformation("Some logging tests right now:");
            _logger.LogTrace("Trace " + date.ToLongTimeString());
            _logger.LogDebug("Debug " + date.ToLongTimeString());
            _logger.LogInformation("Information " + date.ToLongTimeString());
            _logger.LogWarning("Warning " + date.ToLongTimeString());
            _logger.LogError("Error " + date.ToLongTimeString());
            _logger.LogCritical("Critical " + date.ToLongTimeString());

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
