using BlazorConfTool.Server.Controllers.Client;
using System;
using System.Net.Http;

namespace BlazorConfTool.Client.Services
{
    public class CountriesService
    {
        private string _apiBaseUrl = "https://localhost:44323/";

        public Countries Countries { get; set; }

        public CountriesService(HttpClient httpClient)
        {
            Countries = new Countries(httpClient, new Uri(_apiBaseUrl));
        }
    }
}
