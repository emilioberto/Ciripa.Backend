using System.Collections.Generic;
using System.Threading.Tasks;
using Ciripa.Business.Commands;
using Ciripa.Business.Queries.ExtraPresences;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ciripa.Web.Controllers
{
    [Route("api/extrapresences")]
    public class ExtraPresencesController : Controller
    {
        private readonly IMediator _mediator;
        
        public ExtraPresencesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("list/kid/{kidId}")]
        public Task<ExtraPresencesSummaryDto> GetKidPresencesByMonth(int kidId, [FromBody] ByDateDto model)
        {
            return _mediator.Send(new GetMonthlyExtraPresencesByKidQuery(kidId, model.Date));
        }
        
        [HttpPost("list")]
        public async Task<List<ExtraPresenceDto>> GetPresencesByDate([FromBody] ByDateDto model)
        {
            return await _mediator.Send(new CreateMissingExtraPresencesCommand(model.Date));
        }
        
        [HttpPut()]
        public Task<int> UpsertPresences([FromBody] List<ExtraPresenceDto> presences)
        {
            return _mediator.Send(new UpsertExtraPresencesCommand(presences));
        }
    }
}