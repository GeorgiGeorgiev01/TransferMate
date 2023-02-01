using TransferMate.Models.Contracts;

namespace TransferMate.Models
{
    public class Memory : IMemory
    {

        public string ComponentType { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
    }
}
