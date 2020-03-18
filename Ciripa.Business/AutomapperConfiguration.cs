using AutoMapper;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;

namespace Ciripa.Business
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<Kid, KidDto>()
                .ReverseMap();
            
            CreateMap<Kid, UpsertKidDto>()
                .ReverseMap();
            
            CreateMap<Presence, PresenceDto>()
                .ReverseMap();
            
            CreateMap<Presence, PresenceListItemDto>()
                .ReverseMap();
            
            CreateMap<Settings, SettingsDto>()
                .ReverseMap();
        }
    }
}