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
    public class GetSettingsQuery : IRequest<SettingsDto>
    {
        public GetSettingsQuery()
        {
        }
    }

    public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, SettingsDto>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetSettingsQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<SettingsDto> Handle(GetSettingsQuery request, CancellationToken ct)
        {
            return _context
                .Set<Settings>()
                .ProjectTo<SettingsDto>(_mapper.ConfigurationProvider)
                .SingleAsync(ct);
        }
    }
}