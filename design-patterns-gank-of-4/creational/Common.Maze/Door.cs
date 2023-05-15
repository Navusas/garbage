namespace Common.Maze;

public class Door : MapSite
{
    public bool IsOpen { get; set; }
    public Room FromRoow { get; set; }
    public Room ToRoom { get; set; }

    public Door(Room fromRoom, Room toRoom, bool isOpen = false)
    {
        FromRoow = fromRoom;
        ToRoom = toRoom;
        IsOpen = isOpen;
    }
    public override void Enter() => throw new NotImplementedException();
}