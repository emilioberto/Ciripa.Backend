using Ciripa.Data.Interfaces;
using Ciripa.Domain;
using System;

namespace Ciripa.Data.Entities
{
    public class Invoice : IBaseEntity, IDateEntity
    {
        public int Id { get; set; }
        public int KidId { get; set; }
        public Kid Kid { get; set; }
        public string Number { get; set; }
        public Date Date { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Hours { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }

        public Invoice() { }

        public Invoice(int kidId, Date date, decimal amount, decimal hours)
        {
            KidId = kidId;
            Date = date;
            Number = null;
            Amount = amount;
            Hours = hours;
            PaymentMethod = null;
            PaymentDate = null;
        }
    }
}
