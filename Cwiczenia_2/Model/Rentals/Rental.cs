using System;
using Cwiczenia_2.Model.Users;
using Cwiczenia_2.Model.Equipments;

namespace Cwiczenia_2.Model.Rentals;

public class Rental
{
    public Guid Id { get; private set; }
    public User User { get; private set; }
    public Equipment Equipment { get; private set; }
    public DateTime RentalDate { get; private set; }
    public DateTime ReturnDatePlanned { get; private set; }
    public DateTime? ReturnDateReally { get; private set; }
    public int? Punishment { get; private set; }

    public Rental(User user, Equipment equipment, int days)
    {
        Id = Guid.NewGuid();
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        ReturnDatePlanned = RentalDate.AddDays(days);
    }

    public bool IsProlonged {
        get {
            if (ReturnDateReally.HasValue)
                return ReturnDateReally > ReturnDatePlanned;
            else
                return DateTime.Now > ReturnDatePlanned;
        }
    }
    public void ReturnRegister(DateTime returnDate, int punishment)
    {
        ReturnDateReally = returnDate;
        Punishment = punishment;
    }
    public override string ToString()
    {
        string warning = IsProlonged && ReturnDateReally == null ? "[PROLONGED] " : "";
        return $"[ID: {Id.ToString().Substring(0,8)}] {warning}{Equipment.Name} prolonged by {User.Name} (Planned return: {ReturnDatePlanned:yyyy-MM-dd})";
    }
}