using Newtonsoft.Json;
using TransferMate.Models.Contracts;

namespace TransferMate.Models
{
    public class Products  : IProducts
    {
        [JsonProperty("CPUs")]
        public ICollection<CPU> CPUs { get; set; }

        [JsonProperty("Motherboards")]
        public ICollection<Motherboard> Motherboards { get; set; }

        [JsonProperty("Memory")]
        public ICollection<Memory> Memories { get; set; }
    }
}

