using Ciripa.Data.Interfaces;

namespace Ciripa.Data.Entities
{
    public class Settings : IBaseEntity
    {
        public int Id { get; set; }
        public decimal HourCost { get; set; }
        public decimal ExtraHourCost { get; set; }
        public decimal ContractAmount { get; set; }
        
        public Settings() {}

        public Settings(int id, decimal hourCost, decimal extraHourCost, decimal contractAmount)
        {
            Id = id;
            HourCost = hourCost;
            ExtraHourCost = extraHourCost;
            ContractAmount = contractAmount;
        }
    }
}