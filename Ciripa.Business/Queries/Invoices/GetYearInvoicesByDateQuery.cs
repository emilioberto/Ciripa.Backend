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
    public class GetYearInvoicesByDateQuery : IRequest<List<YearInvoiceTotalDto>>
    {
        public Date Date { get; private set; }

        public GetYearInvoicesByDateQuery(Date date)
        {
            Date = date;
        }
    }

    public class GetYearInvoicesByDateQueryHandler : IRequestHandler<GetYearInvoicesByDateQuery, List<YearInvoiceTotalDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetYearInvoicesByDateQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<YearInvoiceTotalDto>> Handle(GetYearInvoicesByDateQuery request, CancellationToken ct)
        {
            var result = await _context
                .Set<Invoice>()
                .OrderBy(x => x.Date)
                .Include(x => x.Kid.Parent1)
                .Include(x => x.Kid.Parent2)
                .AsNoTracking()
                .ToListAsync(ct);

            result = result.Where(x => x.Date.Year == request.Date.Year).ToList();

            var groupedInvoices = result.GroupBy(x => x.KidId);


            return groupedInvoices.Select(x =>
            {
                var billingParent = x.First().Kid.Parent2.Billing ? x.First().Kid.Parent2 : x.First().Kid.Parent1;
                return new YearInvoiceTotalDto
                {
                    Kid = _mapper.Map<KidDto>(x.First().Kid),
                    KidId = x.First().KidId,
                    Amount = x.Sum(x => x.Amount + x.SubscriptionAmount),
                    BillingParent = _mapper.Map<ParentDto>(billingParent),
                };
            }).ToList();
        }
    }
}