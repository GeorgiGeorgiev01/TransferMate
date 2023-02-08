using Newtonsoft.Json;
using TransferMate.Models;
using TransferMate.Models.Contracts;

string text = File.ReadAllText("pc-store-inventory.json");
var data = JsonConvert.DeserializeObject<Products>(text);
const string TerminationCommand = "Exit"; //Terminates the program when "exit" is typed. Case insensitive
var products = new List<IBaseData>();

if (data != null)
{
    products.AddRange(data.Memories);
    products.AddRange(data.Motherboards);
    products.AddRange(data.CPUs);
}

/// <summary>
///  Main logic for calculating. 
///  The algorithm is based on IF the user selects a CPU, if he does the program calculates all the possible parts. 
///  If not, algorithm finds the first CPU and does the calculation from there.
/// </summary>
/// <param input="string"></param>
/// <returns>All possible combinations with the inputed part numbers. </returns>

//Main logic
while (true)
    
{
    Console.WriteLine("Please enter part number(s):");
    try
    {
        int? combinationCount = 1;
        var compatibleCpus = new List<IBaseData>();
        var compatibleMemories = new List<IBaseData>();
        var compatibleMotherboards = new List<IBaseData>();
        var input = Console.ReadLine().Replace(" ", String.Empty);

        //Exception if empty string is entered
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Command can not be empty!");
            continue;
        }
        var inputLine = input.Split(',');

        //If Termination command - Exit, is inputed - terminates the program
        if (inputLine[0].Equals(TerminationCommand, StringComparison.InvariantCultureIgnoreCase))
        {
            break;
        }

        //If the entered CPU, Memory and Motherboard are valid, BUT no avaliable combinations are found
        if (products.Count(x => inputLine.Any(y => y == x.PartNumber) && typeof(CPU).IsAssignableFrom(x.GetType())) > 1
            || products.Count(x => inputLine.Any(y => y == x.PartNumber) && typeof(Memory).IsAssignableFrom(x.GetType())) > 1
            || products.Count(x => inputLine.Any(y => y == x.PartNumber) && typeof(Motherboard).IsAssignableFrom(x.GetType())) > 1)
        {
            Console.WriteLine("Please choose different component types");
            continue;
        }

        if (!products.Any(x => inputLine.Any(y => y == x.PartNumber) && typeof(CPU).IsAssignableFrom(x.GetType()))
            && !products.Any(x => inputLine.Any(y => y == x.PartNumber) && typeof(Memory).IsAssignableFrom(x.GetType()))
            && !products.Any(x => inputLine.Any(y => y == x.PartNumber) && typeof(Motherboard).IsAssignableFrom(x.GetType())))
        {
            Console.WriteLine("No components found with the entered Part Number/s");
            continue;
        }

        var cpu = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(CPU).IsAssignableFrom(x.GetType()));
        var motherboard = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(Motherboard).IsAssignableFrom(x.GetType()));
        var memory = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(Memory).IsAssignableFrom(x.GetType()));
        var totalCombinations = new List<IBaseData>();

        totalCombinations = FindCombinations(cpu, motherboard, memory, products, ref totalCombinations);
        if (totalCombinations == null)
        {
            continue;
        }

        combinationCount = CalculateCombinations(cpu, motherboard, memory, products, ref totalCombinations);
        if (combinationCount == null)
        {
            continue;
        }

        PrintingCombinations(cpu, motherboard, memory, combinationCount, products);
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
//End of Main Logic


/// <summary>
///  Parses the input to return the CPU, based on the part number.
/// </summary>
/// <param input="string"></param>
/// <returns> Returns the first found CPU. </returns>
static List<IBaseData> FindCpu(List<IBaseData> products, string? memoryType, string? motherboardType)
{
    var cpus = new List<IBaseData>();

    cpus = products.Where(x => typeof(CPU).IsAssignableFrom(x.GetType()) &&
    (motherboardType == null || x.Socket == motherboardType)
    && (memoryType == null || x.SupportedMemory == memoryType)).ToList();
    return cpus;
}

