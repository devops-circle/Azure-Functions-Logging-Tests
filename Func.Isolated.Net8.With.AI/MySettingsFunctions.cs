using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Func.Isolated.Net8.With.AI
{
    public class MySettingsFunctions
    {
        private readonly ILogger<MySettingsFunctions> _logger;
        private readonly IConfiguration _configuration;
        private IConfigurationRoot _configRoot;

        public MySettingsFunctions(ILogger<MySettingsFunctions> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            // Source: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0#application-configuration-providers
            _configRoot = (IConfigurationRoot)configuration;
        }

        [Function(nameof(GetSettings))]
        public async Task<IActionResult> GetSettings([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            DateTime date = DateTime.UtcNow;

            _logger.LogInformation("Some logging tests right now:");

            // Log setting providers
            string allProviders = "Configuration providers: \n";
            foreach (var provider in _configRoot.Providers.ToList())
            {
                allProviders += provider.ToString() + "\n";
            }
            _logger.LogInformation(allProviders);

            _logger.LogInformation("Configuration keys and values:");
            _logger.LogInformation("logging.logLevel.Function: " + _configuration["logging.logLevel.Function"]);
            _logger.LogInformation("Key1: " + _configuration["Key1"]);
            _logger.LogInformation("Key1 with GetValue method: " + _configuration.GetValue<string>("Key1"));
            _logger.LogInformation("KeysNested:Key2: " + _configuration["KeysNested:Key2"]);
            _logger.LogInformation("KeysNested:Key3: " + _configuration["KeysNested:Key3"]);

            //_logger.LogTrace("Custom message as Trace " + date.ToLongTimeString());
            //_logger.LogDebug("Custom message as Debug " + date.ToLongTimeString());
            //_logger.LogInformation("Custom message as Information " + date.ToLongTimeString());
            //_logger.LogWarning("Custom message as Warning " + date.ToLongTimeString());
            //_logger.LogError("Custom message as Error " + date.ToLongTimeString());
            //_logger.LogCritical("Custom message as Critical " + date.ToLongTimeString());

            return new OkObjectResult($"Welcome to Azure Functions with the name: {nameof(GetSettings)}!");
        }
    }
}
