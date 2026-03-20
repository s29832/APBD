using System;
using Cwiczenia_2.Model.Users;
using Cwiczenia_2.Model.Equipments;

namespace Cwiczenia_2.Model.Rentals;

public class Rental
{
    public User User { get; private set; }
    public Equipment Equipment { get; private set; }
    public DateTime RentalDate { get; private set; }
    public DateTime ReturnDatePlanned { get; private set; }
    public DateTime? ReturnDateReally { get; private set; }
    public int? Punishment { get; private set; }

    public Rental(User user, Equipment equipment, int days)
    {
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        ReturnDatePlanned = RentalDate.AddDays(days);
    }
}