using TransferMate.Models.Contracts;

namespace TransferMate.Models
{
    public class Memory : BaseData, IMemory
    {
        public override string ComponentType { get; set; }
        public override string PartNumber { get; set; }
        public override string Name { get; set; }
        public override string Type { get; set; }
        public override double Price { get; set; }
    }
}
