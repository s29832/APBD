namespace Cwiczenia_2.Model.Users;

public class Student : User
{
    public override int MaxLimit => 2;
    public Student(string name, string lastName) : base(name, lastName, UserType.Student)
    {
    }
}