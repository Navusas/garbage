using Common.Maze;

namespace DesignPatterns.AbstractFactory;

public class Spell {}

public class EnchantedRoom : Room
{
    private int _roomNumber;
    private Spell _spell;
    public EnchantedRoom(int roomNumber, Spell spell) : base(roomNumber)
    {
        _roomNumber = roomNumber;
        _spell = spell;
    }
}

public class DoorNeedingSpell : Door
{
    private Room _room1;
    private Room _room2;
    public DoorNeedingSpell(Room room1, Room room2) : base(room1, room2)
    {
        _room1 = room1;
        _room2 = room2;
    }
}


/// <summary>
/// The enchanted maze factory has the same walls, but the rooms are enchanted and the doors need a spell to be opened.
/// </summary>
public class EnchantedMazeFactory : MazeFactory
{
    private Spell CastSpell() => new();
    public override Room MakeRoom(int roomNumber) => new EnchantedRoom(roomNumber, CastSpell());
    public override Door MakeDoor(Room room1, Room room2) => new DoorNeedingSpell(room1, room2);
}