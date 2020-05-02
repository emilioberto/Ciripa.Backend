
using Ciripa.Data.Entities;
using System;

namespace Ciripa.Domain.DTO
{
    public class InvoiceDto
    {
        public int? Id { get; set; }
        public int KidId { get; set; }
        public KidDto Kid { get; set; }
        public string Number { get; set; }
        public Date Date { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Hours { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
