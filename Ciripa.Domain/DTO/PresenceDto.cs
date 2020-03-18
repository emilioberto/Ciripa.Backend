using System;

namespace Ciripa.Domain.DTO
{
    public class PresenceDto
    {
        public int? Id { get; set; }
        public Date Date { get; set; }
        public DateTime? MorningEntry { get; set; }
        public DateTime? MorningExit { get; set; }
        public DateTime? EveningEntry { get; set; }
        public DateTime? EveningExit { get; set; }
        public int KidId { get; set; }
        public KidDto Kid { get; set; }
    }
}