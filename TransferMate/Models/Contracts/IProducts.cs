namespace TransferMate.Models.Contracts
{
    public interface IProducts
    {
        ICollection<CPU> CPUs { get; set; }
        ICollection<Motherboard> Motherboards { get; set; }
        ICollection<Memory> Memories { get; set; }
    }
}