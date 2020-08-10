using AutoMapper;
using Ciripa.Data.Entities;
using Ciripa.Domain;
using Ciripa.Domain.DTO;
using System;

namespace Ciripa.Business
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {

            CreateMap<Date, DateTime>()
                .ConvertUsing(e => (DateTime)e);

            CreateMap<DateTime, Date>()
                .ConvertUsing(e => (Date)e);

            CreateMap<Kid, KidDto>()
                .ReverseMap();

            CreateMap<Kid, UpsertKidDto>()
                .ReverseMap();

            CreateMap<Presence, PresenceDto>();

            CreateMap<PresenceDto, Presence>()
                .ForMember(x => x.Kid, opt => opt.Ignore());

            CreateMap<Presence, PresenceListItemDto>()
                .ForMember(x => x.MorningHours, opt => opt.MapFrom(x => CalculateMorningHours(x)))
                .ForMember(x => x.EveningHours, opt => opt.MapFrom(x => CalculateEveningHours(x)))
                .ForMember(x => x.DailyHours, opt => opt.MapFrom(x => CalculateDailyHours(x)))
                .ReverseMap();

            CreateMap<ExtraPresence, ExtraPresenceDto>();

            CreateMap<ExtraPresenceDto, ExtraPresence>()
                .ForMember(x => x.Kid, opt => opt.Ignore());

            CreateMap<ExtraPresence, ExtraPresenceListItemDto>()
                .ForMember(x => x.MorningHours, opt => opt.MapFrom(x => CalculateMorningHours(x)))
                .ForMember(x => x.EveningHours, opt => opt.MapFrom(x => CalculateEveningHours(x)))
                .ForMember(x => x.DailyHours, opt => opt.MapFrom(x => CalculateDailyHours(x)))
                .ReverseMap();

            CreateMap<Settings, SettingsDto>()
                .ReverseMap();

            CreateMap<Invoice, InvoiceDto>()
                .ForMember(x => x.BillingParent, opt => opt.MapFrom(x => (!x.Kid.Parent1.Billing && x.Kid.Parent2 != null && x.Kid.Parent2.Billing) ? x.Kid.Parent2 : x.Kid.Parent1));

            CreateMap<InvoiceDto, Invoice>()
                .ForMember(x => x.Kid, opt => opt.Ignore())
                .ForMember(x => x.BillingParent, opt => opt.Ignore());

            CreateMap<SimpleContract, ContractDto>()
                .ReverseMap();

            CreateMap<Parent, ParentDto>()
                .ReverseMap();

            CreateMap<SpecialContract, SpecialContractDto>()
                .ReverseMap();
        }

        private static Parent GetBillingParent(Kid kid)
        {
            if (kid.Parent1.Billing)
            {
                return kid.Parent1;
            } else if (kid.Parent2.Billing)
            {
                return kid.Parent2;
            } else
            {
                return kid.Parent1;
            }
        }

        private decimal CalculateMorningHours(Presence presence)
        {
            if (presence.MorningEntry == null || presence.MorningExit == null)
            {
                return 0;
            }

            var totalHours = (presence.MorningExit.Value - presence.MorningEntry.Value).TotalHours;
            var result = Math.Round(Convert.ToDecimal(totalHours) * 2, MidpointRounding.AwayFromZero) / 2.0m;
            return result;
        }

        private decimal CalculateEveningHours(Presence presence)
        {
            if (presence.EveningEntry == null || presence.EveningExit == null)
            {
                return 0;
            }

            var totalHours = (presence.EveningExit.Value - presence.EveningEntry.Value).TotalHours;
            var result = Math.Round(Convert.ToDecimal(totalHours) * 2, MidpointRounding.AwayFromZero) / 2.0m;
            return result;
        }

        private decimal CalculateDailyHours(Presence presence)
        {
            return CalculateMorningHours(presence) + CalculateEveningHours(presence);
        }

        private decimal CalculateMorningHours(ExtraPresence extraPresence)
        {
            if (extraPresence.MorningEntry == null || extraPresence.MorningExit == null)
            {
                return 0;
            }

            var totalHours = (extraPresence.MorningExit.Value - extraPresence.MorningEntry.Value).TotalHours;
            var result = Math.Round(Convert.ToDecimal(totalHours) * 2, MidpointRounding.AwayFromZero) / 2.0m;
            return result;
        }

        private decimal CalculateEveningHours(ExtraPresence extraPresence)
        {
            if (extraPresence.EveningEntry == null || extraPresence.EveningExit == null)
            {
                return 0;
            }

            var totalHours = (extraPresence.EveningExit.Value - extraPresence.EveningEntry.Value).TotalHours;
            var result = Math.Round(Convert.ToDecimal(totalHours) * 2, MidpointRounding.AwayFromZero) / 2.0m;
            return result;
        }

        private decimal CalculateDailyHours(ExtraPresence extraPresence)
        {
            return CalculateMorningHours(extraPresence) + CalculateEveningHours(extraPresence);
        }
    }
}