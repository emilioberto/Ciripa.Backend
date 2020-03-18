using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;
using MediatR;

namespace Ciripa.Business.Commands
{
    public class UpdateKidCommand : IRequest<int>
    {
        public int Id { get; private set; }
        public UpsertKidDto Model { get; private set; }

        public UpdateKidCommand(int id, UpsertKidDto model)
        {
            Id = id;
            Model = model;
        }
    }

    public class UpdateKidCommandHandler : IRequestHandler<UpdateKidCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public UpdateKidCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateKidCommand request, CancellationToken ct)
        {
            var kid = await _context.Set<Kid>().FindAsync(request.Id);
            kid = _mapper.Map<UpsertKidDto, Kid>(request.Model, kid);
            _context.Kids.Update(kid);
            await _context.SaveChangesAsync(ct);
            return kid.Id;
        }
    }
}