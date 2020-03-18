using System.Collections.Generic;
using System.Threading.Tasks;
using Ciripa.Business.Commands;
using Ciripa.Business.Queries;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ciripa.Web.Controllers
{
    [Route("api/kids")]
    public class KidsController : Controller
    {
        private readonly IMediator _mediator;
        
        public KidsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public Task<KidDto> GetKid(int id)
        {
            return _mediator.Send(new GetKidQuery(id));
        }

        [HttpGet()]
        public Task<List<KidDto>> GetKidsList()
        {
            return _mediator.Send(new GetKidsListQuery());
        }

        [HttpPost()]
        public Task<int> CreateKid([FromBody] UpsertKidDto kid)
        {
            return _mediator.Send(new CreateKidCommand(kid));
        }
        
        [HttpPut("{id}")]
        public Task<int> UpdateKid(int id, [FromBody] UpsertKidDto kid)
        {
            return _mediator.Send(new UpdateKidCommand(id, kid));
        }
        
        [HttpDelete("{id}")]
        public Task<int> DeleteKid(int id)
        {
            return _mediator.Send(new DeleteKidCommand(id));
        }
    }
}