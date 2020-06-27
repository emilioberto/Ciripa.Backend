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
        public int BillingParentId { get; set; }
        public Parent BillingParent { get; set; }
        public string Number { get; set; }
        public Date Date { get; set; }
        public decimal? Amount { get; set; }
        public decimal? SubscriptionAmount { get; set; }
        public DateTime? SubscriptionPaidDate { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }

        public Invoice() { }

        public Invoice(int kidId, Date date)
        {
            KidId = kidId;
            Date = date;
            Number = null;
            Amount = 0;
            PaymentMethod = null;
            PaymentDate = null;
        }

        public Invoice(int kidId, Date date, decimal subscriptionAmount, Date subscriptionPaidDate)
        {
            KidId = kidId;
            Date = date;
            Number = null;
            Amount = 0;
            PaymentMethod = null;
            PaymentDate = null;
            SubscriptionAmount = subscriptionAmount;
            SubscriptionPaidDate = subscriptionPaidDate;
        }
    }
}
