using TransferMate.Models.Contracts;

namespace TransferMate.Models
{
    public class Motherboard : BaseData, IMotherboard
    {
        public override string ComponentType { get; set; }
        public override string PartNumber { get; set; }
        public override string Name { get; set; }
        public override string Socket { get; set; }
        public override double Price { get; set; }
    }
}
