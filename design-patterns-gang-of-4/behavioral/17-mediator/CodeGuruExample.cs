namespace DesignPatterns.Behavioral.Mediator;

public static class AirTrafficControllerExample
{
    public static void Run()
    {
        var mediator = new AirTrafficControlMediator();

        var flight1 = new Flight("Flight 1", mediator);
        var flight2 = new Flight("Flight 2", mediator);
        var flight3 = new Flight("Flight 3", mediator);

        mediator.RegisterFlight(flight1);
        mediator.RegisterFlight(flight2);
        mediator.RegisterFlight(flight3);

        flight1.Send("Flight 2", "Flight 1 to Flight 2: Proceed with landing clearance.");
        flight2.Send("Flight 1", "Flight 2 to Flight 1: Acknowledged. Preparing for landing.");
        flight3.Send("Flight 2", "Flight 3 to Flight 2: Requesting landing clearance.");

        Console.ReadLine();
    }
}

interface IAirTrafficControlMediator
{
    void RegisterFlight(Flight flight);
    void Send(string fromFlight, string toFlight, string message);
}

class AirTrafficControlMediator : IAirTrafficControlMediator
{
    private readonly Dictionary<string, Flight> _flights = new();

    public void RegisterFlight(Flight flight)
    {
        _flights[flight.Name] = flight;
    }

    public void Send(string fromFlight, string toFlight, string message)
    {
        if (_flights.TryGetValue(toFlight, out var value))
        {
            value.Receive(fromFlight, message);
        }
        else
        {
            Console.WriteLine($"Flight {toFlight} not found. Message not delivered.");
        }
    }
}

class Flight
{
    public string Name { get; private set; }
    private readonly IAirTrafficControlMediator _mediator;

    public Flight(string name, IAirTrafficControlMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void Send(string toFlight, string message)
    {
        Console.WriteLine($"{Name} sends message: {message}");
        _mediator.Send(Name, toFlight, message);
    }

    public void Receive(string fromFlight, string message)
    {
        Console.WriteLine($"{Name} received message: {message} from {fromFlight}");
    }
}