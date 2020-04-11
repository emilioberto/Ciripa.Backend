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
                .ForMember(x => x.MorningHours, opt => opt.MapFrom(x => CalculateMorningHours(x)))
                .ForMember(x => x.EveningHours, opt => opt.MapFrom(x => CalculateEveningHours(x)))
                .ReverseMap();
            
            CreateMap<Settings, SettingsDto>()
                .ReverseMap();
        }

        private double CalculateMorningHours(Presence presence)
        {
            if (presence.MorningEntry == null || presence.MorningExit == null)
            {
                return 0;
            }

            return (presence.MorningExit.Value - presence.MorningEntry.Value).TotalHours;
        }
        
        private double CalculateEveningHours(Presence presence)
        {
            if (presence.EveningEntry == null || presence.EveningExit == null)
            {
                return 0;
            }

            return (presence.EveningExit.Value - presence.EveningEntry.Value).TotalHours;
        }
    }
}