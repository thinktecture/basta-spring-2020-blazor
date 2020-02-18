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
        public string Mode { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        private bool _isShow { get; set; }

        [Inject]
        private ConferencesService _conferencesService { get; set; }

        private ConferenceDetails _conferenceDetails;

        public Conference()
        {
            _conferenceDetails = new ConferenceDetails();
            _conferenceDetails.DateFrom = DateTime.UtcNow;
            _conferenceDetails.DateTo = DateTime.UtcNow;
        }

        protected override async Task OnInitializedAsync()
        {
            _isShow = Mode == Modes.Show;

            switch (Mode)
            {
                case Modes.Show:
                    var result = await _conferencesService.GetConferenceDetails(Id);
                    _conferenceDetails = result;
                    break;
            }
        }
    }
}
