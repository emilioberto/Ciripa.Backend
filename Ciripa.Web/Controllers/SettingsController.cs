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
    [Route("api/settings")]
    public class SettingsController : Controller
    {
        private readonly IMediator _mediator;
        
        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet()]
        public Task<SettingsDto> GetSettings()
        {
            return _mediator.Send(new GetSettingsQuery());
        }
        
        [HttpPut()]
        public Task<int> UpdateSettings([FromBody] SettingsDto settings)
        {
            return _mediator.Send(new UpdateSettingsCommand(settings));
        }
    }
}