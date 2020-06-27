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
    public class UpsertExtraPresencesCommand : IRequest<int>
    {
        public List<ExtraPresenceDto> Presences { get; private set; }

        public UpsertExtraPresencesCommand(List<ExtraPresenceDto> presences)
        {
            Presences = presences;
        }
    }

    public class UpsertExtraPresencesCommandHandler : IRequestHandler<UpsertExtraPresencesCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public UpsertExtraPresencesCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<int> Handle(UpsertExtraPresencesCommand request, CancellationToken ct)
        {
            if (request.Presences == null || request.Presences.Count == 0)
            {
                return Task.FromResult(0);
            }
            
            foreach (var presence in request.Presences)
            {
                if (presence.Id != null)
                {
                    var entity = _context.Set<ExtraPresence>().Find(presence.Id);
                    entity = _mapper.Map<ExtraPresenceDto, ExtraPresence>(presence, entity);
                    _context.Update(entity);
                }
                else
                {
                    var duplicated = _context.Set<ExtraPresence>().AsQueryable().Any(x => x.Date == presence.Date && x.KidId == presence.KidId && x.Id != presence.Id);
                    if (duplicated)
                    {
                        throw new Exception();
                    }
                    
                    var entity = _mapper.Map<ExtraPresence>(presence);
                    _context.Add(entity);
                }
            }
            return _context.SaveChangesAsync(ct);
        }
    }
}