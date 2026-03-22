using System;

namespace Cwiczenia_2.Model.Users;

public abstract class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public UserType UserType { get; private set; }
    
    public abstract int MaxLimit { get; }
    
    protected User(string name, string lastName, UserType userType)
    {
        Name = name;
        LastName = lastName;
        Id = Guid.NewGuid();
        UserType = userType;
    }

    public override string ToString()
    {
        return $"{Name} {LastName} [Limit sprzetu: {MaxLimit}]";
    }
}