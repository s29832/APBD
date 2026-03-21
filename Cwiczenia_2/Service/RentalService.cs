using System;
using System.Collections.Generic;
using System.Linq;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Rentals;
using Cwiczenia_2.Model.Users;

namespace Cwiczenia_2.Service;

public class RentalService
{
    private readonly List<Rental> rentals = new();
    private readonly List<Equipment> equipments = new();

    public void Rent(User user, Equipment equipment, int days)
    {
        if (!equipment.IsAvailable)
        {
            Console.WriteLine($" {equipment.Name} is not available!");
            return;
        }

        int activeRent = rentals.Count(rental => rental.User == user && rental.ReturnDateReally == null);

        if (activeRent >= user.MaxLimit)
        {
            Console.WriteLine($"{user.Name} has reached the limit of rentals!");
            return;
        }

        var newRental = new Rental(user, equipment, days);
        rentals.Add(newRental);
        equipment.setUnavailable();
        Console.WriteLine("Equipment rented!");

    }

    public void Return(User user, Equipment equipment, DateTime returnDate)
    {
        var rental = rentals.FirstOrDefault(r =>
            r.User == user &&
            r.Equipment == equipment &&
            r.ReturnDateReally == null);
        if (rental == null)
        {
            Console.WriteLine("Error: Equipment already returned or not rented by this user!");
            return;
        }

        int punishment = 0;

        if (returnDate > rental.ReturnDatePlanned)
        {
            int daysLate = (int)(returnDate - rental.ReturnDatePlanned).TotalDays;
            punishment = daysLate * 20;
            Console.WriteLine($"Punishment: {punishment} zł");
        }

        rental.Equipment.setAvailable();
        rental.ReturnRegister(returnDate, punishment);

        Console.WriteLine("Equipment returned!");
    }
    
    public void AddEquipment(Equipment equipment)
    {
        equipments.Add(equipment);
    }

    public void DisplayAllEquipment()
    {
        Console.WriteLine("\n EQUIPMENT LIST");
        foreach (var eq in equipments)
        {
            Console.WriteLine($"- {eq}");
        }
    }

    public void DisplayAvailableEquipment()
    {
        Console.WriteLine("\n EQUIPMENT AVAILABLE");
        var available = equipments.Where(e => e.IsAvailable).ToList();

        if (!available.Any())
        {
            Console.WriteLine("This equipment is not available.");
            return;
        }

        foreach (var eq in available)
        {
            Console.WriteLine($"- {eq}");
        }
    }

    public void DisplayUserActiveRentals(User user)
    {
        Console.WriteLine($"\n ACTIVE RENTALS  {user.Name}");
        var active = rentals.Where(r => r.User == user && r.ReturnDateReally == null).ToList();

        if (!active.Any())
        {
            Console.WriteLine("NO ACTIVE RENTALS!");
            return;
        }

        foreach (var r in active)
        {
            Console.WriteLine($"- {r}");
        }
    }

    public void DisplayOverdueRentals()
    {
        Console.WriteLine("\n PROLONGED RENTALS ");
        var overdue = rentals.Where(r => r.IsProlonged && r.ReturnDateReally == null).ToList();

        if (!overdue.Any())
        {
            Console.WriteLine("NO PROLONGED RENTALS!");
            return;
        }

        foreach (var r in overdue)
        {
            Console.WriteLine($"- {r}");
        }
    }

    public void GenerateSummaryReport(List<Equipment> allEquipment)
    {
        Console.WriteLine("\n=== RENTAL REPORT ===");
        Console.WriteLine($"Total equipment count:    {allEquipment.Count}");
        Console.WriteLine($"Available equipment:      {allEquipment.Count(e => e.IsAvailable)}");
        Console.WriteLine($"Rented equipment:         {allEquipment.Count(e => !e.IsAvailable)}");
        Console.WriteLine($"---");
        Console.WriteLine($"Total rentals history:    {rentals.Count}");
        Console.WriteLine($"Currently active rentals: {rentals.Count(r => r.ReturnDateReally == null)}");
        Console.WriteLine($"Overdue rentals:          {rentals.Count(r => r.IsProlonged && r.ReturnDateReally == null)}");
        Console.WriteLine("===============================\n");
    }
}