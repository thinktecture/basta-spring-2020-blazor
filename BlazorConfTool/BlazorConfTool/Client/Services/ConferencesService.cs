using BlazorConfTool.Server.Controllers.Client;
using Sotsera.Blazor.Oidc;
using System;

namespace BlazorConfTool.Client.Services
{
    public class ConferencesService
    {
        private string _apiBaseUrl = "https://localhost:44323/";

        public Conferences Conferences { get; set; }
        public Statistics Statistics { get; set; }

        public ConferencesService(OidcHttpClient httpClient)
        {
            Conferences = new Conferences(httpClient, new Uri(_apiBaseUrl));
            Statistics = new Statistics(httpClient, new Uri(_apiBaseUrl));
        }
    }
}
