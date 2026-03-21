using System;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Users;
using Cwiczenia_2.Service;

namespace Cwiczenia_2;

class Program
{
    public static void Main(string[] args)
    {
        
        Console.WriteLine("^^^ RENTAL SERVICE SYS ^^^");
        var rentalService = new RentalService();

        var thinkPad = new Laptop("ThinkPad T14", "Intel Core i5-1135G7", "16GB");
        var macBook = new Laptop("MacBook Pro 16", "Apple M1 Pro", "16GB");
        var aparatCanon = new Camera("Canon EOS 80D", "24", "18-55mm f/3.5-5.6");
        var projectorLG = new Projector("LG 4K", "4K UHD", "1080p");
        
        var student1 = new Student("Jan","Kowalski");
        var worker1 = new Employee("Piotr","Nowak");
        
        rentalService.Rent(student1, thinkPad,16);
        rentalService.Rent(worker1, aparatCanon,12);
        rentalService.Rent(student1, macBook,12);
        
        rentalService.Rent(student1, projectorLG ,16);
        
        rentalService.Rent(worker1, thinkPad ,16);
        
        rentalService.Return(student1,thinkPad, DateTime.Now.AddDays(17));
        
        Console.WriteLine("SERVICE CLOSED");
    }
}