﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ciripa.Data;
using Ciripa.Data.Entities;
using Ciripa.Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ciripa.Business.Queries
{
    public class GetKidsListQuery : IRequest<List<KidDto>>
    {
        public GetKidsListQuery()
        {
        }
    }

    public class GetKidsListQueryHandler : IRequestHandler<GetKidsListQuery, List<KidDto>>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public GetKidsListQueryHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<KidDto>> Handle(GetKidsListQuery request, CancellationToken ct)
        {
            return _context.Set<Kid>()
                .Include(x => x.Parent1)
                .Include(x => x.Parent2)
                .AsNoTracking()
                .ProjectTo<KidDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }
    }
}