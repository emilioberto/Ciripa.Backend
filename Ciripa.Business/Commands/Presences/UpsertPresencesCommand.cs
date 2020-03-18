using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;
using MediatR;

namespace Ciripa.Business.Commands
{
    public class UpsertPresencesCommand : IRequest<int>
    {
        public List<PresenceDto> Presences { get; private set; }

        public UpsertPresencesCommand(List<PresenceDto> presences)
        {
            Presences = presences;
        }
    }

    public class UpsertPresencesCommandHandler : IRequestHandler<UpsertPresencesCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public UpsertPresencesCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<int> Handle(UpsertPresencesCommand request, CancellationToken ct)
        {
            if (request.Presences == null || request.Presences.Count == 0)
            {
                return Task.FromResult(0);
            }
            
            foreach (var presence in request.Presences)
            {
                if (presence.Id != null)
                {
                    var entity = _context.Set<Presence>().Find(presence.Id);
                    entity = _mapper.Map<PresenceDto, Presence>(presence, entity);
                    _context.Update(entity);
                }
                else
                {
                    var duplicated = _context.Set<Presence>().AsQueryable().Any(x => x.Date == presence.Date && x.KidId == presence.KidId && x.Id != presence.Id);
                    if (duplicated)
                    {
                        throw new Exception();
                    }
                    
                    var entity = _mapper.Map<Presence>(presence);
                    _context.Add(entity);
                }
            }
            return _context.SaveChangesAsync(ct);
        }
    }
}