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
    public class UpdateSettingsCommand : IRequest<int>
    {
        public SettingsDto Settings { get; private set; }

        public UpdateSettingsCommand(SettingsDto settings)
        {
            Settings = settings;
        }
    }

    public class UpdateSettingsCommandHandler : IRequestHandler<UpdateSettingsCommand, int>
    {
        private readonly CiripaContext _context;
        private readonly IMapper _mapper;

        public UpdateSettingsCommandHandler(CiripaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<int> Handle(UpdateSettingsCommand request, CancellationToken ct)
        {
            var settings = _context.Set<Settings>().Single();

            settings.SubscriptionAmount = request.Settings.SubscriptionAmount;
            settings.HourCost = request.Settings.HourCost;
            settings.ExtraHourCost = request.Settings.ExtraHourCost;

            _context.Update(settings);

            return _context.SaveChangesAsync(ct);
        }
    }
}