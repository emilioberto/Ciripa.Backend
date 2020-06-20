using System;
using Ciripa.Data.Entities;

namespace Ciripa.Domain.DTO
{
    public class UpsertKidDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FiscalCode { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public int ContractId { get; set; }
        public string Notes { get; set; }
        public DateTime? SubscriptionPaidDate { get; set; }
        public decimal SubscriptionAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool ExtraServicesEnabled { get; set; }
        public ParentDto Parent1 { get; set; }
        public ParentDto Parent2 { get; set; }
    }
}