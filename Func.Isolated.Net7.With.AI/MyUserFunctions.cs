using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Func.Isolated.Net7.With.AI
{
    public class MyUserFunctions
    {
        private readonly ILogger _logger;
        private readonly IUserDataService _userDataService;

        public MyUserFunctions(ILoggerFactory loggerFactory, IUserDataService userDataService)
        {
            _logger = loggerFactory.CreateLogger<MyUserFunctions>();
            _userDataService = userDataService ?? throw new ArgumentNullException(nameof(userDataService));
        }

        [Function(nameof(GetUsers))]
        public async Task<HttpResponseData> GetUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
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

            var users = await _userDataService.GetUsersAsync();
            _logger.LogDebug("Number of users: " + users.Count);

            // Set again statuscode because of issue: https://github.com/Azure/azure-functions-dotnet-worker/issues/776#issuecomment-1015633552
            await response.WriteAsJsonAsync(users, response.StatusCode);

            return response;
        }
    }
}
