using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Func.Isolated.Net8.With.AI
{
    public class MyUserFunctions
    {
        private readonly ILogger<MyFunctions> _logger;
        private readonly IUserDataService _userDataService;

        public MyUserFunctions(ILogger<MyFunctions> logger, IUserDataService userDataService)
        {
            _logger = logger;
            _userDataService = userDataService;
        }

        [Function(nameof(GetUsers))]
        public async Task<IActionResult> GetUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
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

            return new OkObjectResult($"Welcome to Azure Functions with the name: {nameof(GetUsers)}!");
        }
    }
}
