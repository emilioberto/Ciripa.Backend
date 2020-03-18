using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Data;
using Ciripa.Data.Entities;
using MediatR;

namespace Ciripa.Business.Commands
{
    public class DeleteKidCommand : IRequest<int>
    {
        public int Id { get; private set; }

        public DeleteKidCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteKidCommandHandler : IRequestHandler<DeleteKidCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public DeleteKidCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<int> Handle(DeleteKidCommand request, CancellationToken ct)
        {
            var kid = _context.Set<Kid>().Find(request.Id);
            _context.Remove(kid);
            return _context.SaveChangesAsync(ct);
        }
    }
}