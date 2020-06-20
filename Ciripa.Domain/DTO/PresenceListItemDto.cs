using System;

namespace Ciripa.Domain.DTO
{
    public class PresenceListItemDto
    {
        public int? Id { get; set; }
        public Date Date { get; set; }
        public DateTime? MorningEntry { get; set; }
        public DateTime? MorningExit { get; set; }
        public decimal MorningHours { get; set; }
        public DateTime? EveningEntry { get; set; }
        public DateTime? EveningExit { get; set; }
        public decimal EveningHours { get; set; }
        public decimal DailyHours { get; set; }
        public decimal ExtraContractHours { get; set; }
        public decimal ExtraServiceTimeHours { get; set; }
        public int KidId { get; set; }
    }
}