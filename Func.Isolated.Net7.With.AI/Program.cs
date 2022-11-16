using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        // Is added by package "Microsoft.Azure.Functions.Worker.ApplicationInsights".
        // Documented here because it is still preview: https://github.com/Azure/azure-functions-dotnet-worker/pull/944#issue-1282987627
        builder
            .AddApplicationInsights()
            .AddApplicationInsightsLogger();

        // You will need extra configuration because above will only log per default Warning (default AI configuration) and above because of following line:
        // https://github.com/microsoft/ApplicationInsights-dotnet/blob/main/NETCORE/src/Shared/Extensions/ApplicationInsightsExtensions.cs#L427
        // This is documented here:
        // https://github.com/microsoft/ApplicationInsights-dotnet/issues/2610#issuecomment-1316672650
    })
    .Build();

host.Run();
