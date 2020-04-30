using System;
using Ciripa.Data.Entities;

namespace Ciripa.Domain.DTO
{
    public class KidDto
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FiscalCode { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string Notes { get; set; }
        //Billing data
        public ContractType ContractType { get; set; }
        public decimal ContractValue { get; set; }
        public bool SubscriptionPaid { get; set; }
        public decimal SubscriptionAmount { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentFiscalCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Cap { get; set; }
        public string Province { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}