namespace Cwiczenia_2.Model.Equipments;

public class Laptop : Equipment
{
    public string Processor { get; private set; }
    public string RamSize { get; private set; }
    
    public Laptop(string name, string processor, string ramSize) : base(name)
    {
        Processor = processor;
        RamSize = ramSize;
    }
}