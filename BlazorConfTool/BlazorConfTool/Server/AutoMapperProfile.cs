using AutoMapper;

namespace BlazorConfTool.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Model.Conference, Shared.DTO.Conference>();
            CreateMap<Shared.DTO.Conference, Model.Conference>();
            CreateMap<Model.Conference, Shared.DTO.ConferenceDetails>();
            CreateMap<Shared.DTO.ConferenceDetails, Model.Conference>();
        }
    }
}
