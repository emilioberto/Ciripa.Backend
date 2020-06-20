using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ciripa.Business.Commands
{
    public class UpdateContractCommand : IRequest<int>
    {
        public int Id { get; private set; }
        public ContractDto Contract { get; private set; }

        public UpdateContractCommand(int id, ContractDto contract)
        {
            Id = id;
            Contract = contract;
        }
    }

    public class UpdateContractCommandHandler : IRequestHandler<UpdateContractCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public UpdateContractCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateContractCommand request, CancellationToken ct)
        {
            var alreadyUsedContract = await _context.Set<Kid>().AnyAsync(x => x.ContractId == request.Id);

            if (alreadyUsedContract)
            {
                throw new InvalidOperationException("Cannot update a contract already in use");
            }

            var entity = _context.Set<Contract>().Find(request.Id);
            entity = _mapper.Map(request.Contract, entity);
            _context.Update(entity);
            return await _context.SaveChangesAsync(ct);
        }
    }
}