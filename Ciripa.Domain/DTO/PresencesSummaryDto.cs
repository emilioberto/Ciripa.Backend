using System.Collections.Generic;

namespace Ciripa.Domain.DTO
{
    public class PresencesSummaryDto
    {
        public List<PresenceListItemDto> Presences { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalExtraContractHours { get; set; }
        public decimal TotalExtraServiceTimeHours { get; set; }
        public decimal TotalAmount { get; set; }

        public PresencesSummaryDto(List<PresenceListItemDto> presences, decimal totalHours, decimal totalExtraContractHours, decimal totalExtraServiceTimeHours, decimal totalAmount)
        {
            Presences = presences;
            TotalHours = totalHours;
            TotalAmount = totalAmount;
            TotalExtraContractHours = totalExtraContractHours;
            TotalExtraServiceTimeHours = totalExtraServiceTimeHours;
    }
    }
}