using BlazorConfTool.Client.Services;
using Blazored.Toast;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcGreeter;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Sotsera.Blazor.Oidc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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

            builder.Services.AddBlazoredToast();

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
