using System;

namespace Ciripa.Domain.DTO
{
    public class PresenceListItemDto
    {
        public int? Id { get; set; }
        public Date Date { get; set; }
        public DateTime? MorningEntry { get; set; }
        public DateTime? MorningExit { get; set; }
        public double MorningHours { get; set; }
        public DateTime? EveningEntry { get; set; }
        public DateTime? EveningExit { get; set; }
        public double EveningHours { get; set; }
        public int KidId { get; set; }
    }
}