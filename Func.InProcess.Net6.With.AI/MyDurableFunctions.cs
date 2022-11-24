using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;

namespace Func.InProcess.Net6.With.AI
{
    public class MyDurableFunctions
    {
        private readonly ILogger<MyDurableFunctions> _logger;

        // Make sure the ILogger<T> is opt-in:
        // https://learn.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection#iloggert-and-iloggerfactory
        public MyDurableFunctions(ILogger<MyDurableFunctions> logger)
        {
            _logger = logger;
        }

        [FunctionName(nameof(Orchestrator1))]
        public async Task<List<string>> Orchestrator1(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            // Create replay safe logger to avoid duplicate messages
            // https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-diagnostics?tabs=csharp#app-logging
            var logger = context.CreateReplaySafeLogger(_logger);

            DateTime date = DateTime.UtcNow;
            logger.LogInformation("Some logging tests right now:");
            logger.LogTrace("Custom message as Trace " + date.ToLongTimeString());
            logger.LogDebug("Custom message as Debug " + date.ToLongTimeString());
            logger.LogInformation("Custom message as Information " + date.ToLongTimeString());
            logger.LogWarning("Custom message as Warning " + date.ToLongTimeString());
            logger.LogError("Custom message as Error " + date.ToLongTimeString());
            logger.LogCritical("Custom message as Critical " + date.ToLongTimeString());

            var outputs = new List<string>();
            string output1 = "Output 1";
            outputs.Add(output1 + "\n");

            return outputs;
        }

        [FunctionName(nameof(Orchestrator1HttpTriggerGet))]
        public async Task<HttpResponseMessage> Orchestrator1HttpTriggerGet(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestMessage req,
            [DurableClient] IDurableClient starter)
        {
            // Function input comes from the request content.
            //object eventData = await req.Content.ReadAsAsync<object>(); // Not possible with GET
            string instanceId = await starter.StartNewAsync(nameof(Orchestrator1));//, eventData);

            _logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

        [FunctionName(nameof(Orchestrator1HttpTriggerPost))]
        public async Task<HttpResponseMessage> Orchestrator1HttpTriggerPost(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestMessage req,
            [DurableClient] IDurableClient starter)
        {
            // Function input comes from the request content.
            object eventData = await req.Content.ReadAsAsync<object>();
            string instanceId = await starter.StartNewAsync(nameof(Orchestrator1), eventData);

            _logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
