using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorConfTool.Client.Services;
using System;
using Sotsera.Blazor.Oidc;

namespace BlazorConfTool.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton<ConferencesService>();
            builder.Services.AddSingleton<CountriesService>();

            builder.Services.AddAlerts();

            builder.Services.AddOptions();
            builder.Services.AddOidc(new Uri("https://demo.identityserver.io"), (settings, siteUri) =>
            {
                settings.UseDefaultCallbackUris(siteUri);
                settings.ClientId = "spa";
                settings.ResponseType = "code";
                settings.Scope = "openid profile email api";
            });

            await builder.Build().RunAsync();
        }
    }
}
