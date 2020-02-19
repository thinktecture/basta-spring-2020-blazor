using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorConfTool.Client.Services;
using System;
using Sotsera.Blazor.Oidc;
using System.Net.Http;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Grpc.Net.Client;
using GrpcGreeter;

namespace BlazorConfTool.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<ConferencesService>();
            builder.Services.AddScoped<CountriesService>();

            builder.Services.AddSingleton(services =>
            {
                var httpClient = new HttpClient(
                    new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
                var channel = GrpcChannel.ForAddress(
                    baseUri, new GrpcChannelOptions { HttpClient = httpClient });

                return new Greeter.GreeterClient(channel);
            });

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
