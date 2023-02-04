using Newtonsoft.Json;
using System.Text;
using TransferMate.Models;
using TransferMate.Models.Contracts;

string text = File.ReadAllText("pc-store-inventory.json");
var data = JsonConvert.DeserializeObject<Products>(text);
var products = new List<IBaseData>();
StringBuilder builder = new StringBuilder();
var compatibleCpus = new List<IBaseData>();
var compatibleMemories = new List<IBaseData>();
var compatibleMotherboards = new List<IBaseData>();

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
        var cpu = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(CPU).IsAssignableFrom(x.GetType()));
        var motherboard = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(Motherboard).IsAssignableFrom(x.GetType()));
        var memory = products.FirstOrDefault(x => inputLine.Any(y => y == x.PartNumber) && typeof(Memory).IsAssignableFrom(x.GetType()));

        if (cpu != null)
        {
            compatibleCpus.Add(cpu);
            if (motherboard == null)
            {
                compatibleMotherboards = FindMotherboard(cpu.ComponentType);
            }
            if (memory == null)
            {
                compatibleMemories = FindMemory(cpu.SupportedMemory);
            }
        }
        else
        {
            compatibleCpus = FindCpu(memory?.Type, motherboard?.Type);
            if (motherboard == null)
            {
                compatibleMotherboards = FindMotherboard(cpu.ComponentType);
            }
            if (memory == null)
            {
                compatibleMemories = FindMemory(cpu.SupportedMemory);
            }
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
static List<IBaseData> FindCpu(string memoryType = null, string motherboardType = null)
{
    var input = new List<CPU>();
    if (memoryType != null && motherboardType != null)
    {
        if (memoryType.Equals(""))
        {

        }
        else
        {
            throw new Exception("ERROR: Please choose different component types");
        }
    }
    else if (memoryType != null)
    {

    }
    else if (motherboardType != null)
    {

    }
    else
    {

    }
    //var test = (typeof(CPU)).IsAssignableFrom(products[0].GetType());
    return null;
}
static List<IBaseData> FindMemory(string memoryType = null)
{
    if (memoryType != null)
    {

    }
    //var test = (typeof(Memory)).IsAssignableFrom(products[0].GetType());
    List<Memory> memoryList = new List<Memory>();
    return null;
}
static List<IBaseData> FindMotherboard(string motherboardType = null)
{
    if (motherboardType != null)
    {

    }
    //var test = (typeof(Motherboard)).IsAssignableFrom(products[0].GetType());
    List<Motherboard> motherboardList = new List<Motherboard>();
    return null;
}








