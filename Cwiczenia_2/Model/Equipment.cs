using System;

namespace Cwiczenia_2.Model;

public abstract class Equipment
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsAvailable { get; private set; }

    protected Equipment(string name)
    {
        Name = name;
        IsAvailable = true;
        Id = Guid.NewGuid();
    }
}