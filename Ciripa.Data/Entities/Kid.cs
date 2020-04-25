using System;
using System.Collections.Generic;
using Ciripa.Data.Interfaces;
using Ciripa.Domain;

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
        public DateTime Birthdate { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public ContractType ContractType { get; set; }
        public decimal ContractValue { get; set; }
        public string Notes { get; set; }
        public bool SubscriptionPaid { get; set; }

        public decimal SubscriptionAmount { get; set; }

        //Billing data
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentFiscalCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Cap { get; set; }
        public string Province { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<Presence> PresencesList { get; set; }
    }
}