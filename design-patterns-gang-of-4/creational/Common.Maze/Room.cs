namespace Common.Maze;

public class Room : MapSite
{
    private readonly Dictionary<Direction, MapSite> _sides = new();
    public int RoomNumber { get; set; }

    public Room(int roomNumber)
    {
        RoomNumber = roomNumber;
    }

    public MapSite GetSide(Direction direction) => throw new NotImplementedException();
    public void SetSide(Direction direction, MapSite mapSite) => _sides.Add(direction, mapSite);
    public override void Enter() => throw new NotImplementedException();
}