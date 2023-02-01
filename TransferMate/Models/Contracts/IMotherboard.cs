namespace TransferMate.Models.Contracts
{
    public interface IMotherboard
    {
        string ComponentType { get; set; }
        string PartNumber { get; set; }
        string Name { get; set; }
        string Socket { get; set; }
        double Price { get; set; }

    }
}