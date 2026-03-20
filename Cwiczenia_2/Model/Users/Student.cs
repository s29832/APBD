namespace Cwiczenia_2.Model.Users;

public class Student : User
{
    public Student(string name, string lastName) : base(name, lastName, UserType.Student)
    {
    }
}