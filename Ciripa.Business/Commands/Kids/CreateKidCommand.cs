using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;
using MediatR;

namespace Ciripa.Business.Commands
{
    public class CreateKidCommand : IRequest<int>
    {
        public UpsertKidDto Model { get; private set; }

        public CreateKidCommand(UpsertKidDto model)
        {
            Model = model;
        }
    }

    public class CreateKidCommandHandler : IRequestHandler<CreateKidCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public CreateKidCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateKidCommand request, CancellationToken ct)
        {
            var kid = _mapper.Map<Kid>(request.Model); 
            _context.Kids.Add(kid);
            await _context.SaveChangesAsync(ct);
            return kid.Id;
        }
    }
}