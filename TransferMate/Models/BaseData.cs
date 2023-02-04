
using TransferMate.Models.Contracts;

namespace TransferMate.Models
{
    public class BaseData : IBaseData
    {
        public virtual string ComponentType { get; set; }
        public virtual string PartNumber { get; set; }
        public virtual string Name { get; set; }
        public virtual double Price { get; set; }
        public virtual string SupportedMemory { get; set; }
        public virtual string Socket { get; set; }
        public virtual string Type { get; set; }

    }
}
