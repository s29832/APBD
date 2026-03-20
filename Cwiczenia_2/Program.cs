using System;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Users;
using Cwiczenia_2.Service;

namespace Cwiczenia_2;

class Program
{
    public static void Main(string[] args)
    {
        var rentalService = new RentalService();

        var thinkPad = new Laptop("ThinkPad T14", "Intel Core i5-1135G7", "16GB");
        var macBook = new Laptop("MacBook Pro 16", "Apple M1 Pro", "16GB");
        var aparatCanon = new Camera("Canon EOS 80D", "24", "18-55mm f/3.5-5.6");
        
        var student1 = new Student("Jan","Kowalski");
        var worker1 = new Employee("Piotr","Nowak");
        
        Console.WriteLine("^^^ SYSTEM WYPOŻYCZALNI ^^^");
    }
}