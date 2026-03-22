using System;
using System.Collections.Generic;
using System.Linq;
using Cwiczenia_2.Model.Equipments;

namespace Cwiczenia_2.Service;

public class EqServ
{
    private readonly List<Equipment> equipments = new();

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
}