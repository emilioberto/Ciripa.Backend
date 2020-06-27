using System;
using Ciripa.Data.Interfaces;
using Ciripa.Domain;

namespace Ciripa.Data.Entities
{
    public class ExtraPresence : IBaseEntity, IDateEntity
    {
        public int Id { get; set; }
        public Date Date { get; set; }
        public int KidId { get; set; }
        public Kid Kid { get; set; }
        public DateTime? MorningEntry { get; set; }
        public DateTime? MorningExit { get; set; }
        public DateTime? EveningEntry { get; set; }
        public DateTime? EveningExit { get; set; }
        public int SpecialContractId {get; set; }
        public SpecialContract SpecialContract {get; set; }


        public ExtraPresence()
        {
        }

        public ExtraPresence(int kidId, Date date)
        {
            KidId = kidId;
            Date = date;
            MorningEntry = null;
            MorningExit = null;
            EveningEntry = null;
            EveningExit = null;
        }
        
    }
}