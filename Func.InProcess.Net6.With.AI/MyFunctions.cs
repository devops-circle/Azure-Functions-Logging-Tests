using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Func.InProcess.Net6.With.AI
{
    public class MyFunctions
    {
        private readonly ILogger<MyFunctions> _logger;

        // Make sure the ILogger<T> is opt-in:
        // https://learn.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection#iloggert-and-iloggerfactory
        public MyFunctions(ILogger<MyFunctions> logger)
        {
            _logger = logger;
        }

        [FunctionName(nameof(Function1))]
        public async Task<IActionResult> Function1(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            DateTime date = DateTime.UtcNow;

            _logger.LogInformation("Some logging tests right now:");
            _logger.LogTrace("Custom message as Trace " + date.ToLongTimeString());
            _logger.LogDebug("Custom message as Debug " + date.ToLongTimeString());
            _logger.LogInformation("Custom message as Information " + date.ToLongTimeString());
            _logger.LogWarning("Custom message as Warning " + date.ToLongTimeString());
            _logger.LogError("Custom message as Error " + date.ToLongTimeString());
            _logger.LogCritical("Custom message as Critical " + date.ToLongTimeString());

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
