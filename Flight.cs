namespace Group_project_2.Models
{
    public class Flight
    {
        public string FlightCode { get; }
        public string Airline { get; }
        public string Origin { get; }
        public string Destination { get; }
        public string Day { get; }
        public string Time { get; }
        public double Cost { get; }
        public int Seats { get; private set; }

        public Flight(string flightCode, string airline, string origin, string destination, string day, string time, double cost, int seats)
        {
            FlightCode = flightCode;
            Airline = airline;
            Origin = origin;
            Destination = destination;
            Day = day;
            Time = time;
            Cost = cost;
            Seats = seats;
        }

        public bool IsFull() => Seats <= 0;

        public void BookSeat()
        {
            if (IsFull())
                throw new InvalidOperationException("No more seats available.");
            Seats--;
        }

        public override string ToString() => $"{FlightCode} - {Airline} ({Origin} to {Destination}) on {Day} at {Time}, ${Cost}, Seats: {Seats}";
    }
}