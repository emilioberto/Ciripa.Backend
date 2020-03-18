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
    public class GetKidPresencesByDateQuery : IRequest<List<PresenceListItemDto>>
    {
        public int KidId { get; private set; }
        public Date Date { get; private set; }

        public GetKidPresencesByDateQuery(int kidId, Date date)
        {
            KidId = kidId;
            Date = date;
        }
    }

    public class
        GetKidPresencesByDateQueryHandler : IRequestHandler<GetKidPresencesByDateQuery, List<PresenceListItemDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetKidPresencesByDateQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PresenceListItemDto>> Handle(GetKidPresencesByDateQuery request, CancellationToken ct)
        {
            var result = await _context
                .Set<Presence>()
                .Where(x => x.KidId == request.KidId)
                .OrderBy(x => x.Date)
                .ProjectTo<PresenceListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return result
                .Where(x => x.Date.Year == request.Date.Year && x.Date.Month == request.Date.Month)
                .ToList();
        }
    }
}