using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ciripa.Business.Queries
{
    public class GetKidsByDateQuery : IRequest<List<KidDto>>
    {
        public DateTime Date { get; private set; }

        public GetKidsByDateQuery(DateTime date)
        {
            Date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
    }

    public class GetKidsByDateQueryHandler : IRequestHandler<GetKidsByDateQuery, List<KidDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetKidsByDateQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<KidDto>> Handle(GetKidsByDateQuery request, CancellationToken ct)
        {
            return _context
                .Set<Kid>()
                .Where(x => x.From <= request.Date && (x.To == null || x.To >= request.Date))
                .ProjectTo<KidDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }
    }
}