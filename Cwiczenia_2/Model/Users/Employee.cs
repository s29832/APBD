namespace Cwiczenia_2.Model.Users;

public class Employee : User
{
    public override int MaxLimit => 5;
    public Employee(string name, string lastName) : base(name, lastName, UserType.Employee)
    {
    }
}