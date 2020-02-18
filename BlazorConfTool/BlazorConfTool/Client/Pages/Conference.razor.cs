using BlazorConfTool.Client.Services;
using BlazorConfTool.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
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
        [Inject]
        private CountriesService _countriesService { get; set; }

        private ConferenceDetails _conferenceDetails;
        private List<string> _countries;

        public Conference()
        {
            _conferenceDetails = new ConferenceDetails
            {
                DateFrom = DateTime.UtcNow,
                DateTo = DateTime.UtcNow
            };

            _countries = new List<string>();
        }

        protected override async Task OnInitializedAsync()
        {
            _isShow = Mode == Modes.Show;

            switch (Mode)
            {
                case Modes.Show:
                    var conferenceResult = await _conferencesService.GetConferenceDetails(Id);
                    _conferenceDetails = conferenceResult;
                    break;
                case Modes.Edit:
                case Modes.New:
                    var countriesResult = await _countriesService.ListCountries();
                    _countries = countriesResult;
                    break;
            }
        }

        private async Task SaveConference(EditContext editContext)
        {
            await _conferencesService.AddConference(_conferenceDetails);

            Console.WriteLine("NEW Conference added...");
        }
    }
}