/// <summary>
///  Parses the input to return Memoty, based on the part number.
/// </summary>
/// <param input="string"></param>
/// <returns> Returns the first found Memory. </returns>
static List<IBaseData> FindMemory(List<IBaseData> products, string? memoryType)
{
    var memories = new List<IBaseData>();

    memories = products.Where(x => typeof(Memory).IsAssignableFrom(x.GetType()) &&
    (memoryType == null || x.Type == memoryType)).ToList();
    return memories;
}

/// <summary>
///  Parses the input to return the Motherboard, based on the part number.
/// </summary>
/// <param input="string"></param>
/// <returns> Returns the first found Motherboard. </returns>
static List<IBaseData> FindMotherboard(List<IBaseData> products, string? motherboardType)
{
    var motherboards = new List<IBaseData>();

    motherboards = products.Where(x => typeof(Motherboard).IsAssignableFrom(x.GetType()) &&
    (motherboardType == null || x.Socket == motherboardType)).ToList();
    return motherboards;
}

/// <summary>
///  Prints all the combinations, based on the inputed part numbers.
/// </summary>
/// <param input="string"></param>
/// <returns> Prints all the combinations. </returns>
static void PrintData(IBaseData cpu, List<IBaseData> motherboards, List<IBaseData> memories, ref int? combinationCount)
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

/// <summary>
///  Finds all combinations, based on the inputed part numbers.
/// </summary>
/// <param input="string"></param>
/// <returns> All combinations. </returns>
static List<IBaseData> FindCombinations(IBaseData cpu, IBaseData motherboard, IBaseData memory, List<IBaseData> products, ref List<IBaseData> totalCombinations)
{
    //If a CPU part number is not entered
    if (cpu == null)
    {
        return totalCombinations = products.Where(x => ((motherboard != null && memory != null && x.Socket == motherboard.Socket && typeof(CPU).IsAssignableFrom(x.GetType()) && x.SupportedMemory == memory.Type)
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
                return null;
            }
        }
        return totalCombinations = products.Where(x => (
        (motherboard == null && x.Socket == cpu.Socket && typeof(Motherboard).IsAssignableFrom(x.GetType()))
            || (memory == null && x.Type == cpu.SupportedMemory && typeof(Memory).IsAssignableFrom(x.GetType())))
                ).ToList();
    }
}

/// <summary>
///  Calculates the combinations based on input
/// </summary>
/// <param input="string"></param>
/// <returns> All combinations. </returns>
static int? CalculateCombinations(IBaseData cpu,  IBaseData motherboard, IBaseData memory, List<IBaseData> products, ref List<IBaseData> totalCombinations)
{
    if (totalCombinations.Any(x => typeof(CPU).IsAssignableFrom(x.GetType())))
    {
        if (memory == null || motherboard == null)
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
            return count;
        }
        else
        {
            Console.WriteLine($"Total Combinations: {totalCombinations.Count}");
            return totalCombinations.Count();
        }
    }

    //If input is a valid combination
    else if (cpu != null && memory != null && motherboard != null)
    {
        Console.WriteLine($"This is a valid combination.");
        return null;
    }
    else
    {
        Console.WriteLine($"Total Combinations: {totalCombinations.Count}");
        return totalCombinations.Count();
    }

    //If input is a valid combination
    if (memory != null && motherboard != null && cpu != null)
    {
        Console.WriteLine("This is a valid combination");
        return null;
    }
    return null;
}

/// <summary>
///  Calculates the printing combinations based on input
/// </summary>
/// <param input="string"></param>
/// <returns> All combinations. </returns>
static void PrintingCombinations(IBaseData cpu, IBaseData motherboard, IBaseData memory, int? combinationCount, List<IBaseData> products)
{

    var compatibleCpus = new List<IBaseData>();
    if (cpu != null)
    {
        compatibleCpus.Add(cpu);
        combinationCount = 1;
        foreach (var compatibleCpu in compatibleCpus)
        {
            //In order to save the list and not print it again, we need to temp. save it
            //Otherwise with the same input the count will continue
            //(If 2 combinations are printed, if you input the same data, count will start from 3, then 4, after that - 5,6 etc.)
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
        //Reseting the counter to 1, otherwise the next input combination will not start from 1
        combinationCount = 1;
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
        //Reseting the counter to 1, otherwise the next input combination will not start from 1
        combinationCount = 1;
    }
}








