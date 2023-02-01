namespace TransferMate.Models.Contracts
{
    interface ICPU
    {
        string ComponentType { get; set; }
        string PartNumber { get; set; }
        string Name { get; set; }
        string SupportedMemory { get; set; }
        string Socket { get; set; }
        double Price { get; set; }
    }
}