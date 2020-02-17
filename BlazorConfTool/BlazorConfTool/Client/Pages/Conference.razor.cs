using BlazorConfTool.Client.Services;
using BlazorConfTool.Shared.DTO;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorConfTool.Client.Pages
{
    public partial class Conference : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        [Inject]
        private ConferencesService _conferencesService { get; set; }

        private ConferenceDetails _conference = new ConferenceDetails();

        protected override async Task OnInitializedAsync()
        {
            var result = await _conferencesService.GetConferenceDetails(Id);

            _conference = result;
        }
    }
}
