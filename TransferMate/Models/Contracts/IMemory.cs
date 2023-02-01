namespace TransferMate.Models.Contracts
{
    public interface IMemory
    {
        string ComponentType { get; set; }
        string PartNumber { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        double Price { get; set; }
    }
}