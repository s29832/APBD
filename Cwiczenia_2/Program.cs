using System;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Users;
using Cwiczenia_2.Service;

namespace Cwiczenia_2;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("^^^ LOADING RENTAL SERVICE SYS ^^^");
        var service = new RentalService();
        
        service.AddEquipment(new Laptop("ThinkPad T14", "Intel Core i5", "16GB"));
        service.AddEquipment(new Laptop("MacBook Pro 16", "Apple M1 Pro", "16GB"));
        service.AddEquipment(new Camera("Canon EOS 80D", "24", "18-55mm f/3.5-5.6"));
        service.AddEquipment(new Projector("LG 4K", "LG", "UHD 1080p"));
        
        service.AddUser(new Student("Jan", "Kowalski"));
        service.AddUser(new Employee("Piotr", "Nowak"));
        
        
        Console.WriteLine("SERVICE CLOSED");
    }
}