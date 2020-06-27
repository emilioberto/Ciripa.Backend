using System.Collections.Generic;
using System.Threading.Tasks;
using Ciripa.Business.Commands;
using Ciripa.Business.Queries;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ciripa.Web.Controllers
{
    [Route("api/contracts")]
    public class ContractsController : Controller
    {
        private readonly IMediator _mediator;

        public ContractsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<List<ContractDto>> GetContractsList()
        {
            return await _mediator.Send(new GetContractsListQuery());
        }

        [HttpGet("special")]
        public async Task<List<SpecialContractDto>> GetSpecialContractsList()
        {
            return await _mediator.Send(new GetSpecialContractsListQuery());
        }

        [HttpPost()]
        public Task<int> CreateContract([FromBody] ContractDto contract)
        {
            return _mediator.Send(new CreateContractCommand(contract));
        }

        [HttpPut("{id}")]
        public Task<int> UpsertContract(int id, [FromBody] ContractDto contract)
        {
            return _mediator.Send(new UpdateContractCommand(id, contract));
        }
    }
}