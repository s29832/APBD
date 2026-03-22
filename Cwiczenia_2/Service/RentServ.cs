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
    private readonly EqServ equipmentService;
    
    public RentalService(EqServ equipmentService)
    {
        this.equipmentService = equipmentService;
    }
    
    public void RentEquipment(User user, Equipment equipment, int days)
    {
        if (!equipment.IsAvailable)
            Console.WriteLine($"{equipment.Name} is not available!");

        int activeRent = rentals.Count(r => r.User == user && r.ReturnDateReally == null);
        if (activeRent >= user.MaxLimit)
            Console.WriteLine($"{user.Name} has reached the limit of rentals!");

        var newRental = new Rental(user, equipment, days);
        rentals.Add(newRental);
        equipment.setUnavailable();
    }

    public void ReturnEquipment(Guid rentalId, DateTime returnDate)
    {
        var rental = rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null || rental.ReturnDateReally != null)
            Console.WriteLine("RENTAL NOT FOUND OR RETURNED ALREADY!");

        int punishment = 0;
        if (returnDate > rental.ReturnDatePlanned)
        {
            int daysLate = (int)(returnDate - rental.ReturnDatePlanned).TotalDays;
            punishment = daysLate * 20;
        }

        rental.Equipment.setAvailable();
        rental.ReturnRegister(returnDate, punishment);
    }
    
    public Rental? GetRentalById(Guid id) => rentals.FirstOrDefault(r => r.Id == id);
    public List<Rental> GetRentals() => rentals.ToList();
    public List<Rental> GetActiveRentals() => rentals.Where(r => r.ReturnDateReally == null).ToList();
    public List<Rental> GetExpiredRentals() => rentals.Where(r => r.IsProlonged && r.ReturnDateReally == null).ToList();
    public List<Rental> GetActiveByUser(User user) => rentals.Where(r => r.User == user && r.ReturnDateReally == null).ToList();

    public string GetSummary()
    {
        var eq = equipmentService.GetAllEquipments();
        var r = rentals;

        return $@"
            === RENTAL SHOP REPORT ===
            Total equipment count: {eq.Count}
            Available equipment:  {eq.Count(e => e.IsAvailable)}
            Rented equipment:       {eq.Count(e => !e.IsAvailable)}
            ---
            History of all rentals: {r.Count}
            Ongoing rentals: {r.Count(x => x.ReturnDateReally == null)}
            Overdue rentals:   {r.Count(x => x.IsProlonged && x.ReturnDateReally == null)}";
    }
}