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
    private readonly List<User> users = new();

  
    public void AddUser(User user) => users.Add(user);
    public List<User> GetAllUsers() => users;
    public User? GetUserById(Guid id) => users.FirstOrDefault(u => u.Id == id);


    public void AddEquipment(Equipment equipment) => equipments.Add(equipment);
    public List<Equipment> GetAllEquipments() => equipments;
    public List<Equipment> GetAvailableEquipments() => equipments.Where(e => e.IsAvailable).ToList();
    public Equipment? GetEquipmentById(Guid id) => equipments.FirstOrDefault(e => e.Id == id);
    
    public void RemoveEquipment(Equipment equipment) => equipments.Remove(equipment);
    public void SetUnavailable(Guid id) 
    { 
        var eq = GetEquipmentById(id); 
        if (eq != null) eq.setUnavailable(); 
    }


    public void Rent(User user, Equipment equipment, int days)
    {
        if (!equipment.IsAvailable)
        {
            throw new InvalidOperationException($"{equipment.Name} is not available!");
        }

        int activeRent = rentals.Count(rental => rental.User == user && rental.ReturnDateReally == null);
        if (activeRent >= user.MaxLimit)
        {
            throw new InvalidOperationException($"{user.Name} has reached the limit of rentals!");
        }

        var newRental = new Rental(user, equipment, days);
        rentals.Add(newRental);
        equipment.setUnavailable();
    }

    public void Return(Guid rentalId, DateTime returnDate)
    {
        var rental = GetRentalById(rentalId);
        if (rental == null || rental.ReturnDateReally != null)
        {
            throw new InvalidOperationException("Error: Equipment already returned or rental not found!");
        }

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
    public List<Rental> GetRentals() => rentals;
    public List<Rental> GetActiveRentals() => rentals.Where(r => r.ReturnDateReally == null).ToList();
    public List<Rental> GetExpiredRentals() => rentals.Where(r => r.IsProlonged && r.ReturnDateReally == null).ToList();
    public List<Rental> GetActiveByUser(User user) => rentals.Where(r => r.User == user && r.ReturnDateReally == null).ToList();

    public string GetSummary()
    {
        return $@"
            RENTAL REPORT
            Total equipment count:    {equipments.Count}
            Available equipment:      {equipments.Count(e => e.IsAvailable)}
            Rented equipment:         {equipments.Count(e => !e.IsAvailable)}
            ---
            Total rentals history:    {rentals.Count}
            Currently active rentals: {rentals.Count(r => r.ReturnDateReally == null)}
            Overdue rentals:          {rentals.Count(r => r.IsProlonged && r.ReturnDateReally == null)}";
    }
}