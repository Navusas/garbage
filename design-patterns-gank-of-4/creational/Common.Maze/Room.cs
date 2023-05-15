namespace Common.Maze;

public class Room : MapSite
{
    public int RoomNumber { get; set; }

    public Room(int roomNumber)
    {
        RoomNumber = roomNumber;
    }
    public MapSite GetSide(Direction direction)
    {
        throw new NotImplementedException();
    }
    public void SetSide(Direction direction, MapSite mapSite)
    {
        throw new NotImplementedException();
    }

    public override void Enter()
    {
        throw new NotImplementedException();
    }
}