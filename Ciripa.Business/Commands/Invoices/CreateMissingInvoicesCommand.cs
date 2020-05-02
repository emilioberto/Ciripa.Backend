using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Business.Queries;
using Ciripa.Business.Queries.Presences;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain;
using Ciripa.Domain.DTO;
using MediatR;

namespace Ciripa.Business.Commands
{
    public class CreateMissingInvoicesCommand : IRequest<List<InvoiceDto>>
    {
        public Date Date { get; private set; }

        public CreateMissingInvoicesCommand(Date date)
        {
            Date = new Date(date.Year, date.Month, 1);
        }
    }

    public class CreateMissingInvoicesCommandHandler : IRequestHandler<CreateMissingInvoicesCommand, List<InvoiceDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateMissingInvoicesCommandHandler(CiripaContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<InvoiceDto>> Handle(CreateMissingInvoicesCommand request, CancellationToken ct)
        {
            var kids = await _mediator.Send(new GetKidsByDateQuery(request.Date), ct);
            var invoices = await _mediator.Send(new GetMonthlyInvoicesByDateQuery(request.Date), ct);

            invoices.ForEach(async invoice =>
            {
                var presencesSummary = await _mediator.Send(new GetMonthlyPresencesByKidQuery(invoice.KidId, request.Date));
                var entity = _context.Set<Invoice>().Find(invoice.Id);
                entity = _mapper.Map<InvoiceDto, Invoice>(invoice, entity);
                entity.Amount = presencesSummary.TotalAmount;
                _context.Update(entity);
            });

            var missingInvoices = new List<Invoice>();
            kids.ForEach(async kid =>
            {
                if (invoices.SingleOrDefault(p => p.KidId == kid.Id) == null)
                {
                    var presencesSummary = await _mediator.Send(new GetMonthlyPresencesByKidQuery(kid.Id, request.Date));
                    if (presencesSummary?.TotalAmount != null)
                    {
                        missingInvoices.Add(new Invoice(kid.Id, request.Date, presencesSummary.TotalAmount, presencesSummary.TotalHours));
                    }
                    else
                    {
                        missingInvoices.Add(new Invoice(kid.Id, request.Date, 0, presencesSummary.TotalHours));
                    }
                }
            });

            if (missingInvoices.Count > 0)
            {
                _context.AddRange(missingInvoices);
            }

            await _context.SaveChangesAsync(ct);

            return await _mediator.Send(new GetMonthlyInvoicesByDateQuery(request.Date), ct);
        }
    }
}