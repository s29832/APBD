using System.Collections.Generic;
using Cwiczenia_2.Model.Rentals;

namespace Cwiczenia_2.Service;

public class RentalService
{
    private List<Rental> rentals;

    public RentalService()
    {
        rentals = new List<Rental>();
    }
}