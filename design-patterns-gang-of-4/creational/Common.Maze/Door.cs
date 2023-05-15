namespace Common.Maze;

public class Door : MapSite
{
    public bool IsOpen { get; set; }
    private Room _fromRoom;
    private Room _toRoom;

    public Door(Room fromRoom, Room toRoom, bool isOpen = false)
    {
        _fromRoom = fromRoom;
        _toRoom = toRoom;
        IsOpen = isOpen;
    }
    public Room OtherSideFrom(Room room) => throw new NotImplementedException();
    public override void Enter() => throw new NotImplementedException();
}