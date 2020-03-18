using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Business.Queries;
using Ciripa.Business.Queries.Presences;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain;
using MediatR;

namespace Ciripa.Business.Commands
{
    public class CreateMissingPresencesCommand : IRequest<bool>
    {
        public Date Date { get; private set; }

        public CreateMissingPresencesCommand(Date date)
        {
            Date = date;
        }
    }

    public class CreateMissingPresencesCommandHandler : IRequestHandler<CreateMissingPresencesCommand, bool>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateMissingPresencesCommandHandler(CiripaContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateMissingPresencesCommand request, CancellationToken ct)
        {
            var kids = await _mediator.Send(new GetKidsByDateQuery(request.Date), ct);
            var presences = await _mediator.Send(new GetPresencesByDateQuery(request.Date), ct);

            var missingPresences = new List<Presence>();
            kids.ForEach(kid =>
            {
                if (presences.SingleOrDefault(p => p.KidId == kid.Id) == null)
                {
                    missingPresences.Add(new Presence(kid.Id, request.Date));
                }
            });
            
            _context.AddRange(missingPresences);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}