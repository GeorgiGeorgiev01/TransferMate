using Newtonsoft.Json;
using System.Text;
using TransferMate.Models;
using TransferMate.Models.Contracts;

string text = File.ReadAllText("pc-store-inventory.json");
var data = JsonConvert.DeserializeObject<Products>(text);

var products = new List<IBaseData>();

if (data != null)
{
    products.AddRange(data.Memories);
    products.AddRange(data.Motherboards);
    products.AddRange(data.CPUs);
}

const string TerminationCommand = "Exit";
const string EmptyCommandError = "Command cannot be empty.";

while (true)
{
    try
    {
        short combinationCount = 1;
        var compatibleCpus = new List<IBaseData>();
        var compatibleMemories = new List<IBaseData>();
        var compatibleMotherboards = new List<IBaseData>();

        var inputLine = Console.ReadLine().Split(',');

        if (!inputLine.Any())
        {
            Console.WriteLine(EmptyCommandError);
            continue;
        }
        if (inputLine[0].Equals(TerminationCommand, StringComparison.InvariantCultureIgnoreCase))
        {
            break;
        }
        if (products.Count(x => inputLine.Any(y => y == x.PartNumber) && typeof(CPU).IsAssignableFrom(x.GetType())) > 1 || products.Count(x => inputLine.Any(y => y == x.PartNumber) && typeof(Memory).IsAssignableFrom(x.GetType())) > 1 || products.Count(x => inputLine.Any(y => y == x.PartNumber) && typeof(Motherboard).IsAssignableFrom(x.GetType())) > 1)
        {
            Console.WriteLine("Please choose different component types");
            continue;
        }
        if (!products.Any(x => inputLine.Any(y => y == x.PartNumber) && typeof(CPU).IsAssignableFrom(x.GetType())) && !products.Any(x => inputLine.Any(y => y == x.PartNumber) && typeof(Memory).IsAssignableFrom(x.GetType())) && !products.Any(x => inputLine.Any(y => y == x.PartNumber) && typeof(Motherboard).IsAssignableFrom(x.GetType())))
        {
            Console.WriteLine("No components found with the entered Part Number/s");
            continue;
        }
        var cpu = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(CPU).IsAssignableFrom(x.GetType()));
        var motherboard = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(Motherboard).IsAssignableFrom(x.GetType()));
        var memory = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(Memory).IsAssignableFrom(x.GetType()));
        var totalCombinations = new List<IBaseData>();
        if (cpu == null)
        {
            totalCombinations = products.Where(x => ((motherboard != null && memory != null && x.Socket == motherboard.Socket && typeof(CPU).IsAssignableFrom(x.GetType()) && x.SupportedMemory == memory.Type)
        ||
        (motherboard == null && memory != null && typeof(CPU).IsAssignableFrom(x.GetType()) && x.SupportedMemory == memory.Type)
        ||
        (motherboard != null && memory == null && typeof(CPU).IsAssignableFrom(x.GetType()) && x.Socket == motherboard.Socket)
        )).ToList();
        }
        else
        {
            int counter = 1;
            if (motherboard != null || memory != null)
            {
                if (memory != null && memory.Type != cpu.SupportedMemory || motherboard != null && motherboard.Socket != cpu.Socket)
                {
                    Console.WriteLine("ERROR: The selected configuration is not valid.");
                }
                if (memory != null && memory.Type != cpu.SupportedMemory)
                {
                    Console.WriteLine($"{counter}. Memory of type {memory.Type} is not compatible with the CPU");
                    counter++;
                }
                if (motherboard != null && motherboard.Socket != cpu.Socket)
                {
                    Console.WriteLine($"{counter}. Motherboard with Socket {motherboard.Socket} is not compatible with the CPU");
                }
                if (memory != null && memory.Type != cpu.SupportedMemory || motherboard != null && motherboard.Socket != cpu.Socket)
                {
                    continue;
                }
            }

            totalCombinations = products.Where(x => (
            (motherboard == null && x.Socket == cpu.Socket && typeof(Motherboard).IsAssignableFrom(x.GetType()))
        || (memory == null && x.Type == cpu.SupportedMemory && typeof(Memory).IsAssignableFrom(x.GetType())))
            ).ToList();
        }

        if (totalCombinations.Any(x => typeof(CPU).IsAssignableFrom(x.GetType())))
        {
            if (memory != null && motherboard != null)
            {

            }
            else
            {
                var count = 0;

                foreach (var cpuCombination in totalCombinations)
                {
                    count += products.Where(x => (
                (motherboard == null && x.Socket == cpuCombination.Socket && typeof(Motherboard).IsAssignableFrom(x.GetType()))
            || (memory == null && x.Type == cpuCombination.SupportedMemory && typeof(Memory).IsAssignableFrom(x.GetType())))
                ).Count();
                }

                Console.WriteLine($"Total Combinations: {count}");
            }

        }
        else if (cpu != null && memory != null && motherboard != null)
        {
            Console.WriteLine($"This is a valid combination.");
            continue;
        }
        else
        {
            Console.WriteLine($"Total Combinations: {totalCombinations.Count}");
        }

        if (memory != null && motherboard != null && cpu != null)
        {
            Console.WriteLine("This is a valid combination");
            continue;
        }

        if (cpu != null)
        {
            compatibleCpus.Add(cpu);

            foreach (var compatibleCpu in compatibleCpus)
            {
                var tempMotherBoards = new List<IBaseData>();
                var tempMemories = new List<IBaseData>();
                if (motherboard == null)
                {
                    tempMotherBoards = FindMotherboard(products, compatibleCpu.Socket);
                }
                else
                {
                    tempMotherBoards.Add(motherboard);
                }
                if (memory == null)
                {
                    tempMemories = FindMemory(products, compatibleCpu.SupportedMemory);
                }
                else
                {
                    tempMemories.Add(memory);
                }

                PrintData(compatibleCpu, tempMotherBoards, tempMemories, ref combinationCount);
            }
        }
        else
        {
            compatibleCpus = FindCpu(products, memory?.Type, motherboard?.Socket);
            foreach (var compatibleCpu in compatibleCpus)
            {
                var tempMotherBoards = new List<IBaseData>();
                var tempMemories = new List<IBaseData>();
                if (motherboard == null)
                {
                    tempMotherBoards = FindMotherboard(products, compatibleCpu.Socket);
                }
                else
                {
                    tempMotherBoards.Add(motherboard);
                }
                if (memory == null)
                {
                    tempMemories = FindMemory(products, compatibleCpu.SupportedMemory);
                }
                else
                {
                    tempMemories.Add(memory);
                }

                PrintData(compatibleCpu, tempMotherBoards, tempMemories, ref combinationCount);
            }
            combinationCount = 1;
        }

    }
    catch (Exception ex)
    {
        if (!string.IsNullOrEmpty(ex.Message))
        {
            Console.WriteLine(ex.Message);
        }
        else
        {
            Console.WriteLine(ex);
        }
    }
}
static List<IBaseData> FindCpu(List<IBaseData> products, string? memoryType, string? motherboardType)
{
    var cpus = new List<IBaseData>();

    cpus = products.Where(x => typeof(CPU).IsAssignableFrom(x.GetType()) &&
    (motherboardType == null || x.Socket == motherboardType)
    && (memoryType == null || x.SupportedMemory == memoryType)).ToList();
    return cpus;


}
static List<IBaseData> FindMemory(List<IBaseData> products, string? memoryType)
{
    var memories = new List<IBaseData>();

    memories = products.Where(x => typeof(Memory).IsAssignableFrom(x.GetType()) &&
    (memoryType == null || x.Type == memoryType)).ToList();
    return memories;
}
static List<IBaseData> FindMotherboard(List<IBaseData> products, string? motherboardType)
{
    var motherboards = new List<IBaseData>();

    motherboards = products.Where(x => typeof(Motherboard).IsAssignableFrom(x.GetType()) &&
    (motherboardType == null || x.Socket == motherboardType)).ToList();
    return motherboards;
}

static void PrintData(IBaseData cpu, List<IBaseData> motherboards, List<IBaseData> memories, ref short combinationCount)
{
    foreach (var motherboard in motherboards)
    {
        foreach (var memory in memories)
        {
            Console.WriteLine($"Combination {combinationCount}");
            combinationCount++;
            Console.WriteLine($"CPU - {cpu.Name} - {cpu.Socket}, {cpu.SupportedMemory}");
            Console.WriteLine($"Motherboard - {motherboard.Name} - {motherboard.Socket}");
            Console.WriteLine($"Memory - {memory.Name} - {memory.Type}");
            Console.WriteLine($"Price: {cpu.Price + motherboard.Price + memory.Price}");
        }
    }
}







