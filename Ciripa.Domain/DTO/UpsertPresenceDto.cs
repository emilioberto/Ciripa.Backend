using System;

namespace Ciripa.Domain.DTO
{
    public class UpsertPresenceDto
    {
        public int? Id { get; set; }
        public Date Date { get; set; }
        public DateTime? MorningEntry { get; set; }
        public DateTime? MorningExit { get; set; }
        public DateTime? EveningEntry { get; set; }
        public DateTime? EveningExit { get; set; }
    }
}