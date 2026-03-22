using System;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Users;
using Cwiczenia_2.Service;

namespace Cwiczenia_2;

class Program
{
    public static void Main(string[] args) {
        var equipmentService = new EqServ();
        var userService = new UserServ();
        var rentalService = new RentServ(equipmentService);
        
        Console.WriteLine("Welcome to the rental system!");
        
        equipmentService.AddEquipment(new Laptop("ThinkPad T14", "Intel Core i5", "16GB"));
        equipmentService.AddEquipment(new Laptop("MacBook Pro 16", "Apple M1 Pro", "32GB"));
        equipmentService.AddEquipment(new Laptop("Dell XPS 15", "Intel Core i7", "16GB"));
        equipmentService.AddEquipment(new Laptop("Asus ROG Zephyrus", "AMD Ryzen 9", "32GB"));
        equipmentService.AddEquipment(new Camera("Canon EOS 80D", "24 MP", "f/3.5-5.6"));
        equipmentService.AddEquipment(new Camera("Sony Alpha a7 III", "24.2 MP", "f/2.8"));
        equipmentService.AddEquipment(new Camera("Nikon Z6 II", "24.5 MP", "f/4.0"));
        equipmentService.AddEquipment(new Projector("LG 4K UHD", "LG", "HU70LA"));
        equipmentService.AddEquipment(new Projector("Epson Home Cinema", "Epson", "2250"));
        equipmentService.AddEquipment(new Projector("BenQ 1080p", "BenQ", "HT2050A"));
        
        userService.AddUser(new Student("Jan", "Kowalski"));
        userService.AddUser(new Student("Anna", "Nowak"));
        userService.AddUser(new Student("Piotr", "Wiśniewski"));
        userService.AddUser(new Student("Katarzyna", "Wójcik"));
        userService.AddUser(new Employee("Michał", "Kamiński"));
        userService.AddUser(new Employee("Agnieszka", "Lewandowska"));
        userService.AddUser(new Employee("Tomasz", "Zieliński"));
        
        var interf = new Interface.Interface(rentalService, equipmentService, userService);
        interf.Run();
    }
}