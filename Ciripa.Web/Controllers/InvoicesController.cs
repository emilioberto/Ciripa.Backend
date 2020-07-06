using System.Collections.Generic;
using System.Threading.Tasks;
using Ciripa.Business.Commands;
using Ciripa.Business.Queries;
using Ciripa.Business.Queries.Presences;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ciripa.Web.Controllers
{
    [Route("api/invoices")]
    public class InvoicesController : Controller
    {
        private readonly IMediator _mediator;
        
        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("list")]
        public async Task<List<InvoiceDto>> GetInvoicesByDate([FromBody] ByDateDto model)
        {
            return await _mediator.Send(new CreateMissingInvoicesCommand(model.Date));
        }

        [HttpPut()]
        public Task<int> UpsertInvoices([FromBody] List<InvoiceDto> invoices)
        {
            return _mediator.Send(new UpsertInvoicesCommand(invoices));
        }

        [HttpPut("year")]
        public Task<List<YearInvoiceTotalDto>> UpsertInvoices([FromBody] ByDateDto model)
        {
            return _mediator.Send(new GetYearInvoicesByDateQuery(model.Date));
        }
    }
}