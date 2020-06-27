using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;
using MediatR;

namespace Ciripa.Business.Commands
{
    public class CreateContractCommand : IRequest<int>
    {
        public ContractDto Contract { get; private set; }

        public CreateContractCommand(ContractDto contract)
        {
            Contract = contract;
        }
    }

    public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public CreateContractCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<int> Handle(CreateContractCommand request, CancellationToken ct)
        {
            var contract = request.Contract;
            var entity = _mapper.Map<SimpleContract>(contract);
            _context.Add(entity);

            return _context.SaveChangesAsync(ct);
        }
    }
}