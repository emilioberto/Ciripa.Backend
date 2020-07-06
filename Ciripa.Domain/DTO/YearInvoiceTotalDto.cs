
namespace Ciripa.Domain.DTO
{
    public class YearInvoiceTotalDto
    {
        public int? Id { get; set; }
        public int KidId { get; set; }
        public KidDto Kid { get; set; }
        public ParentDto BillingParent { get; set; }
        public decimal? Amount { get; set; }
    }
}
