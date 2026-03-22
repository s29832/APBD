using System;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Users;
using Cwiczenia_2.Service;

namespace Cwiczenia_2.Interface;

public class Interface
{
    private readonly RentServ rentalService;
    private readonly EqServ equipmentService;
    private readonly UserServ userService;

    public Interface(RentServ rentalService, EqServ equipmentService, UserServ userService)
    {
        this.rentalService = rentalService;
        this.equipmentService = equipmentService;
        this.userService = userService;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n MENU");
            Console.WriteLine("1. Add User | 2. Add Equipment | 3. All Equipment | 4. Available Equipment");
            Console.WriteLine("5. Rent | 6. Return | 7. Mark Unavailable | 8. Active User Rentals");
            Console.WriteLine("9. Overdue Rentals | 10. Summary Report | 0. Exit");
            
            string choice = ReadText("\nChoose option ");
            Console.Clear(); 

            switch (choice)
            {
                case "1": AddNewUser(); break;
                case "2": AddNewEquipment(); break;
                case "3": DisplayAllEquipment(); break;
                case "4": DisplayAvailableEquipment(); break;
                case "5": RentEquipment(); break;
                case "6": ReturnEquipment(); break;
                case "7": MarkEquipmentUnavailable(); break;
                case "8": DisplayUserActiveRentals(); break;
                case "9": DisplayOverdueRentals(); break;
                case "10": DisplaySummaryReport(); break;
                case "0": return; 
                default: Console.WriteLine("Unknown option."); break;
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void AddNewUser()
    {
        string type = ReadText("Student or Employee? (s/e)");
        string fn = ReadText("First name"), ln = ReadText("Last name");

        if (type == "s")
        {
            var u = new Student(fn, ln); userService.AddUser(u); Console.WriteLine($"\n✅ Student added! ID: {u.Id.ToString().Substring(0,8)}");
        }
        else if (type == "e")
        {
            var u = new Employee(fn, ln); userService.AddUser(u); Console.WriteLine($"\n✅ Employee added! ID: {u.Id.ToString().Substring(0,8)}");
        }
        else Console.WriteLine("\n Error: Unknown type.");
    }

    private void AddNewEquipment()
    {
        string type = ReadText("Equipment type (laptop/projector/camera)");
        string name = ReadText("Name");

        if (type == "laptop") { var eq = new Laptop(name, ReadText("Processor"), ReadText("RAM")); equipmentService.AddEquipment(eq); Console.WriteLine($"\n✅ Laptop added! ID: {eq.Id.ToString().Substring(0,8)}"); }
        else if (type == "projector")
        {
            var eq = new Projector(name, ReadText("Brand"), ReadText("Model")); equipmentService.AddEquipment(eq); Console.WriteLine($"\n✅ Projector added! ID: {eq.Id.ToString().Substring(0,8)}");
        }
        else if (type == "camera")
        {
            var eq = new Camera(name, ReadText("Megapixels"), ReadText("Aperture")); equipmentService.AddEquipment(eq); Console.WriteLine($"\n✅ Camera added! ID: {eq.Id.ToString().Substring(0,8)}");
        }
        else Console.WriteLine("\n Error: Unknown equipment type.");
    }

    private void DisplayAllEquipment()
    {
        var eqList = equipmentService.GetAllEquipments();
        if (eqList.Count == 0)
        {
            Console.WriteLine("No equipment found."); return;
        }
        Console.WriteLine("\n--- ALL EQUIPMENT ---");
        foreach (var eq in eqList) Console.WriteLine($"[ID: {eq.Id.ToString().Substring(0,8)}] {eq}");
    }

    private void DisplayAvailableEquipment()
    {
        var eqList = equipmentService.GetAvailableEquipments();
        if (eqList.Count == 0)
        {
            Console.WriteLine("No available equipment."); return;
        }
        Console.WriteLine("\n--- AVAILABLE EQUIPMENT ---");
        foreach (var eq in eqList) Console.WriteLine($"[ID: {eq.Id.ToString().Substring(0,8)}] {eq}");
    }

    private void RentEquipment()
    {
        string uId = ReadShortId("\nEnter user ID", "user");
        if (uId == null) return;
        var u = userService.GetUserById(uId);
        if (u == null) { Console.WriteLine("User not found."); return; }

        string eqId = ReadShortId("Enter equipment ID", "equipment");
        if (eqId == null) return;
        var eq = equipmentService.GetEquipmentById(eqId);
        if (eq == null) { Console.WriteLine("Equipment not found."); return; }

        int? days = ReadInt("For how many days?");
        if (days == null) return;

        try
        {
            rentalService.RentEquipment(u, eq, days.Value); Console.WriteLine("Equipment rented!");
        }
        catch (Exception e) { Console.WriteLine($"ERROR: {e.Message}"); }
    }

    private void ReturnEquipment()
    {
        string rId = ReadShortId("\nEnter rental ID", "rental");
        if (rId == null) return;
        DateTime? d = ReadDate("Return date (yyyy-MM-dd)");
        if (d == null) return;

        try 
        { 
            var rental = rentalService.GetRentalById(rId); 
            
            rentalService.ReturnEquipment(rId, d.Value); 
            if (rental.Punishment > 0)
                Console.WriteLine($"\n⚠️ Equipment returned overdue! Penalty applied: {rental.Punishment} PLN");
            else
                Console.WriteLine("\n✅ Equipment returned on time! No penalty.");
        }
        catch (Exception e) { Console.WriteLine($"ERROR: {e.Message}"); }
    }

    private void MarkEquipmentUnavailable()
    {
        string eqId = ReadShortId("\nEnter equipment ID", "equipment");
        if (eqId == null) return;
        var eq = equipmentService.GetEquipmentById(eqId);
        if (eq == null)
        {
            Console.WriteLine("Equipment not found."); return;
        }
        equipmentService.SetUnavailable(eqId); Console.WriteLine($"Marked as unavailable.");
    }

    private void DisplayUserActiveRentals()
    {
        string uId = ReadShortId("\nEnter user ID", "user");
        if (uId == null) return;
        var u = userService.GetUserById(uId);
        if (u == null)
        {
            Console.WriteLine("User not found."); return;
        }
        
        var rents = rentalService.GetActiveByUser(u);
        if (rents.Count == 0)
        {
            Console.WriteLine("No active rentals."); return;
        }
        Console.WriteLine($"\n--- ACTIVE RENTALS ---");
        foreach (var r in rents) Console.WriteLine(r);
    }

    private void DisplayOverdueRentals()
    {
        var overdue = rentalService.GetExpiredRentals();
        if (overdue.Count == 0)
        {
            Console.WriteLine("No overdue rentals!"); return;
        }
        Console.WriteLine("\n--- OVERDUE RENTALS ---");
        foreach (var r in overdue) Console.WriteLine(r);
    }

    private void DisplaySummaryReport() => Console.WriteLine(rentalService.GetSummary());

    private void ShowUsersList()
    {
        var users = userService.GetAllUsers();
        if (users.Count == 0) return;
        Console.WriteLine("\n--- USERS ---");
        foreach (var u in users) Console.WriteLine($"[ID: {u.Id.ToString().Substring(0,8)}] {u.Name} {u.LastName}");
    }
    private void ShowEquipmentList()
    {
        var eqs = equipmentService.GetAllEquipments();
        if (eqs.Count == 0) return;
        Console.WriteLine("\n--- EQUIPMENT ---");
        foreach (var eq in eqs) Console.WriteLine($"[ID: {eq.Id.ToString().Substring(0,8)}] {eq.Name}");
    }
    private void ShowRentalsList()
    {
        var rents = rentalService.GetRentals();
        if (rents.Count == 0) return;
        Console.WriteLine("\n--- RENTALS ---");
        foreach (var r in rents) Console.WriteLine($"[ID: {r.Id.ToString().Substring(0,8)}] {r.Equipment.Name} -> {r.User.Name}");
    }

    private string ReadText(string prompt) { Console.Write($"{prompt}: "); return Console.ReadLine()?.Trim(); }
    private string ReadShortId(string prompt, string listType = "")
    {
        while (true)
        {
            string help = listType != "" ? ", ? = list" : "";
            string input = ReadText($"{prompt} (q=cancel{help})");
            if (input == "q") return null;
            if (input == "?" && listType != "") {
                if (listType == "user") ShowUsersList();
                else if (listType == "equipment") ShowEquipmentList();
                else if (listType == "rental") ShowRentalsList();
                continue;
            }
            if (!string.IsNullOrEmpty(input)) return input; 
            Console.WriteLine("ID cannot be empty.");
        }
    }
    private DateTime? ReadDate(string prompt)
    {
        while (true) {
            string input = ReadText($"{prompt} (q=cancel)");
            if (input == "q") return null;
            if (DateTime.TryParse(input, out DateTime r)) return r;
            Console.WriteLine("Invalid format (yyyy-MM-dd).");
        }
    }
    private int? ReadInt(string prompt)
    {
        while (true) {
            string input = ReadText($"{prompt} (q=cancel)");
            if (input == "q") return null;
            if (int.TryParse(input, out int r)) return r;
            Console.WriteLine("Please enter an integer.");
        }
    }
}