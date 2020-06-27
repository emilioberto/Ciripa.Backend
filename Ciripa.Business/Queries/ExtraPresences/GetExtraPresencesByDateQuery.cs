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

namespace Ciripa.Business.Queries.ExtraPresences
{
    public class GetExtraPresencesByDateQuery : IRequest<List<ExtraPresenceDto>>
    {
        public Date Date { get; private set; }

        public GetExtraPresencesByDateQuery(Date date)
        {
            Date = date;
        }
    }

    public class GetExtraPresencesByDateQueryHandler : IRequestHandler<GetExtraPresencesByDateQuery, List<ExtraPresenceDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetExtraPresencesByDateQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ExtraPresenceDto>> Handle(GetExtraPresencesByDateQuery request, CancellationToken ct)
        {
            var result = await _context
                .Set<ExtraPresence>()
                .Where(x => x.Date == request.Date)
                .AsNoTracking()
                .ProjectTo<ExtraPresenceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
            return result;
        }
    }
}