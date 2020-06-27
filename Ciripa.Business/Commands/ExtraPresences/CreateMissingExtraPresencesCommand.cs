using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Business.Queries;
using Ciripa.Business.Queries.ExtraPresences;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ciripa.Business.Commands
{
    public class CreateMissingExtraPresencesCommand : IRequest<List<ExtraPresenceDto>>
    {
        public Date Date { get; private set; }

        public CreateMissingExtraPresencesCommand(Date date)
        {
            Date = date;
        }
    }

    public class CreateMissingExtraPresencesCommandHandler : IRequestHandler<CreateMissingExtraPresencesCommand, List<ExtraPresenceDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateMissingExtraPresencesCommandHandler(CiripaContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<ExtraPresenceDto>> Handle(CreateMissingExtraPresencesCommand request, CancellationToken ct)
        {
            var kids = await _mediator.Send(new GetKidsByDateQuery(request.Date), ct);
            var presences = await _mediator.Send(new GetExtraPresencesByDateQuery(request.Date), ct);
            var specialContracts = await _context.Set<SpecialContract>().ToListAsync(ct);

            kids = kids.Where(x => x.ExtraServicesEnabled).ToList();

            var missingPresences = new List<ExtraPresence>();
            kids.ForEach(kid =>
            {
                if (presences.SingleOrDefault(p => p.KidId == kid.Id) == null)
                {
                    var extraPresence = new ExtraPresence(kid.Id, request.Date);
                    extraPresence.SpecialContractId = specialContracts[0].Id;
                    missingPresences.Add(extraPresence);
                }
            });

            if (missingPresences.Count > 0)
            {
                _context.AddRange(missingPresences);
                await _context.SaveChangesAsync(ct);
            }

            return await _mediator.Send(new GetExtraPresencesByDateQuery(request.Date), ct);
        }
    }
}