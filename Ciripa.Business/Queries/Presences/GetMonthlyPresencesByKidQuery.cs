using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Data.Interfaces;
using Ciripa.Domain;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ciripa.Business.Queries.Presences
{
    public class GetMonthlyPresencesByKidQuery : IRequest<PresencesSummaryDto>
    {
        public int KidId { get; private set; }
        public Date Date { get; private set; }

        public GetMonthlyPresencesByKidQuery(int kidId, Date date)
        {
            KidId = kidId;
            Date = date;
        }
    }

    public class
        GetKidPresencesByDateQueryHandler : IRequestHandler<GetMonthlyPresencesByKidQuery, PresencesSummaryDto>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetKidPresencesByDateQueryHandler(CiripaContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PresencesSummaryDto> Handle(GetMonthlyPresencesByKidQuery request, CancellationToken ct)
        {
            var kid = await _mediator.Send(new GetKidQuery(request.KidId));
            var settings = await _context.Set<Settings>().FirstAsync(ct);

            var result = await _context
                .Set<Presence>()
                .Where(x => x.KidId == request.KidId)
                .OrderBy(x => x.Date)
                .ToListAsync(ct);

            var existingPresences = result
                .Where(x => x.Date.Year == request.Date.Year && x.Date.Month == request.Date.Month)
                .ToList();

            var daysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(request.Date.Year, request.Date.Month))
                .Select(day => new Date(request.Date.Year, request.Date.Month, day))
                .ToList();

            var presenceList = new List<Presence>();

            daysInMonth.ForEach(day =>
            {
                var dailyPresence = existingPresences.SingleOrDefault(x => x.Date == day);
                presenceList.Add(dailyPresence ?? new Presence(request.KidId, day));
            });

            var presences = _mapper.Map<List<PresenceListItemDto>>(presenceList);

            var totalHours = CalculateTotalHours(presences);
            var totalExtraServiceTimeHours = 0m;
            var totalExtraContractHours = 0m;

            if (!kid.Contract.MonthlyContract)
            {
                var exceedingDays = presences.Where(x => x.DailyHours > kid.Contract.DailyHours).ToList();
                exceedingDays.ForEach(exceedingPresence =>
                {
                    if (exceedingPresence.Id == 0)
                    {
                        return;
                    }

                    var exceedingContractHours = exceedingPresence.DailyHours - kid.Contract.DailyHours;

                    var exceedingMorningServiceTimeHours = CalculateExceedingMorningServiceTime(exceedingPresence, kid.Contract);
                    var exceedingEveningServiceTimeHours = CalculateExceedingEveningServiceTime(exceedingPresence, kid.Contract);
                    var totalExceedingServiceTimeHours = exceedingMorningServiceTimeHours + exceedingEveningServiceTimeHours;
                    totalExtraServiceTimeHours += totalExceedingServiceTimeHours;
                    totalExtraContractHours += (exceedingContractHours - totalExceedingServiceTimeHours);

                    presences.Single(x => x.Id == exceedingPresence.Id).ExtraServiceTimeHours = totalExceedingServiceTimeHours;
                    presences.Single(x => x.Id == exceedingPresence.Id).ExtraContractHours = (exceedingContractHours - totalExceedingServiceTimeHours);
                });
            }
            else
            {
                var exceedingMonthlyContractHours = Math.Max((totalHours - kid.Contract.MonthlyHours), 0);

                presences.ForEach(presence =>
                {
                    if (presence.Id == 0)
                    {
                        return;
                    }

                    var exceedingMorningServiceTimeHours = CalculateExceedingMorningServiceTime(presence, kid.Contract);
                    var exceedingEveningServiceTimeHours = CalculateExceedingEveningServiceTime(presence, kid.Contract);
                    var totalExceedingServiceTimeHours = exceedingMorningServiceTimeHours + exceedingEveningServiceTimeHours;
                    totalExtraServiceTimeHours += totalExceedingServiceTimeHours;

                    presences.Single(x => x.Id == presence.Id).ExtraServiceTimeHours = totalExceedingServiceTimeHours;
                });

                if (exceedingMonthlyContractHours > 0)
                {
                    totalExtraContractHours += exceedingMonthlyContractHours - totalExtraServiceTimeHours;
                    //presences.Last(x => IsNotWeekend(x.Date.AsDateTime())).ExtraContractHours = totalExtraContractHours;
                    presences.Add(new PresenceListItemDto
                    {
                        Date = Date.MaxValue,
                        ExtraContractHours = totalExtraContractHours,
                    });
                }

            }


            var totalAmount = kid.Contract.MinContractValue + (totalExtraContractHours * kid.Contract.HourCost) + (totalExtraServiceTimeHours * kid.Contract.ExtraHourCost);

            return new PresencesSummaryDto(presences, totalHours, totalExtraContractHours, totalExtraServiceTimeHours, totalAmount);
        }

        private bool IsNotWeekend(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        private decimal CalculateExceedingMorningServiceTime(PresenceListItemDto presence, SimpleContract contract)
        {
            if (!presence.MorningEntry.HasValue)
            {
                return 0m;
            }

            var morningTime = GetTimeWithMinDate(presence.MorningEntry.Value);
            var startTime = GetTimeWithMinDate(contract.StartTime.Value);
            if (startTime > morningTime)
            {
                var totalHours = (startTime - morningTime).TotalHours;
                return Math.Round(Convert.ToDecimal(totalHours) * 2, MidpointRounding.AwayFromZero) / 2.0m;
            }
            return 0m;
        }

        private decimal CalculateExceedingEveningServiceTime(PresenceListItemDto presence, SimpleContract contract)
        {
            if (!presence.EveningExit.HasValue)
            {
                return 0m;
            }

            var endTime = GetTimeWithMinDate(contract.EndTime.Value);
            var eveningTime = GetTimeWithMinDate(presence.EveningExit.Value);
            if (endTime < eveningTime)
            {
                var totalHours = (eveningTime - endTime).TotalHours;
                return Math.Round(Convert.ToDecimal(totalHours) * 2, MidpointRounding.AwayFromZero) / 2;
            }
            return 0m;
        }

        private DateTime GetTimeWithMinDate(DateTime value)
        {
            var mindate = default(DateTime);
            return new DateTime(mindate.Year, mindate.Month, mindate.Day, value.Hour, value.Minute, 0);
        }

        private decimal CalculateTotalHours(List<PresenceListItemDto> presences)
        {
            var dainlyHoursSum = presences.Sum(x => x.DailyHours);
            return Convert.ToDecimal(dainlyHoursSum);
        }
    }
}