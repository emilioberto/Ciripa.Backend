using Ciripa.Data.Interfaces;
using System;

namespace Ciripa.Data.Entities
{
    public abstract class Contract : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool MonthlyContract { get; set; }
        public decimal DailyHours { get; set; }
        public decimal MonthlyHours { get; set; }
        public decimal HourCost { get; set; }
        public decimal ExtraHourCost { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal MinContractValue { get; set; }

        public Contract() { }

        public Contract(int id, string description, decimal hourCost, decimal extraHourCost, DateTime? startTime, DateTime? endTime, decimal minContractValue)
        {
            Id = id;
            Description = description;
            HourCost = hourCost;
            ExtraHourCost = extraHourCost;
            StartTime = startTime;
            EndTime = endTime;
            MinContractValue = minContractValue;
        }
    }
}