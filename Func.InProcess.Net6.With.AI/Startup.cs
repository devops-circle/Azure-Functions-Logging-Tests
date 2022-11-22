using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

// See to setup DI:
// https://learn.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
[assembly: FunctionsStartup(typeof(Func.InProcess.Net6.With.AI.Startup))]

namespace Func.InProcess.Net6.With.AI;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // https://learn.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection#use-injected-dependencies
        builder.Services.AddHttpClient();

        // Setup DI
        builder.Services.AddTransient<IUserDataService, UserDataService>();

        // If you want to setup your own logging services:
        // https://learn.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection#logging-services
        ////builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
    }
}
