using TransferMate.Models.Contracts;

namespace TransferMate.Models
{
    public class CPU : ICPU
    {

        public string ComponentType { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string SupportedMemory { get; set; }
        public string Socket { get; set; }
        public double Price { get; set; }
    }
}
