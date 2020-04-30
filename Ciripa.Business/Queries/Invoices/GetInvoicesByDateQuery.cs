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
    public class GetInvoicesByDateQuery : IRequest<List<InvoiceDto>>
    {
        public Date Date { get; private set; }

        public GetInvoicesByDateQuery(Date date)
        {
            Date = date;
        }
    }

    public class GetInvoicesByDateQueryHandler : IRequestHandler<GetInvoicesByDateQuery, List<InvoiceDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetInvoicesByDateQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InvoiceDto>> Handle(GetInvoicesByDateQuery request, CancellationToken ct)
        {
            var result = await _context
                .Set<Invoice>()
                .Where(x => x.Date == request.Date)
                .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
            return result;
        }
    }
}