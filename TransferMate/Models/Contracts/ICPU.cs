namespace TransferMate.Models.Contracts
{
    interface ICPU
    {
        string SupportedMemory { get; set; }
        string Socket { get; set; }
    }
}