using System;
using Ciripa.Data.Entities;

namespace Ciripa.Domain.DTO
{
    public class KidDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FiscalCode { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public int ContractId { get; set; }
        public SimpleContract Contract { get; set; }
        public string Notes { get; set; }
        public DateTime? SubscriptionPaidDate { get; set; }
        public decimal SubscriptionAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool ExtraServicesEnabled { get; set; }
        public Parent Parent1 { get; set; }
        public Parent Parent2 { get; set; }
    }
}