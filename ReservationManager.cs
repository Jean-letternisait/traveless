using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TravelessSystem.Models;

namespace TravelessSystem.Services
{
    public class ReservationManager
    {
        private List<Reservation> reservations = new();
        private const string FileName = "Data/reservations.json";

        public ReservationManager()
        {
            LoadReservations();
        }

        public Reservation MakeReservation(Flight flight, string name, string citizenship)
        {
            if (flight == null)
                throw new ArgumentException("Flight must be selected.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(citizenship))
                throw new ArgumentException("Citizenship is required.");

            int count = reservations.Count(r => r.Flight.FlightCode == flight.FlightCode);
            if (count >= 5)
                throw new InvalidOperationException("Flight is fully booked.");

            var reservation = new Reservation
            {
                Code = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                Flight = flight,
                Name = name,
                Citizenship = citizenship
            };

            reservations.Add(reservation);
            return reservation;
        }

        public void PersistReservations()
        {
            Directory.CreateDirectory("Data");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(reservations, options);
            File.WriteAllText(FileName, json);
        }

        private void LoadReservations()
        {
            if (!File.Exists(FileName)) return;

            try
            {
                string json = File.ReadAllText(FileName);
                reservations = JsonSerializer.Deserialize<List<Reservation>>(json) ?? new();
            }
            catch
            {
                reservations = new List<Reservation>();
            }
        }

        public List<Reservation> GetAllReservations() => reservations;
    }
}
