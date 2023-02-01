using TransferMate.Models.Contracts;

namespace TransferMate.Models
{
    public class Motherboard : IMotherboard
    {

        public string ComponentType { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string Socket { get; set; }
        public double Price { get; set; }
    }
}
