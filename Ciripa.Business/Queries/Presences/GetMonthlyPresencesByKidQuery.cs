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

        public GetKidPresencesByDateQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PresencesSummaryDto> Handle(GetMonthlyPresencesByKidQuery request, CancellationToken ct)
        {
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

            var settings = await _context.Set<Settings>().FirstAsync(ct);

            var totalHours = CalculateTotalHours(presences);
            var totalAmount = totalHours * settings.HourCost;
            
            return new PresencesSummaryDto(presences, totalHours, totalAmount);
 
        }

        private decimal CalculateTotalHours(List<PresenceListItemDto> presences)
        {
            var morningHours = presences.Sum(x => x.MorningHours);
            var eveningHours = presences.Sum(x => x.EveningHours);
            var total = morningHours + eveningHours;
            return Convert.ToDecimal(total);
        }
    }
}