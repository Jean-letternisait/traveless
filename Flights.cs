namespace TravelessSystem.Models
{
    public class Flight
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public int Seats { get; set; }
        public double Cost { get; set; }
    }
}
