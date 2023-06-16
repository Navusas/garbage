namespace DesignPatterns.Behavioral.Mediator;

public static class ChatRoomExample
{
    public static void Run()
    {
        var chatRoom = new ChatRoom();

        var alice = new User("Alice");
        var bob = new User("Bob");
        var charlie = new User("Charlie");

        chatRoom.Register(alice);
        chatRoom.Register(bob);
        chatRoom.Register(charlie);

        alice.SendMessage("Hello, Bob!");
        bob.SendMessage("Hi, Alice!");
        charlie.SendMessage("Hi, everyone!");
       
    }
}

interface IMediator
{
    void SendMessage(string message, Participant sender);
}

class ChatRoom : IMediator
{
    private readonly List<Participant> _participants = new();

    public void Register(Participant participant)
    {
        _participants.Add(participant);
        participant.SetMediator(this);
    }

    public void SendMessage(string message, Participant sender)
    {
        foreach (var participant in _participants.Where(participant => participant != sender))
        {
            participant.ReceiveMessage(message);
        }
    }
}

abstract class Participant
{
    private IMediator _mediator;

    public void SetMediator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void SendMessage(string message)
    {
        _mediator.SendMessage(message, this);
    }

    public abstract void ReceiveMessage(string message);
}

class User : Participant
{
    public string Name { get; }
    public User(string name)
    {
        Name = name;
    }
    
    public override void ReceiveMessage(string message)
    {
        Console.WriteLine($"{Name} received: {message}");
    }
}