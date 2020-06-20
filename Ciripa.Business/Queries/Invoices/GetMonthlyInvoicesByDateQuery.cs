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
    public class GetMonthlyInvoicesByDateQuery : IRequest<List<InvoiceDto>>
    {
        public Date Date { get; private set; }

        public GetMonthlyInvoicesByDateQuery(Date date)
        {
            Date = date;
        }
    }

    public class GetMonthlyInvoicesByDateQueryHandler : IRequestHandler<GetMonthlyInvoicesByDateQuery, List<InvoiceDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetMonthlyInvoicesByDateQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InvoiceDto>> Handle(GetMonthlyInvoicesByDateQuery request, CancellationToken ct)
        {
            var result = await _context
                .Set<Invoice>()
                .OrderBy(x => x.Date)
                .AsNoTracking()
                .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            var extistingInvoices = result
                .Where(x => x.Date.Year == request.Date.Year && x.Date.Month == request.Date.Month)
                .ToList();

            return extistingInvoices;
        }
    }
}