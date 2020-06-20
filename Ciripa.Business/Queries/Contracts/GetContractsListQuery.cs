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
    public class GetContractsListQuery : IRequest<List<ContractDto>>
    {
        public GetContractsListQuery()
        {
        }
    }

    public class GetContractsListQueryHandler : IRequestHandler<GetContractsListQuery, List<ContractDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetContractsListQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<ContractDto>> Handle(GetContractsListQuery request, CancellationToken ct)
        {
            return _context.Set<Contract>()
                .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }
    }
}