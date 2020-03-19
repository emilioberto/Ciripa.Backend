using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ciripa.Business.Queries.Presences
{
    public class GetPresencesByDateQuery : IRequest<List<PresenceDto>>
    {
        public Date Date { get; private set; }

        public GetPresencesByDateQuery(Date date)
        {
            Date = date;
        }
    }

    public class GetPresencesByDateQueryHandler : IRequestHandler<GetPresencesByDateQuery, List<PresenceDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetPresencesByDateQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<PresenceDto>> Handle(GetPresencesByDateQuery request, CancellationToken ct)
        {
            return _context
                .Set<Presence>()
                .Where(x => x.Date == request.Date)
                .ProjectTo<PresenceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }
    }
}