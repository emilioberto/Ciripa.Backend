using System;
using System.Collections.Generic;
using Ciripa.Data.Interfaces;

namespace Ciripa.Data.Entities
{
    public class Kid : IBaseEntity
    {
        public Kid()
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FiscalCode { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public string Notes { get; set; }
        public DateTime? SubscriptionPaidDate { get; set; }
        public decimal SubscriptionAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool ExtraServicesEnabled { get; set; }
        public ICollection<Presence> PresencesList { get; set; }
        public Parent Parent1 { get; set; }
        public Parent Parent2 { get; set; }
    }
}