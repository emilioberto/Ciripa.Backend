using System;

namespace Ciripa.Domain.DTO
{
    public class ContractDto
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public bool MonthlyContract { get; set; }
        public decimal DailyHours { get; set; }
        public decimal MonthlyHours { get; set; }
        public decimal HourCost { get; set; }
        public decimal ExtraHourCost { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal MinContractValue { get; set; }
    }
}
