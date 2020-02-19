using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorConfTool.Client.Services;

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

            await builder.Build().RunAsync();
        }
    }
}
