using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Business.Queries;
using Ciripa.Business.Queries.ExtraPresences;
using Ciripa.Business.Queries.Presences;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var lastDayOfMonth = GetLastDayOfMonth(request.Date);
            var kids = await _mediator.Send(new GetKidsByDateQuery(lastDayOfMonth), ct);
            var invoices = await _mediator.Send(new GetMonthlyInvoicesByDateQuery(request.Date), ct);

            invoices.ForEach(invoice =>
            {
                var entity = _context.Set<Invoice>().Include(x => x.Kid).Include(x => x.BillingParent).AsQueryable().First(x => x.Id == invoice.Id);
                entity = _mapper.Map<InvoiceDto, Invoice>(invoice, entity);

                var subscriptionPaidInMonth = entity.Kid.SubscriptionPaidDate.HasValue && IsInMonth(entity.Kid.SubscriptionPaidDate.Value, request.Date);
                if (subscriptionPaidInMonth)
                {
                    entity.SubscriptionAmount = entity.Kid.SubscriptionAmount;
                    entity.SubscriptionPaidDate = entity.Kid.SubscriptionPaidDate;
                }

                _context.Update(entity);
            });

            var missingInvoices = new List<Invoice>();
            kids.ForEach(async kid =>
            {
                if (invoices.Where(p => p.KidId == kid.Id).Count() == 0)
                {
                    var presencesSummary = await _mediator.Send(new GetMonthlyPresencesByKidQuery(kid.Id, request.Date));
                    var extraPresencesSummary = await _mediator.Send(new GetMonthlyExtraPresencesByKidQuery(kid.Id, request.Date));

                    var subscriptionPaidInMonth = kid.SubscriptionPaidDate.HasValue && IsInMonth(kid.SubscriptionPaidDate.Value, request.Date);

                    if (subscriptionPaidInMonth)
                    {
                        missingInvoices.Add(new Invoice(kid.Id, request.Date, kid.SubscriptionAmount, kid.SubscriptionPaidDate.Value));
                    }
                    else
                    {
                        missingInvoices.Add(new Invoice(kid.Id, request.Date));
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

        private bool IsInMonth(Date subscriptionPaidDate, Date currentMonthDate)
        {
            return subscriptionPaidDate.Year == currentMonthDate.Year && subscriptionPaidDate.Month == currentMonthDate.Month;
        }
        private static Date GetLastDayOfMonth(DateTime requestDate)
        {
            var lastDayOfMonth = new DateTime(requestDate.Year, requestDate.Month, DateTime.DaysInMonth(requestDate.Year, requestDate.Month));
            return new Date(lastDayOfMonth);
        }
    }
}