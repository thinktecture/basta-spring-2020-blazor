using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorConfTool.Server.Model;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using BlazorConfTool.Shared;

namespace BlazorConfTool.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferencesController : ControllerBase
    {
        private readonly ILogger<ConferencesController> _logger;
        private readonly ConferencesDbContext _conferencesDbContext;
        private readonly IMapper _mapper;
        private readonly ConferenceValidator _validator;

        public ConferencesController(ILogger<ConferencesController> logger, 
            ConferencesDbContext conferencesDbContext, 
            IMapper mapper,
            ConferenceValidator validator)
        {
            _logger = logger;
            _conferencesDbContext = conferencesDbContext;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IEnumerable<Shared.DTO.ConferenceOverview> Get()
        {
            var conferences = _conferencesDbContext.Conferences.ToList();

            return _mapper.Map<IEnumerable<Shared.DTO.ConferenceOverview>>(conferences);
        }

        [HttpGet("{id}")]
        public Shared.DTO.ConferenceDetails Get(string id)
        {
            var conferenceDetails = _conferencesDbContext.Conferences.Where(c => c.ID == Guid.Parse(id)).FirstOrDefault();

            return _mapper.Map<Shared.DTO.ConferenceDetails>(conferenceDetails);
        }

        [HttpPost]
        public async Task<ActionResult<Shared.DTO.ConferenceDetails>> PostConference(Shared.DTO.ConferenceDetails conference)
        {
            if(!_validator.IsValidPeriod(conference.DateFrom, conference.DateTo))
            {
                return BadRequest();
            }

            var conf = _mapper.Map<Conference>(conference);
            _conferencesDbContext.Conferences.Add(conf);
            await _conferencesDbContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = conference.ID }, _mapper.Map<Shared.DTO.ConferenceDetails>(conf));
        }
    }
}
