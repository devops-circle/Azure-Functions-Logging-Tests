using Func.Isolated.Net8.With.AI;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Setup DI
        services.AddTransient<IUserDataService, UserDataService>();
    })
    .ConfigureLogging(logging =>
    {
        /*
         * By default, logs with LogLevel.Warning or higher are sent to Application Insights.
         * To change this, remove the default rule so other log levels are sent to Application Insights.
         * See for more information: https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide?tabs=hostbuilder%2Cwindows#managing-log-levels
         * The default log level for Azure Functions is Information. So by removing the default rule, Information and above will be sent to Application Insights.
         * 
         * For configuring the loglevel per function. See the following documentation: https://learn.microsoft.com/en-us/azure/azure-functions/configure-monitoring?tabs=v2#configure-categories
         * In example: 
         * "logLevel": {
                "Host.Aggregator": "Trace", // Default
                "Host.Results": "Information", // Default
                "Function": "Information", // Default. Entries related to running a function are assigned a category of Function.<FUNCTION_NAME>.
                "Function.Function1.User": "Warning", 
                "Function.Function2.User": "Error",
                "Function.GetUsers": "Information", // Entries related to running a function are assigned a category of Function.<FUNCTION_NAME>.
                "Function.GetUsers.User": "Information", // Entries created by user code inside the function, such as when calling logger.LogInformation(), are assigned a category of Function.<FUNCTION_NAME>.User.
            },
        */
        logging.Services.Configure<LoggerFilterOptions>(options =>
        {
            LoggerFilterRule? defaultRule = options.Rules.FirstOrDefault(rule => rule.ProviderName
                == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

            if (defaultRule is not null)
            {
                options.Rules.Remove(defaultRule);
            }
        });
    })
    .Build();

host.Run();