using Ciripa.Data.Interfaces;
using Ciripa.Domain;
using System;

namespace Ciripa.Data.Entities
{
    public class Invoice : IBaseEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public Date Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
