using System;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Users;
using Cwiczenia_2.Service;

namespace Cwiczenia_2;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("^^^ Uruchamianie RENTAL SERVICE SYS ^^^");
    
        
        var equipmentService = new EqServ();
        var userService = new UserServ();
        var rentalService = new RentalService(equipmentService);
        
        equipmentService.AddEquipment(new Laptop("ThinkPad T14", "Intel Core i5", "16GB"));
        userService.AddUser(new Student("Jan", "Kowalski"));
    }
}