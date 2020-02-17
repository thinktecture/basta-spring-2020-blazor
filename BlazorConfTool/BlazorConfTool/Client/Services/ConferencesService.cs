using BlazorConfTool.Shared.DTO;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorConfTool.Client.Services
{
    public class ConferencesService
    {
        private HttpClient _httpClient;
        private string _conferencesUrl = "https://localhost:44323/api/conferences/";

        public ConferencesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Conference>> GetConferences()
        {
            var result = await _httpClient.GetJsonAsync<List<Conference>>(_conferencesUrl);

            return result;
        }

        public async Task<ConferenceDetails> GetConferenceDetails(Guid id)
        {
            var result = await _httpClient.GetJsonAsync<ConferenceDetails>(_conferencesUrl + id);

            return result;
        }
    }
}
