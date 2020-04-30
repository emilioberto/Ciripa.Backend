using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;
using MediatR;

namespace Ciripa.Business.Commands
{
    public class UpsertInvoicesCommand : IRequest<int>
    {
        public List<InvoiceDto> Invoices { get; private set; }

        public UpsertInvoicesCommand(List<InvoiceDto> invoices)
        {
            Invoices = invoices;
        }
    }

    public class UpsertInvoicesCommandHandler : IRequestHandler<UpsertInvoicesCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public UpsertInvoicesCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<int> Handle(UpsertInvoicesCommand request, CancellationToken ct)
        {
            if (request.Invoices == null || request.Invoices.Count == 0)
            {
                return Task.FromResult(0);
            }
            
            foreach (var invoice in request.Invoices)
            {
                if (invoice.Id != null)
                {
                    var entity = _context.Set<Invoice>().Find(invoice.Id);
                    entity = _mapper.Map<InvoiceDto, Invoice>(invoice, entity);
                    _context.Update(entity);
                }
                else
                {
                    var duplicated = _context.Set<Invoice>().AsQueryable().Any(x => x.Date == invoice.Date && x.KidId == invoice.KidId && x.Id != invoice.Id);
                    if (duplicated)
                    {
                        throw new Exception();
                    }
                    
                    var entity = _mapper.Map<Invoice>(invoice);
                    _context.Add(entity);
                }
            }
            return _context.SaveChangesAsync(ct);
        }
    }
}