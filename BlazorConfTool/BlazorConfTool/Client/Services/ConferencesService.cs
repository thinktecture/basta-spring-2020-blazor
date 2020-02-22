using BlazorConfTool.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Oidc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorConfTool.Client.Services
{
    public class ConferencesService
    {
        private OidcHttpClient _httpClient;
        private string _conferencesUrl = "/api/conferences/";
        private string _statisticsUrl = "/api/statistics/";

        public ConferencesService(OidcHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ConferenceOverview>> ListConferences()
        {
            var result = await _httpClient.GetJsonAsync<List<ConferenceOverview>>(_conferencesUrl);
            
            return result;
        }

        public async Task<ConferenceDetails> GetConferenceDetails(Guid id)
        {
            var result = await _httpClient.GetJsonAsync<ConferenceDetails>(_conferencesUrl + id);

            return result;
        }

        public async Task<ConferenceDetails> AddConference(ConferenceDetails conference)
        {
            var result = await _httpClient.PostJsonAsync<ConferenceDetails>(
                _conferencesUrl, conference);

            return result;
        }

        public async Task<dynamic> GetStatistics()
        {
            var result = await _httpClient.GetJsonAsync<dynamic>(_statisticsUrl);

            return result;
        }
    }
}
