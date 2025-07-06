
using System;
namespace TravelessSystem.Models;


[Serializable]
public class Reservation
{
    public string Code { get; set; }          
    public Flight Flight { get; set; }        
    public string Name { get; set; }          
    public string Citizenship { get; set; }   

    public override string ToString()
    {
        return $"Reservation Code: {Code}, Flight: {Flight}, Name: {Name}, Citizenship: {Citizenship}";
    }
}
