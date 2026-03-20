namespace Cwiczenia_2.Model.Equipments;

public class Projector : Equipment
{
    public string Brand { get; private set; }
    
    public string Model { get; private set; }

    public Projector(string name, string brand, string model) : base(name)
    {
        Brand = brand;
        Model = model;
    }
}