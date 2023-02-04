namespace TransferMate.Models.Contracts
{
    public interface IBaseData
    {
        string ComponentType { get; set; }
        string PartNumber { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        string SupportedMemory { get; set; }
        string Socket { get; set; }
        string Type { get; set; }
    }
}
