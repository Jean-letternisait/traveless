using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelessSystem.Models;

namespace TravelessSystem.Services
{
  
    /// Manages flight data, including loading from CSV and searching.
    
    public class FlightManager
    {
        private List<Flight> flights;

        public FlightManager()
        {
            flights = LoadFlightsFromCsv("Data/flights.csv");
        }


        // Find matching flights based on origin, destination, and day.
        
        public List<Flight> FindFlights(string origin, string destination, string day)
        {
            return flights
                .Where(f =>
                    f.Origin.Equals(origin, StringComparison.OrdinalIgnoreCase) &&
                    f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
                    f.Day.Equals(day, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

       
        // Load flights from a CSV file.
        
        private List<Flight> LoadFlightsFromCsv(string filePath)
        {
            var flightList = new List<Flight>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[Error] File not found: {filePath}");
                return flightList;
            }

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length != 7)
                    continue;

                var flight = new Flight
                {
                    FlightCode = parts[0].Trim(),
                    Airline = parts[1].Trim(),
                    Origin = parts[2].Trim(),
                    Destination = parts[3].Trim(),
                    Day = parts[4].Trim(),
                    Time = parts[5].Trim(),
                    Cost = decimal.TryParse(parts[6], out var cost) ? (double)cost : 0
                };

                flightList.Add(flight);
            }

            return flightList;
        }
    }
}
