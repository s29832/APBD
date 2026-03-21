using System;
using System.Collections.Generic;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Rentals;
using Cwiczenia_2.Model.Users;

namespace Cwiczenia_2.Service;

public class RentalService
{
    private readonly List<Rental> rentals = new();

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
            int daysLate = (int) (returnDate - rental.ReturnDatePlanned).TotalDays;
            punishment = daysLate * 20;
            Console.WriteLine($"Punishment: {punishment} zł");
        }
        rental.Equipment.setAvailable();
        rental.ReturnRegister(returnDate, punishment);
    
        Console.WriteLine("Equipment returned!");
    }
}