using Ciripa.Data.Interfaces;
using System;

namespace Ciripa.Data.Entities
{
    public class SimpleContract : Contract
    {
        public SimpleContract() { }

        public SimpleContract(int id, string description, decimal hourCost, decimal extraHourCost, DateTime? startTime, DateTime? endTime, decimal minContractValue)
            : base(id, description, hourCost, extraHourCost, startTime, endTime, minContractValue)
        { }
    }
}