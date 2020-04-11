using System.Collections.Generic;

namespace Ciripa.Domain.DTO
{
    public class PresencesSummaryDto
    {
        public List<PresenceListItemDto> Presences { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalAmount { get; set; }

        public PresencesSummaryDto(List<PresenceListItemDto> presences, decimal totalHours, decimal totalAmount)
        {
            Presences = presences;
            TotalHours = totalHours;
            TotalAmount = totalAmount;
        }
    }
}