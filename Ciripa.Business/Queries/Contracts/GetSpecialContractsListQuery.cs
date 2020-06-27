using System.Collections.Generic;
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
    public class GetSpecialContractsListQuery : IRequest<List<SpecialContractDto>>
    {
        public GetSpecialContractsListQuery()
        {
        }
    }

    public class GetSpecialContractsListQueryHandler : IRequestHandler<GetSpecialContractsListQuery, List<SpecialContractDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetSpecialContractsListQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<SpecialContractDto>> Handle(GetSpecialContractsListQuery request, CancellationToken ct)
        {
            return _context.Set<SpecialContract>()
                .ProjectTo<SpecialContractDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }
    }
}