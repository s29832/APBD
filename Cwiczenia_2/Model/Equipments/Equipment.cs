using System;

namespace Cwiczenia_2.Model.Equipments;

public abstract class Equipment
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsAvailable { get; set; }

    protected Equipment(string name)
    {
        Name = name;
        IsAvailable = true; //domyslnie jest dostepny
        Id = Guid.NewGuid();
    }
    public void SetUnavailable()
    {
        IsAvailable = false;
    }
    public void SetAvailable()
    {
        IsAvailable = true;
    }
    public override string ToString()
    {
        string status = IsAvailable ? "Available" : "Unavailable";
        return $"{Name} - {status}";
    }
}