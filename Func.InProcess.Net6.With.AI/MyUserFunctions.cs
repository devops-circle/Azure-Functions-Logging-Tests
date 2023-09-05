using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Func.InProcess.Net6.With.AI
{
    public class MyUserFunctions
    {
        private readonly ILogger<MyUserFunctions> _logger;
        private readonly IUserDataService _userDataService;

        // Make sure the ILogger<T> is opt-in:
        // https://learn.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection#iloggert-and-iloggerfactory
        public MyUserFunctions(ILogger<MyUserFunctions> logger, IUserDataService userDataService)
        {
            _logger = logger;
            _userDataService = userDataService;
        }

        [FunctionName(nameof(GetUsers))]
        public async Task<IActionResult> GetUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            DateTime date = DateTime.UtcNow;

            _logger.LogInformation("Some logging tests right now:");
            _logger.LogTrace("Custom message as Trace " + date.ToLongTimeString());
            _logger.LogDebug("Custom message as Debug " + date.ToLongTimeString());
            _logger.LogInformation("Custom message as Information " + date.ToLongTimeString());
            _logger.LogWarning("Custom message as Warning " + date.ToLongTimeString());
            _logger.LogError("Custom message as Error " + date.ToLongTimeString());
            _logger.LogCritical("Custom message as Critical " + date.ToLongTimeString());

            var users = await _userDataService.GetUsersAsync();
            _logger.LogDebug("Number of users: " + users.Count);

            return new OkObjectResult(users);
        }
    }
}
