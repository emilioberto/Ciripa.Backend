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
    public class GetKidQuery : IRequest<KidDto>
    {
        public int Id { get; private set; }

        public GetKidQuery(int id)
        {
            Id = id;
        }
    }

    public class GetKidQueryHandler : IRequestHandler<GetKidQuery, KidDto>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetKidQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<KidDto> Handle(GetKidQuery request, CancellationToken ct)
        {
            return _context
                .Set<Kid>()
                .Include(x => x.Parent1)
                .Include(x => x.Parent2)
                .AsNoTracking()
                .ProjectTo<KidDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == request.Id, ct);
        }
    }
}