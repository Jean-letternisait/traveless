using Group_project_2.Models;
using System.Text.Json;

namespace Group_project_2.Manager
{
    public class ReservationManager
    {
        private List<Reservation> reservations;
        private readonly string filePath = Path.Combine("Data", "reservations.json");
        private readonly Random random;

        public ReservationManager()
        {
            reservations = new List<Reservation>();
            random = new Random();
            LoadReservations();
        }

        public Reservation MakeReservation(Flight flight, string travelerName, string citizenship)
        {
            if (flight == null) throw new ArgumentNullException(nameof(flight));

            var code = GenerateReservationCode();
            var reservation = new Reservation
            {
                ReservationCode = code,
                FlightCode = flight.FlightCode,
                Airline = flight.Airline,
                Origin = flight.Origin,
                Destination = flight.Destination,
                Day = flight.Day,
                Time = flight.Time,
                Cost = flight.Cost,
                TravelerName = travelerName,
                Citizenship = citizenship,
                Status = "Active"
            };

            reservations.Add(reservation);
            SaveReservations();
            return reservation;
        }

        private string GenerateReservationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void SaveReservations()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            var json = JsonSerializer.Serialize(reservations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private void LoadReservations()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    reservations = JsonSerializer.Deserialize<List<Reservation>>(json) ?? new List<Reservation>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load reservations: {ex.Message}");
                    reservations = new List<Reservation>();
                }
            }
        }
    }
}
