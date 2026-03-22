﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cwiczenia_2.Model.Equipments;
using Cwiczenia_2.Model.Rentals;
using Cwiczenia_2.Model.Users;

namespace Cwiczenia_2.Service;

public class RentServ
{
    private readonly List<Rental> rentals = new();
    private readonly EqServ equipmentService;
    
    public RentServ(EqServ equipmentService)
    {
        this.equipmentService = equipmentService;
    }
    
    public void RentEquipment(User user, Equipment equipment, int days)
    {
        if (!equipment.IsAvailable)
            throw new InvalidOperationException($"{equipment.Name} is not available");

        int activeRent = rentals.Count(r => r.User == user && r.ReturnDateReally == null);
        
        if (activeRent >= user.MaxLimit)
            throw new InvalidOperationException($"{user.Name} has reached the limit of rentals");

        var newRental = new Rental(user, equipment, days);
        rentals.Add(newRental);
        equipment.SetUnavailable();
    }

    public void ReturnEquipment(string rentalId, DateTime returnDate)
    {
        var rental = rentals.FirstOrDefault(r => r.Id.ToString().StartsWith(rentalId));
        if (rental == null || rental.ReturnDateReally != null)
            throw new InvalidOperationException("rental not found or already returned");

        int punishment = 0;
        if (returnDate > rental.ReturnDatePlanned)
        {
            int daysLate = (int)(returnDate - rental.ReturnDatePlanned).TotalDays;
            punishment = daysLate * 20;
        }

        rental.Equipment.SetAvailable();
        rental.ReturnRegister(returnDate, punishment);
    }
    
    public Rental? GetRentalById(string id) => rentals.FirstOrDefault(r => r.Id.ToString().StartsWith(id));
    public List<Rental> GetRentals() => rentals.ToList();
    public List<Rental> GetExpiredRentals() => rentals.Where(r => r.IsProlonged && r.ReturnDateReally == null).ToList();
    public List<Rental> GetActiveByUser(User user) => rentals.Where(r => r.User == user && r.ReturnDateReally == null).ToList();

    public string GetSummary()
    {
        var eq = equipmentService.GetAllEquipments();
        var r = rentals;

        return $@"
            REPORT
            Total equipment count {eq.Count}
            Available equipment  {eq.Count(e => e.IsAvailable)}
            Rented equipment       {eq.Count(e => !e.IsAvailable)}
            
            History of all rentals {r.Count}
            Ongoing rentals {r.Count(x => x.ReturnDateReally == null)}
            Overdue rentals   {r.Count(x => x.IsProlonged && x.ReturnDateReally == null)}";
    }
}