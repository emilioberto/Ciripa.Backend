using System;

namespace Ciripa.Data.Entities
{
    public class SpecialContract : Contract
    {
        public bool WeeklyContract { get; set; }

        public SpecialContract() { }

        public SpecialContract(int id, string description, decimal hourCost, decimal extraHourCost, DateTime? startTime, DateTime? endTime, decimal minContractValue)
            : base(id, description, hourCost, extraHourCost, startTime, endTime, minContractValue)
        { }
    }
}